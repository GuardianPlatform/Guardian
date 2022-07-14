using System;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Logging.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Guardian.Logging.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Log log)
        {
            var message = $"[{log.LogLevel}] {log.DateTime:yyyy-MM-dd HH:mm:ss.fff} - {log.Message}" + Environment.NewLine;

            await semaphore.WaitAsync();
            try
            {
                await System.IO.File.AppendAllTextAsync($"{Environment.CurrentDirectory}\\log.txt", message);
            }
            finally
            {
                semaphore.Release();
            }

            return Ok(log);
        }
    }
}
