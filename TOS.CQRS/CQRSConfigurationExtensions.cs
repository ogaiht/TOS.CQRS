using Microsoft.Extensions.DependencyInjection;
using TOS.CQRS.Dispatchers;
using TOS.CQRS.Dispatchers.Commands;
using TOS.CQRS.Dispatchers.Events;
using TOS.CQRS.Dispatchers.Queries;
using TOS.CQRS.Executions.Commands;
using TOS.CQRS.Executions.Events;
using TOS.CQRS.Executions.Queries;
using TOS.CQRS.Handlers;
using TOS.CQRS.Handlers.Commands;
using TOS.CQRS.Handlers.Events;
using TOS.CQRS.Handlers.Queries;
using TOS.CQRS.Logging;

namespace TOS.CQRS
{
    public static class CQRSConfigurationExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            services
                .AddTransient<IExecutionHandlerProvider, ExecutionHandlerProvider>()
                .AddTransient<ICommandDispatcher, CommandDispatcher>()
                .AddTransient<IQueryDispatcher, QueryDispatcher>()
                .AddTransient<IEventDispatcher, EventDispatcher>()
                .AddTransient<IRequestExecutor, RequestExecutor>()
                .AddTransient<IHandlerExecutorIdGenerator, HandlerExecutorIdGenerator>()
                .AddTransient<IHandlerExecutorLogger, HandlerExecutorLogger>();
            return services;
        }

        public static IServiceCollection AddCommand<TCommand, TCommandHandler>(this IServiceCollection services)
            where TCommand : class, ICommand
            where TCommandHandler : class, ICommandHandler<TCommand>
        {
            services
                .AddTransient<ICommandHandler<TCommand>, TCommandHandler>();
            return services;
        }

        public static IServiceCollection AddCommand<TCommand, TResult, TCommandHandler>(this IServiceCollection services)
            where TCommand : class, ICommand<TResult>
            where TCommandHandler : class, ICommandHandler<TCommand, TResult>
        {
            services
                .AddTransient<ICommandHandler<TCommand, TResult>, TCommandHandler>();
            return services;
        }

        public static IServiceCollection AddAsyncCommand<TAsyncCommand, TAsyncCommandHandler>(this IServiceCollection services)
            where TAsyncCommand : class, IAsyncCommand
            where TAsyncCommandHandler : class, IAsyncCommandHandler<TAsyncCommand>
        {
            services
                .AddTransient<IAsyncCommandHandler<TAsyncCommand>, TAsyncCommandHandler>();
            return services;
        }

        public static IServiceCollection AddAsyncCommand<TAsyncCommand, TResult, TAsyncCommandHandler>(this IServiceCollection services)
            where TAsyncCommand : class, IAsyncCommand<TResult>
            where TAsyncCommandHandler : class, IAsyncCommandHandler<TAsyncCommand, TResult>
        {
            services
                .AddTransient<IAsyncCommandHandler<TAsyncCommand, TResult>, TAsyncCommandHandler>();
            return services;
        }

        public static IServiceCollection AddQuery<TQuery, TResult, TQueryHandler>(this IServiceCollection services)
           where TQuery : class, IQuery<TResult>
           where TQueryHandler : class, IQueryHandler<TQuery, TResult>
        {
            services
                .AddTransient<IQueryHandler<TQuery, TResult>, TQueryHandler>();
            return services;
        }

        public static IServiceCollection AddAsyncQuery<TAsyncQuery, TResult, TQueryHandler>(this IServiceCollection services)
           where TAsyncQuery : class, IAsyncQuery<TResult>
           where TQueryHandler : class, IAsyncQueryHandler<TAsyncQuery, TResult>
        {
            services
                .AddTransient<IAsyncQueryHandler<TAsyncQuery, TResult>, TQueryHandler>();
            return services;
        }

        public static IServiceCollection AddEvent<TEvent, TEventHandler>(this IServiceCollection services)
            where TEvent : class, IEvent
            where TEventHandler : class, IEventHandler<TEvent>
        {
            services
                .AddTransient<IEventHandler<TEvent>, TEventHandler>();
            return services;
        }
    }
}
