using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Houston
{
    public class GemServer : TcpListener
    {
        public class ClientConnectEventArgs : EventArgs {
            public GemClient client { get; set; }
        }

        private string hostName;
        private X509Certificate2 cert;
        private static readonly int GEMINI_PORT = 1965;

        public event EventHandler<ClientConnectEventArgs> ClientConnect;
        protected virtual void OnClientConnect(ClientConnectEventArgs e) {
            EventHandler<ClientConnectEventArgs> handler = ClientConnect;
            if (handler != null) {
                handler(this, e);
            }
        }

        public GemServer() : base(IPAddress.Parse("0.0.0.0"), GEMINI_PORT)
        {
            this.hostName = "houston.gemini.dev";
            var key = Crypto.CreateRSAKey();
            this.cert = Crypto.CreateSelfSignedRSA(this.hostName, key);

            StartServer();
        }


        public void AcceptTLSClient() {
            var gc = new GemClient(this.AcceptTcpClient());
            gc.AuthenicateClient(this.cert, false, System.Security.Authentication.SslProtocols.Tls13, true); 
            OnClientConnect(e: new ClientConnectEventArgs { client = gc });
        }

        public void UpgradeClientStream() {

        }

        public void StartServer() {
            this.Start();
            while (true) {
                AcceptTLSClient();

                
            }
        }

    }
}
