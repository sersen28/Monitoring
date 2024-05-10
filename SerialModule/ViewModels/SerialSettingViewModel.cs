using Prism.Mvvm;
using Reactive.Bindings;
using SerialModule.Services;
using System;
using System.IO.Ports;

namespace SerialModule.ViewModels
{
	public class SerialSettingViewModel : BindableBase
	{
		private readonly SerialCommunicationService _serialCommunicationService;

		public ReadOnlyReactivePropertySlim<SerialPort?> Port { get; set; }
		public ReadOnlyReactivePropertySlim<bool> IsConnected { get; set; }

		public ReactiveProperty<string> ComPort { get; set; } = new(string.Empty);
		public ReactiveProperty<int> Baudrate { get; set; } = new();
		public ReactiveProperty<int> DataBit { get; set; } = new();
		public ReactiveProperty<Parity> ParityBit { get; set; } = new(Parity.None);
		public ReactiveProperty<StopBits> StopBit { get; set; } = new(StopBits.None);

		public ReactiveCommand<bool> ButtonCommand { get; set; } = new();
		public ReactiveCollection<string> PortName { get; set; } = new();

		public SerialSettingViewModel(SerialCommunicationService serialCommunicationService)
		{
			this._serialCommunicationService = serialCommunicationService;

			this.Port = this._serialCommunicationService.Port.ToReadOnlyReactivePropertySlim();
			this.IsConnected = this._serialCommunicationService.IsConnected.ToReadOnlyReactivePropertySlim();

			this.PortName.AddRangeOnScheduler(SerialPort.GetPortNames());
			this.ButtonCommand.Subscribe((isConnected) =>
			{
				if (this.IsConnected.Value)
				{
					this._serialCommunicationService.Disconnect();
				}
				else
				{
					this._serialCommunicationService.Connect(
							portName: this.ComPort.Value,
							baudrate: this.Baudrate.Value,
							dataBit: this.DataBit.Value,
							parity: this.ParityBit.Value,
							stopBits: this.StopBit.Value
						);
				}
			});
		}
	}
}
