using System;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Houston;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HoustonClient
{
    class Program
    {
        static void Main(string[] args)
        {
          var key = Houston.Crypto.CreateRSAKey();
          var cert = Houston.Crypto.CreateSelfSignedRSA("myhost.gemini", key); 
          var gs = new GemServer();
        }
    }
}
