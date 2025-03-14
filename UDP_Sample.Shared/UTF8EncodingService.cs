using System.Text;

namespace UDP_Sample.Shared
{
    public class UTF8EncodingService : IEncodingService
    {
        public string Decode(byte[] data)
            => Encoding.UTF8.GetString(data);

        public byte[] Encode(string data)
            => Encoding.UTF8.GetBytes(data);
    }

}
