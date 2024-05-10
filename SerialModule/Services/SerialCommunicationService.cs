using Reactive.Bindings;
using System;
using System.IO.Ports;
using System.Windows;

namespace SerialModule.Services
{
	public class SerialCommunicationService
	{
		public ReactivePropertySlim<SerialPort> Port { get; set; } = new();
		public ReactivePropertySlim<bool> IsConnected { get; set; } = new(false);

		public SerialCommunicationService() 
		{
			this.Port.Value = new SerialPort();
		}

		public void Connect(string portName, int baudrate, int dataBit, Parity parity, StopBits stopBits)
		{
			if (this.Port.Value.IsOpen)
			{
				MessageBox.Show("Connect Failed", "Error");
				return;
			}

			try
			{
				this.Port.Value.PortName = portName;
				this.Port.Value.BaudRate = baudrate;
				this.Port.Value.DataBits = dataBit;
				this.Port.Value.Parity = parity;
				this.Port.Value.StopBits = stopBits;

				this.Port.Value.Open();
				this.IsConnected.Value = true;
			}
			catch
			{
				MessageBox.Show("Connect Failed", "Error");
			}
		}

		public void Disconnect()
		{
			if (this.Port.Value.IsOpen is false)
			{
				MessageBox.Show("Disonnect Failed", "Error");
				return;
			}

			try
			{
				this.Port.Value.Close();
				this.IsConnected.Value = false;
			}
			catch
			{
				MessageBox.Show("Disonnect Failed", "Error");
			}
		}
	}
}
