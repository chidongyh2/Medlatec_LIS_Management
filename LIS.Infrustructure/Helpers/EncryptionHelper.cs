using LIS.Infrastructure.Constants;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Rijndael256;
using System;
using System.Security.Cryptography.X509Certificates;

namespace LIS.Infrastructure.Helpers
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string plainText, string password)
        {
            return RijndaelEtM.Encrypt(plainText, password, KeySize.Aes256);
        }

        public static string Decrypt(string cipherText, string password)
        {
            return RijndaelEtM.Decrypt(cipherText, password, KeySize.Aes256);
        }

        public static X509Certificate2 GenerateCertificate(string certName)
        {
            var keypairgen = new RsaKeyPairGenerator();
            var randomGenerator = new CustomCryptoApiRandomGenerator();
            var random = new SecureRandom(randomGenerator);
            keypairgen.Init(new KeyGenerationParameters(random, 1024));

            var keypair = keypairgen.GenerateKeyPair();

            var gen = new X509V3CertificateGenerator();

            var CN = new X509Name("CN=" + certName);
            var SN = Org.BouncyCastle.Math.BigInteger.ProbablePrime(120, new Random());

            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA512WITHRSA", keypair.Private, random);

            gen.SetSerialNumber(SN);
            gen.SetPublicKey(keypair.Private);
            gen.SetSubjectDN(CN);
            gen.SetIssuerDN(CN);
            gen.SetNotAfter(DateTime.MaxValue);
            gen.SetNotBefore(DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)));
            gen.SetPublicKey(keypair.Public);
            var newCert = gen.Generate(signatureFactory);

            return new X509Certificate2(newCert.GetEncoded());
        }
    }
}
