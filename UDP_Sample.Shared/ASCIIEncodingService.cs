using System.Text;

namespace UDP_Sample.Shared
{
    public class ASCIIEncodingService : IEncodingService
    {
        public string Decode(byte[] data)
            => Encoding.ASCII.GetString(data);

        public byte[] Encode(string data)
            => Encoding.ASCII.GetBytes(data);
    }

}
