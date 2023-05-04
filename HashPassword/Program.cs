using System.Security.Cryptography;
using System.Text;

public class Example
{
    // Asynchronous Main (since vstudio 2017):
    public static async Task Main(string[] args)
    {
        string pass = "1234";
        string hashedpass = HashPass(pass);
        Console.Write(hashedpass);
    }
    internal static string HashPass(string text)
    {
        MD5 md5 = MD5.Create();
        byte[] temp = Encoding.ASCII.GetBytes(text);
        byte[] hashData = md5.ComputeHash(temp);
        string hashPass = "";
        foreach (var item in hashData)
        {
            hashPass += item.ToString("x2");
        }
        return hashPass;
    }
}
        