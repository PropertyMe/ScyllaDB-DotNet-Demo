using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Services;

public static class RegisterServices
{
    public static IServiceCollection RegisterScoped(this IServiceCollection services)
    {
        services.AddScoped<IContactServices, ContactServices>();
        services.AddScoped<IMeasurementsRepository, MeasurementsRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IOwnersRepository, OwnersRepository>();
        services.AddScoped<ISensorSimulator, SensorSimulator>();

        return services;
    }
}