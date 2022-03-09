using Microsoft.Extensions.Logging;
using System;
using TOS.Common;
using TOS.Common.Serialization.Json;
using TOS.CQRS.Handlers;
using TOS.Extensions.Logging;

namespace TOS.CQRS.Logging
{
    public class HandlerExecutorLoggerScope : Disposable, IHandlerExecutorLoggerScope
    {
        protected ILogger<RequestExecutor> Logger { get; }
        protected string Identity { get; }
        protected Lazy<string> SerializedRequest { get; }

        public HandlerExecutorLoggerScope(
            ILogger<RequestExecutor> logger,
            string identity,
            Lazy<string> serializedRequest
            )
        {
            Logger = logger;
            Identity = identity;
            SerializedRequest = serializedRequest;
        }

        public void BeforeExecution()
        {
            Logger.LogInformation("Executing {identity}.", Identity);
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Executing {identity} for request '{request}'.",
                    Identity, SerializedRequest.Value);
            }
        }

        public void AfterExecution()
        {
            Logger.LogInformation("Executed {identity}.", Identity);
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Executed {identity} for request '{request}'.",
                    Identity, SerializedRequest.Value);
            }
        }

        public void OnError(Exception ex)
        {
            Logger.LogError(ex, "Error on execute {identity} for {request}", Identity, SerializedRequest.Value);
        }

        protected override void Dispose(bool disposing)
        {
            string finishStatus = disposing ? "Dispose" : "Destructor";
            Logger.LogInformation("Execution {identity} finished by " + finishStatus + ".", Identity);
        }

        public IDisposable TimeExecution()
        {
            return Logger.TimeExecution(Identity);
        }
    }

    public class HandlerExecutorLoggerScope<TResult> : HandlerExecutorLoggerScope, IHandlerExecutorLoggerScope<TResult>
    {
        private readonly IJsonSerializer _jsonSerializer;

        public HandlerExecutorLoggerScope(
            ILogger<RequestExecutor> logger,
            string identity,
            Lazy<string> serializedRequest,
            IJsonSerializer jsonSerializer
            )
            : base(logger, identity, serializedRequest)
        {
            _jsonSerializer = jsonSerializer;
        }

        public void AfterExecution(TResult result)
        {
            Logger.LogInformation("Executed {identity}.", Identity);
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Executed {identity} for request '{request}' with result '{result}'.",
                        Identity, SerializedRequest.Value, _jsonSerializer.Serialize(result));
            }
        }
    }
}
