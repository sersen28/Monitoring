using Reactive.Bindings;
using SerialModule.Utility;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows;

namespace SerialModule.Services
{
	public class SerialCommunicationService
	{
		public const int HistoryLength = 100;

		private SerialPort _serialPort = new();

		public ReactivePropertySlim<bool> IsConnected { get; set; } = new(false);
		public ReactiveCollection<string> History { get; set; } = new();

		public ReactiveProperty<string> CurrentPortName { get; set; } = new(string.Empty);
		public ReactiveProperty<int> CurrentBaudrate { get; set; } = new();
		public ReactiveProperty<int> CurrentDataBit { get; set; } = new();
		public ReactiveProperty<Parity> CurrentParityBit { get; set; } = new(Parity.None);
		public ReactiveProperty<StopBits> CurrentStopBit { get; set; } = new(StopBits.None);

		public SerialCommunicationService()
		{
			this._serialPort.DataReceived += DataReceived;
			History.AddOnScheduler("Test");
			History.AddOnScheduler("Test");
			History.AddOnScheduler("Test");
			History.AddOnScheduler("Test");
			History.AddOnScheduler("Test");
		}

		private void DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			var buffer = new byte[_serialPort.BytesToRead];			
			this._serialPort.Read(buffer, 0, _serialPort.BytesToRead);

			var record = SerialUtility.ByteArrayToString(buffer, true);

			History.AddRangeOnScheduler(record);
			Debug.WriteLine(record);
		}

		public void Connect(string portName, int baudrate, int dataBit, Parity parity, StopBits stopBits)
		{
			if (this._serialPort.IsOpen)
			{
				MessageBox.Show("Connect Failed", "Error");
				return;
			}

			try
			{
				this._serialPort.PortName = portName;
				this._serialPort.BaudRate = baudrate;
				this._serialPort.DataBits = dataBit;
				this._serialPort.Parity = parity;
				this._serialPort.StopBits = stopBits;
				this._serialPort.Open();

				this.CurrentPortName.Value = portName;
				this.CurrentBaudrate.Value = baudrate;
				this.CurrentDataBit.Value = dataBit;
				this.CurrentParityBit.Value = parity;
				this.CurrentStopBit.Value = stopBits;

				this.IsConnected.Value = true;
			}
			catch
			{
				MessageBox.Show("Connect Failed", "Error");
			}
		}

		public void Disconnect()
		{
			if (this._serialPort.IsOpen is false)
			{
				MessageBox.Show("Disonnect Failed", "Error");
				return;
			}

			try
			{
				this._serialPort.Close();
				this.IsConnected.Value = false;
			}
			catch
			{
				MessageBox.Show("Disonnect Failed", "Error");
			}
		}
	}
}
