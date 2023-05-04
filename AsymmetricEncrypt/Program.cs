using System;
using System.Security.Cryptography;
using System.Text;

namespace AsymmetricEncryptionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a new RSA key pair
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // Get the public and private key
                    string publicKeyXml = rsa.ToXmlString(false);
                    string privateKeyXml = rsa.ToXmlString(true);

                    Console.WriteLine("publicKey "+publicKeyXml);
                    Console.WriteLine("privateKey " + privateKeyXml);

                    // Encrypt a message using the public key
                    byte[] plaintext = Encoding.UTF8.GetBytes("Hello, world!");
                    byte[] ciphertext = Encrypt(Encoding.UTF8.GetBytes(publicKeyXml), plaintext);
                    Console.WriteLine("Encrypted message: " + Convert.ToBase64String(ciphertext));

                    // Decrypt the message using the private key
                    byte[] decrypted = Decrypt(Encoding.UTF8.GetBytes(privateKeyXml), ciphertext);
                    Console.WriteLine("Decrypted message: " + Encoding.UTF8.GetString(decrypted));
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static byte[] Encrypt(byte[] publicKeyBytes, byte[] plaintext)
        {
            // Create a new instance of the RSACryptoServiceProvider class using the public key
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(Encoding.UTF8.GetString(publicKeyBytes));
                return rsa.Encrypt(plaintext, true);
            }
        }

        static byte[] Decrypt(byte[] privateKeyBytes, byte[] ciphertext)
        {
            // Create a new instance of the RSACryptoServiceProvider class using the private key
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(Encoding.UTF8.GetString(privateKeyBytes));
                return rsa.Decrypt(ciphertext, true);
            }
        }
    }
}
