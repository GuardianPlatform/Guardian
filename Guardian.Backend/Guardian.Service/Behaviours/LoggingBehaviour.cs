using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Guardian.Infrastructure.Communication;
using Guardian.Logging.Contract;
using MediatR;
using Newtonsoft.Json;

namespace Guardian.Service.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILoggingApiClient _loggingApiClient;

        public LoggingBehaviour(ILoggingApiClient loggingApiClient)
        {
            _loggingApiClient = loggingApiClient;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = JsonConvert.SerializeObject(request);
            await _loggingApiClient.Log($"Handling {typeof(TRequest).Name}: " + result, DateTime.UtcNow, LogLevel.Information, cancellationToken);
            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();
            await _loggingApiClient.Log($"Handled {typeof(TRequest).Name}: {result}. Total elapsed {stopwatch.ElapsedMilliseconds}ms", DateTime.UtcNow, LogLevel.Information, cancellationToken);

            return response;
        }
    }
}
