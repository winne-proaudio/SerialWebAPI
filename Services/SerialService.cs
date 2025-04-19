using System.IO.Ports;

namespace SerialWebAPI.Services;

public class SerialService : IDisposable
{
    private readonly SerialPort _serialPort;

    public SerialService(IConfiguration config)
    {
        var portName = config["Serial:PortName"] ?? "/dev/tty.usbserial-FTVVAR61";
        var baudRate = int.Parse(config["Serial:BaudRate"] ?? "9600");
        _serialPort = new SerialPort(portName, baudRate);
        _serialPort.Open();
    }

    public void SendCommand(string command)
    {
        if (_serialPort.IsOpen)
        {
           Console.WriteLine(command);
            _serialPort.WriteLine(command);
        }
    }

    public void Dispose()
    {
        _serialPort?.Close();
        _serialPort?.Dispose();
    }
}