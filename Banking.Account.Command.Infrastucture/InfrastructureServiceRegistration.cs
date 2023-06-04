
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Account.Command.Aplication.Contracts.Persistence;
using Banking.Account.Command.Infrastucture.kafkaEvents;
using Banking.Account.Command.Infrastucture.Repositories;
using Banking.Cqrs.Core.Events;
using Banking.Cqrs.Core.Handlers;
using Banking.Cqrs.Core.Infrastructure;
using Banking.Cqrs.Core.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Banking.Account.Command.Infrastucture
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config) 
        {
            BsonClassMap.RegisterClassMap<BaseEvent>();
            BsonClassMap.RegisterClassMap<AccountOpenedEvent>();
            BsonClassMap.RegisterClassMap<AccountCloseEvent>();
            BsonClassMap.RegisterClassMap<FundsDepositedEvent>();
            BsonClassMap.RegisterClassMap<FundsWithdrawnEvent>();

            services.AddScoped(typeof(IMongoRepository<>), typeof(IMongoRepository<>));
            services.AddScoped<EventProducer, AccountEventProducer>();
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
            services.AddTransient<EventStore, AccountEventStore>();
            services.AddTransient<EventSourcingHandler<AccountAggregate>, AccountEventSourcingHandler>();

            return services;
        }
    }
}
