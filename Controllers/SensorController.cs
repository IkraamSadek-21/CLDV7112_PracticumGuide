using Microsoft.AspNetCore.Mvc;
using CLDV7112_PracticumGuide.Data;
using CLDV7112_PracticumGuide.Models;

namespace CLDV7112_PracticumGuide.Controllers
{
    public class SensorController : Controller
    {

        [ApiController]
        [Route("api/[controller]")]
        public class SensorApiController : ControllerBase
        {
            private readonly AppDbContext _context;
            public SensorApiController(AppDbContext context)
            {
                _context = context;
            }


            [HttpPost]
            public async Task<IActionResult> PostSensorReading([FromBody] SensorReading reading)
            {
                if (reading == null)
                {
                    return BadRequest();
                }
                reading.RecordedAt = DateTime.UtcNow; // Set the RecordedAt to current UTC time
                _context.SensorReadings.Add(reading);
                await _context.SaveChangesAsync();
                return Ok();
            }

            [HttpGet]
            public IActionResult GetReadings()
            { 
            var data = _context.SensorReadings
                    .OrderByDescending( x => x.RecordedAt)
                    .Take(20)
                    .ToList();
                return Ok(data);


            }




        }



    }
}
