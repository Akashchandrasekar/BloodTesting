using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodTesting.Data;
using BloodTesting.Models;

namespace BloodTesting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalTestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicalTestsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTests(int page = 1, int pageSize = 10, string sortBy = "Name", string sortDir = "asc")
        {
            try
            {
                var query = _context.MedicalTests
                                    .Where(t => !t.IsDeleted);

                // Case-insensitive sort column lookup
                var property = typeof(MedicalTest)
                    .GetProperties()
                    .FirstOrDefault(p => p.Name.Equals(sortBy, StringComparison.OrdinalIgnoreCase));

                if (property == null)
                    return BadRequest($"Invalid sort column: {sortBy}");

                query = sortDir.ToLower() == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, property.Name))
                    : query.OrderBy(e => EF.Property<object>(e, property.Name));

                var totalRecords = await query.CountAsync();
                var data = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new
                {
                    total = totalRecords,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}
