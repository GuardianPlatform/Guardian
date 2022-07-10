using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Domain.Settings;
using Guardian.Logging.Api;
using Guardian.Logging.Contract;
using Microsoft.Extensions.Options;

namespace Guardian.Infrastructure.Communication
{
    public class LoggingApiClient : ILoggingApiClient
    {
        private readonly RestClient _client;

        public LoggingApiClient(IOptions<MicroservicesSettings> options)
        {
            _client = new RestClient(options.Value.LoggingApiUrl);
        }

        public async Task<HttpStatusCode> Log(string message, DateTime dateTime, LogLevel logLevel = LogLevel.Information, CancellationToken cancellationToken = default)
        {
            var result = await _client.PostJsonAsync<Log>("api/logs",
                new Log() { Message = message, DateTime = dateTime, LogLevel = logLevel },
                cancellationToken);

            return result;
        }
    }

    public interface ILoggingApiClient
    {
        Task<HttpStatusCode> Log(string message, DateTime dateTime, LogLevel logLevel = LogLevel.Information,
            CancellationToken cancellationToken = default);
    }
}
