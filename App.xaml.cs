using Splat;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;
using Black_Hole.MVVM.Models;
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

        public Uri CurrentTheme => Resources.MergedDictionaries[0].MergedDictionaries[0].Source;

        public Uri CurrentLanguage => Resources.MergedDictionaries[1].MergedDictionaries[0].Source;

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

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(MainWindow));
            serviceCollection.AddSingleton<IParticlesService, ParticlesService>();
            serviceCollection.AddSingleton<IParticlesHistoryService, ParticlesHistoryService>();
            serviceCollection.AddSingleton<ISimulationService, SimulationService>();
        }

        public void ChangeTheme(Uri uri)
        {
            var themeDictionary = Resources.MergedDictionaries[0];
            themeDictionary.MergedDictionaries.Clear();
            themeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }

        public void ChangeLanguage(Uri uri)
        {
            var themeDictionary = Resources.MergedDictionaries[1];
            themeDictionary.MergedDictionaries.Clear();
            themeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }
    }
}
