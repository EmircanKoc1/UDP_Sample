using System.Net.Sockets;

namespace UDP_Sample.Shared
{
    public class ClientFactory : IClientFactory
    {
        private readonly IEncodingServiceFactory _encodingServiceFactory;

        public ClientFactory(IEncodingServiceFactory encodingServiceFactory)
        {
            _encodingServiceFactory = encodingServiceFactory ?? throw new ArgumentNullException(nameof(encodingServiceFactory));
        }

        public IClient CreateClient(ClientType clientType, EncodingType encodingType)
        {
            var encodingService = _encodingServiceFactory.Create(encodingType);

            IClient client = clientType switch
            {
                ClientType.UDP => new UDPClientAdapter(new UdpClient(), encodingService),
                _ => throw new NotSupportedException($"Unsupported ClientType: {clientType}")
            };

            return client;


        }
    }
}
