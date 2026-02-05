using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SteamKit.Factory;

namespace SteamKit.Internal
{
    internal class ServiceManagement
    {
        private readonly IServiceCollection serviceCollection;

        public ServiceManagement()
        {
            serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IAuthenticator, DefaultAuthenticator>();
            serviceCollection.AddSingleton<IHttpClientFactory, DefaultHttpClientFactory>();
            serviceCollection.AddSingleton<ILogger, DefaultLogger>();
            serviceCollection.AddSingleton<IServerProvider, DefaultServerProvider>();
            serviceCollection.AddSingleton<ISocketProvider, DefaultSocketProvider>();
        }

        public IServiceCollection Replace<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            return serviceCollection.Replace(ServiceDescriptor.Singleton<TService, TImplementation>());
        }

        public IServiceCollection Replace<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            return serviceCollection.Replace(ServiceDescriptor.Singleton(implementationFactory));
        }

        public IServiceCollection Replace<TService>(TService implementationInstance) where TService : class
        {
            return serviceCollection.Replace(ServiceDescriptor.Singleton(implementationInstance));
        }

        public TService? GetService<TService>() where TService : class
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetService<TService>();
        }

        public TService GetRequiredService<TService>() where TService : class
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetRequiredService<TService>();
        }
    }
}
