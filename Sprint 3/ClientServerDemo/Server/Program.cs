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
            string cerFile = Directory.GetCurrentDirectory() + "\\certificate.cer";
            string pfxFile = Directory.GetCurrentDirectory() + "\\certificate.pfx";

            if (!File.Exists(cerFile) && !File.Exists(pfxFile))
            {
                CreateCertificate("127.0.0.1");
            }

            server = new ServerListener(Directory.GetCurrentDirectory() + "\\certificate.pfx", "145.49.38.160", 25545);
            server.Start();

            Console.ReadKey();
        }

        private static void CreateCertificate(string cn)
        {
            Console.WriteLine("Creating certificate...");
            using (CryptContext ctx = new CryptContext())
            {
                ctx.Open();

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

                byte[] certFileRaw = cert.Export(X509ContentType.Pfx, "banaantje");
                string filePath = Directory.GetCurrentDirectory() + "\\certificate.pfx";

                File.WriteAllBytes(filePath, certFileRaw);

                File.WriteAllText(Directory.GetCurrentDirectory() + "\\certificate.cer",
                    "-----BEGIN CERTIFICATE-----\r\n"
                        + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                        + "\r\n-----END CERTIFICATE-----"
                );
                Console.WriteLine("Done");

                Console.WriteLine("Adding to store...");
                using (X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser))
                {
                    store.Open(OpenFlags.ReadWrite);
                    store.Add(cert);
                }
                Console.WriteLine("Done");
            }
        }
    }
}