using System.Net;
using System.Net.Sockets;

namespace UDP_Sample.Shared;

public sealed class UDPClientAdapter : IUDPClient
{
    private readonly UdpClient _udpClient;
    private readonly IEncodingService _encodingService;
    public bool IsConnected { get; private set; }
    public bool IsInitialized { get; private set; }

    public UDPClientAdapter(
        UdpClient udpClient,
        IEncodingService encodingService)
    {
        _udpClient = udpClient ?? throw new ArgumentNullException($"{nameof(udpClient)} cannot be null !");
        _encodingService = encodingService ?? throw new ArgumentNullException($"{nameof(encodingService)} cannot be null !");
    }



    public bool Connect(string host, int port)
    {
        try
        {
            _udpClient.Connect(host, port);
            IsConnected = true;
        }
        catch (Exception)
        {
            return false;
        }
        return true;

    }

    public void Dispose()
    {
        IsConnected = false;
        _udpClient?.Dispose();
    }

    public UDPReceivedResult Receive()
    {
        var ipEndpoint = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            var receiveBytes = _udpClient.Receive(ref ipEndpoint);
            var message = _encodingService.Decode(receiveBytes);
            return new UDPReceivedResult(message, ipEndpoint.Address.ToString(), ipEndpoint.Port);
        }
        catch (Exception)
        {

            throw;
        }


    }

    public bool Send(string host, int port, string message)
    {
        if (IsConnected)
            throw new ArgumentException("If connected, shipments cannot be sent to a different address.");

        try
        {
            var sendBytes = EncodeMessage(message);
            _udpClient.Send(sendBytes, sendBytes.Length, host, port);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// Sends a message to the connected address.
    /// </summary>
    /// <param name="message">The message to be sent.</param>
    /// <returns>
    /// True if the message is sent successfully; otherwise, false.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the method is called without an active connection (i.e., if <see cref="IsConnected"/> is false).
    /// </exception>
    public bool Send(string message)
    {
        if (!IsConnected)
            throw new ArgumentException("You need to connect first in order to use this method.");

        try
        {
            var sendBytes = EncodeMessage(message);
            _udpClient.Send(sendBytes, sendBytes.Length);

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
    private byte[] EncodeMessage(string message) => _encodingService.Encode(message);

    public bool InitializeAtPort(int port)
    {
        try
        {
            _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
            IsInitialized = true;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
