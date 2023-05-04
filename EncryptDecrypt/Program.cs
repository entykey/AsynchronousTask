using System.IO;
using System.Security.Cryptography;
using System.Text;

// https://www.geeksforgeeks.org/encrypt-decrypt-using-rijndael-key-in-c-sharp/?ref=rp
// As for C#, the Rijndael key supports key lengths of 128, 192, and 256 bits
// and also supports blocks of 128 (by default), 192, and 256 bits.
// Rijndael key is very much similar to AES(Advance Encryption Standard). 

public class GFGEncryption
{

    static public void Main()
    {
        string encryptedString = encodeString();
        string decryptedString = decodeString(encryptedString);
        Console.Write("Encoded String is: " + encryptedString);
        Console.Write("\nDecoded String is: " + decryptedString);
    }

    public static string encodeString()
    {
        string data = "GeeksForGeeks Text";
        string answer = "";
        string publicKey = "GEEK1234";  // 64 bits is the only valid key size for DESCryptoServiceProvider encryption algorithm!! ASCII encoding uses 8 bits per character, therefore 8 ASCII characters == 64 bits.
        string privateKey = "PKEY4321";
        byte[] privateKeyBytes = { };
        privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
        byte[] publicKeyBytes = { };
        publicKeyBytes = Encoding.UTF8.GetBytes(publicKey);
        byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(data);
        using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
        {
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream,
            provider.CreateEncryptor(publicKeyBytes, privateKeyBytes),
            CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
            cryptoStream.FlushFinalBlock();
            answer = Convert.ToBase64String(memoryStream.ToArray());
        }
        return answer;
    }

    public static string decodeString(String data)
    {
        string answer = "";
        string publicKey = "GEEK1234";
        string privateKey = "PKEY4321";
        byte[] privateKeyBytes = { };
        privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
        byte[] publicKeyBytes = { };
        publicKeyBytes = Encoding.UTF8.GetBytes(publicKey);
        byte[] inputByteArray = new byte[data.Replace(" ", "+").Length];
        inputByteArray = Convert.FromBase64String(data.Replace(" ", "+"));
        using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
        {
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream,
            provider.CreateDecryptor(publicKeyBytes, privateKeyBytes),
            CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
            cryptoStream.FlushFinalBlock();
            answer = Encoding.UTF8.GetString(memoryStream.ToArray());
        }
        return answer;
    }
}
