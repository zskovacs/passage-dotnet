namespace Passage.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for the IServiceCollection interface to simplify the registration of Passage services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Passage service to the IServiceCollection and configures it with the specified options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <param name="configureOptions">An action to configure the Passage options.</param>
    /// <returns>The IHttpClientBuilder to further configure the Passage service.</returns>
    /// <exception cref="ArgumentNullException">Thrown if options or appId is null or empty.</exception>
    public static IHttpClientBuilder AddPassage(this IServiceCollection services, Action<IServiceProvider, PassageConfig> configureOptions)
    {
        services.AddOptions<PassageConfig>().Configure<IServiceProvider>((options, resolver) => configureOptions(resolver, options))
            .PostConfigure(options =>
            {
                if (string.IsNullOrWhiteSpace(options.AppId))
                {
                    throw new ArgumentNullException(nameof(options.AppId));
                }
            });

        services.TryAddSingleton<IPassage>(resolver => resolver.GetRequiredService<InjectablePassageClient>());

        return services.AddHttpClient<InjectablePassageClient>();
    }

    /// <summary>
    /// Adds the Passage service to the IServiceCollection and configures it with the specified options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <param name="configureOptions">An action to configure the Passage options.</param>
    /// <returns>The IHttpClientBuilder to further configure the Passage service.</returns>
    /// <exception cref="ArgumentNullException">Thrown if options or appId is null or empty.</exception>
    public static IHttpClientBuilder AddPassage(this IServiceCollection services, Action<PassageConfig> configureOptions)
    {
        return services.AddPassage((_, options) => configureOptions(options));
    }
}