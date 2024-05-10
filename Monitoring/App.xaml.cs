using Monitoring.Shell;
using Prism.Ioc;
using Prism.Modularity;
using SerialModule;
using SerialModule.Services;
using System.Windows;

namespace Monitoring
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainShell>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<SerialCommunicationService>();
		}

		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			moduleCatalog.AddModule<SerialCommunicationModule>();
		}
	}
}
