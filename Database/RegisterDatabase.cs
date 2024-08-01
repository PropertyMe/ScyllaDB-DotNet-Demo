using Cassandra.Mapping;
using ScyllaDBDemo.Services;

namespace ScyllaDBDemo.Database;

public static class RegisterServices
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection services)
    {
        // configure model mappings for Datastax
        MappingConfiguration.Global.Define<ModelConfiguration>();

        services.AddSingleton<IScyllaDb, ScyllaConnect>();
        
        services.RegisterScoped().AddTransient<IMigrationService, MigrationService>();

        return services;
    }
}