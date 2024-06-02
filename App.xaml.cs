using Splat;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;
using Black_Hole.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Black_Hole
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static IServiceProvider? ServiceProvider { get; private set; }

		public IConfiguration? Configuration { get; private set; }

		public App()
		{
			Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
		}

        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(MainWindow));
            serviceCollection.AddSingleton<IParticlesService, ParticlesService>();
            serviceCollection.AddSingleton<IParticlesHistoryService, ParticlesHistoryService>();
        }
	}
}
