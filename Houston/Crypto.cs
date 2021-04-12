using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Houston
{
    public class Crypto
    {
        public static RSA CreateRSAKey() {
            return new RSACryptoServiceProvider(4096);
        }
        public static X509Certificate2 CreateSelfSignedRSA(string hostName, RSA rsa)
        {
            var cr = new CertificateRequest($"CN={hostName}", rsa, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
            //The export is here due to a persistent bug in ephemeral Private Keys. See -> https://github.com/dotnet/runtime/issues/23749#issuecomment-388231655
            return new X509Certificate2(cr.CreateSelfSigned(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(365)).Export(X509ContentType.Pkcs12));
        }

    }
}
