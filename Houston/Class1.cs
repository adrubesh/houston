using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Houston 
{
    public class Class1
    {
        public async Task<IPAddress> GetAddress(string host)
        {
            var hostEntries = await Dns.GetHostEntryAsync(host);
            return hostEntries.AddressList[0];
        } 

        
        public string PrintsTest()
        {
            return "Test";
        }
    }
}
