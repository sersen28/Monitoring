using Prism.Ioc;
using Prism.Modularity;
using SerialModule.ViewModels;
using SerialModule.Views;

namespace SerialModule
{
	public class SerialCommunicationModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<SerialSettingView, SerialSettingViewModel>();
		}
	}
}
