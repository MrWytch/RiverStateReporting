using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiverStateReporting.Data;
using RiverStateReporting.Data.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RiverStateReporting.Controllers
{
    /// <summary>
    /// API Controller for recieving values via HTTP POST requests and saving them in DB.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StationsController> _logger;

        public StationsController(ApplicationDbContext context, ILogger<StationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new value entry for a specified station.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostValue([FromBody] Value value)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is invalid: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var station = await _context.Stations.FindAsync(value.StationId);
            if (station == null)
            {
                _logger.LogError("Station with ID {StationId} not found.", value.StationId);
                return NotFound("Station not found.");
            }

            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
