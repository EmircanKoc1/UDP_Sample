using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Sample.Shared
{
    public interface IEncodingService
    {
        byte[] Encode(string data);
        string Decode(byte[] data); 

    }
}
