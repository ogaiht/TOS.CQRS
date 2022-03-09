using System;

namespace TOS.CQRS.Logging
{
    public interface IHandlerExecutorLoggerScope : IDisposable
    {
        void BeforeExecution();
        void AfterExecution();
        void OnError(Exception ex);
        IDisposable TimeExecution();
    }

    public interface IHandlerExecutorLoggerScope<in TResult> : IHandlerExecutorLoggerScope
    {
        void AfterExecution(TResult result);
    }
}
