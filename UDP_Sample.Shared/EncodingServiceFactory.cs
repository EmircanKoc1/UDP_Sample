namespace UDP_Sample.Shared
{

    public class EncodingServiceFactory : IEncodingServiceFactory
    {
        public IEncodingService Create(EncodingType encodingType)
        {
            IEncodingService encodingService = encodingType switch
            {
                EncodingType.UTF8 => new UTF8EncodingService(),
                EncodingType.ASCII => new ASCIIEncodingService(),
                _ => throw new NotSupportedException($"Unsupported EncodingType: {encodingType}")
            };

            return encodingService;

        }
    }

}
