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

		#region 현재 연결된 시리얼 포트 정보
		public ReadOnlyReactivePropertySlim<bool> IsConnected { get; set; }
		public ReadOnlyReactivePropertySlim<string?> CurrentPortName { get; set; }
		public ReadOnlyReactivePropertySlim<int> CurrentBaudrate { get; set; }
		public ReadOnlyReactivePropertySlim<int> CurrentDataBit { get; set; }
		public ReadOnlyReactivePropertySlim<Parity> CurrentParityBit { get; set; }
		public ReadOnlyReactivePropertySlim<StopBits> CurrentStopBit { get; set; }
		#endregion

		#region 유저가 설정한 시리얼 포트 정보
		public ReactiveProperty<string> PortName { get; set; } = new(string.Empty);
		public ReactiveProperty<int> Baudrate { get; set; } = new();
		public ReactiveProperty<int> DataBit { get; set; } = new();
		public ReactiveProperty<Parity> ParityBit { get; set; } = new(Parity.None);
		public ReactiveProperty<StopBits> StopBit { get; set; } = new(StopBits.None);
		#endregion

		public ReactiveCollection<string> SerialPortCollection { get; set; } = new();
		public ReactiveCommand<bool> ButtonCommand { get; set; } = new();

		public SerialSettingViewModel(SerialCommunicationService serialCommunicationService)
		{
			this._serialCommunicationService = serialCommunicationService;

			this.IsConnected = this._serialCommunicationService.IsConnected.ToReadOnlyReactivePropertySlim();
			this.CurrentPortName = this._serialCommunicationService.CurrentPortName.ToReadOnlyReactivePropertySlim();
			this.CurrentBaudrate = this._serialCommunicationService.CurrentBaudrate.ToReadOnlyReactivePropertySlim();
			this.CurrentDataBit = this._serialCommunicationService.CurrentDataBit.ToReadOnlyReactivePropertySlim();
			this.CurrentParityBit = this._serialCommunicationService.CurrentParityBit.ToReadOnlyReactivePropertySlim();
			this.CurrentStopBit = this._serialCommunicationService.CurrentStopBit.ToReadOnlyReactivePropertySlim();

			this.SerialPortCollection.AddRangeOnScheduler(SerialPort.GetPortNames());
			this.ButtonCommand.Subscribe((isConnected) =>
			{
				if (this.IsConnected.Value)
				{
					this._serialCommunicationService.Disconnect();
				}
				else
				{
					this._serialCommunicationService.Connect(
							portName: this.PortName.Value,
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
