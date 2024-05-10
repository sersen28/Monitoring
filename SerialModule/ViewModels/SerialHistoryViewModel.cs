using Prism.Commands;
using Prism.Mvvm;
using SerialModule.Services;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace SerialModule.ViewModels
{
	public class SerialHistoryViewModel : BindableBase
	{
		private readonly SerialCommunicationService _serialCommunicationService;

		public SerialHistoryViewModel(SerialCommunicationService serialCommunicationService)
		{
			this._serialCommunicationService = serialCommunicationService;
		}
	}
}
