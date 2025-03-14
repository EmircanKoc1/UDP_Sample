namespace UDP_Sample.Shared
{
    public interface IEncodingServiceFactory
    {
        IEncodingService Create(EncodingType encodingType);
    }

}
