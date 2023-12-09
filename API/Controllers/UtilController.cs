using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController : ControllerBase
    {
        [HttpGet("overlap")]
        public IActionResult CheckOverlap([FromQuery] int x1, [FromQuery] int x2, [FromQuery] int x3, [FromQuery] int x4)
        {
            bool overlap = AreLinesOverlapping(x1, x2, x3, x4);

            if (overlap)
            {
                return Ok("The lines overlap.");
            }
            return Ok("The lines do not overlap.");
        }
        
        [HttpGet("compare")]
        public IActionResult CompareVersions([FromQuery] string version1, [FromQuery] string version2)
        {
            int result = VersionComparer.CompareVersions(version1, version2);

            if (result < 0)
            {
                return Ok($"{version1} is less than {version2}.");
            }

            if (result > 0)
            {
                return Ok($"{version1} is greater than {version2}.");
            }

            return Ok($"{version1} is equal to {version2}.");
        }

        private static bool AreLinesOverlapping(int x1, int x2, int x3, int x4)
        {
            return Math.Max(x1, x3) <= Math.Min(x2, x4);
        }
    }
}
