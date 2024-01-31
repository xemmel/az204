using Microsoft.Extensions.DependencyInjection;

public static class DIFactory
{
    public static IServiceCollection AddFunctionServices(this IServiceCollection services)
    {
        services
            .AddScoped<IGreetingHandler,AngryGreetingHandler>()
            .AddScoped<ICalendarHandler,CalendarHandler>()
            ;
        return services;
    }
}