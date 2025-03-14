namespace UDP_Sample.Shared
{
    public interface IClientFactory
    {
        public IClient CreateClient(
            ClientType clientType,
            EncodingType encodingType);

    }
}
