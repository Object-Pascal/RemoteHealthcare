using Pluralsight.Crypto;
using Server.Listener;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Server
{
    class Program
    {
        private static ServerListener server;

        static void Main(string[] args)
        {
            server = new ServerListener(Directory.GetCurrentDirectory() + "\\certificate.pfx", "192.168.1.2", 25570);
            server.Start();

            Console.ReadKey();

            //CreateCertificate();
        }

        private static void CreateCertificate()
        {
            using (CryptContext ctx = new CryptContext())
            {
                ctx.Open();

                string cn = "192.168.1.2";

                X509Certificate2 cert = ctx.CreateSelfSignedCertificate(
                    new SelfSignedCertProperties
                    {
                        IsPrivateKeyExportable = true,
                        KeyBitLength = 4096,
                        Name = new X500DistinguishedName($"cn={cn}"),
                        ValidFrom = DateTime.Today.AddDays(-1),
                        ValidTo = DateTime.Today.AddYears(1),
                    }
                );

                byte[] certFileRaw = cert.Export(X509ContentType.Pfx, "bruh");
                string filePath = Directory.GetCurrentDirectory() + "\\certificate.pfx";

                File.WriteAllBytes(filePath, certFileRaw);

                File.WriteAllText(Directory.GetCurrentDirectory() + "\\certificate.cer",
                    "-----BEGIN CERTIFICATE-----\r\n"
                        + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                        + "\r\n-----END CERTIFICATE-----"
                );

                using (X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser))
                {
                    store.Open(OpenFlags.ReadWrite);
                    store.Add(cert);
                }
            }
        }
    }
}