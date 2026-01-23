using iText.Signatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Security
{
    public class X509Certificate2Signature : IExternalSignature
    {
        private readonly X509Certificate2 _certificate;
        private readonly string _hashAlgorithm;

        public X509Certificate2Signature(X509Certificate2 certificate, string hashAlgorithm)
        {
            _certificate = certificate;
            _hashAlgorithm = hashAlgorithm;
        }

        public string GetDigestAlgorithmName() => _hashAlgorithm;

        public string GetSignatureAlgorithmName()
        {
            string keyAlg = _certificate.PublicKey.Oid.FriendlyName;
            if (keyAlg.Contains("RSA")) return "RSA";
            if (keyAlg.Contains("ECDsa")) return "ECDsa";
            return keyAlg;
        }

        public ISignatureMechanismParams GetSignatureMechanismParameters() => null;

        public byte[] Sign(byte[] message)
        {
            using (RSA rsa = _certificate.GetRSAPrivateKey())
            {
                if (rsa != null)
                {
                    return rsa.SignData(message, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }

            using (ECDsa ecdsa = _certificate.GetECDsaPrivateKey())
            {
                if (ecdsa != null)
                {
                    return ecdsa.SignData(message, HashAlgorithmName.SHA256);
                }
            }

            throw new NotSupportedException("Thuật toán mã hóa không được hỗ trợ.");
        }
    }
}
