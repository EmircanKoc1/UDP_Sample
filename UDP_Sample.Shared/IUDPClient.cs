namespace UDP_Sample.Shared;

public interface IUDPClient : IClient
{
    bool Send(
        string host,
        int port,
        string message
        );

    bool Send(string message);

    bool InitializeAtPort(int port);
    UDPReceivedResult Receive();

    bool Connect(string host, int port);


}



