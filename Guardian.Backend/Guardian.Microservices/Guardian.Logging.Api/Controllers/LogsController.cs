using System;
using System.Threading.Tasks;
using Guardian.Logging.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Guardian.Logging.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Log log)
        {
            var message = $"[{log.LogLevel}] {log.DateTime:yyyy-MM-dd HH:mm:ss.fff} - {log.Message}" + Environment.NewLine;
            await System.IO.File.AppendAllTextAsync($"{Environment.CurrentDirectory}\\log.txt", message);
            return Ok(log);
        }
    }
}
