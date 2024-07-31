using System.Diagnostics;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;

namespace NTDHunter.SharedKernel;

/// <summary>
/// Adds logging for all requests in MediatR pipeline.
/// Configure by adding the service with a scoped lifetime
/// 
/// Example for Autofac:
/// builder
///   .RegisterType&lt;Mediator&gt;()
///   .As&lt;IMediator&gt;()
///   .InstancePerLifetimeScope();
///
/// builder
///   .RegisterGeneric(typeof(LoggingBehavior&lt;,&gt;))
///      .As(typeof(IPipelineBehavior&lt;,&gt;))
///   .InstancePerLifetimeScope();
///
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class LoggingBehavior<TRequest, TResponse>(ILogger<Mediator> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<Mediator> _logger = logger;

    /// <summary>
    /// Logging information of command and query pipelines
    /// </summary>
    /// <param name="request">Type of request <see cref="ICommand{TResponse}"/> or <see cref="IQuery{TResponse}"/>, etc.</param>
    /// <param name="next">Delegate of handler</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

            // Reflection! Could be a performance concern
            Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object? propValue = prop?.GetValue(request, null);
                _logger.LogInformation("Property {Property} : {@Value}", prop?.Name, propValue);
            }
        }

        var sw = Stopwatch.StartNew();

        var response = await next();

        _logger.LogInformation("Handled {RequestName} with {Response} in {ms} ms", typeof(TRequest).Name, response, sw.ElapsedMilliseconds);
        sw.Stop();
        return response;
    }
}
