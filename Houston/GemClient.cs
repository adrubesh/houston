using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Security;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Houston
{
    public class GemClient : TcpClient
    {
        private const int GEMINI_PORT = 1965;

        private SslStream tlsStream = null;

        public RemoteCertificateValidationCallback AcceptAll = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => { return true; };

        public class TLSUpgradeFailException : Exception
        {
            public TLSUpgradeFailException() { }
            public TLSUpgradeFailException(string message) : base(message) { }
            public TLSUpgradeFailException(string message, Exception inner) : base(message, inner) { }
        }

        public GemClient(IPEndPoint endPoint) : base(endPoint)
        {
            if (Connected)
            {

            }
        }

        public GemClient(TcpClient client)
        {
            this.Client = client.Client;
        }

        public GemClient(string host, Int32 port) : base(host, port)
        {

        }
        public GemClient()
        {

        }


        public static async Task<string> DownloadStringAsync(string URL)
        {
            return await new Task<string>(() => "");
        }

        private GemClient CreateDefaultClient(IPEndPoint endPoint)
        {
            return new GemClient(endPoint);
        }

        protected internal void UpgradeStream(RemoteCertificateValidationCallback certificateValidationCallback,
         bool leaveOpen = false)
        {
            if (tlsStream != null)
            {
                tlsStream.Dispose();
            }

            tlsStream = new SslStream(this.GetStream(), leaveOpen, certificateValidationCallback);
        }

        protected internal void AuthenicateClient(X509Certificate2 certificate2, bool clientCertRequired = false,
            SslProtocols tlsVersion = SslProtocols.Tls13, bool checkRevocation = true)
        {
            if (certificate2 == null) return;
            if (tlsStream == null) return;

            try
            {
                tlsStream.AuthenticateAsServer(certificate2, clientCertRequired, tlsVersion, checkRevocation);
            }
            catch (AuthenticationException ex)
            {
                throw new TLSUpgradeFailException("TLS authentication failed", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new TLSUpgradeFailException("Stream already authenticated", ex);
            }
        }

        public byte[] ReadAll()
        {
            var result = new List<byte>();
            byte[] buffer = new byte[1024];
            int read = -1;
            using (var stream = GetStream())
            {
                do
                {
                    read = stream.Read(buffer, 0, 1024);
                    result.AddRange(buffer);

                } while (read != 0);
            }

            return result.ToArray();
        }

    }
}
