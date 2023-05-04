using System.Security.Cryptography;
using System.Text;
using System.Web;

class Program
{
    private static readonly string _secretKey = "macbookpro";

    //public static UrlSignatureGenerator(string secretKey)
    //{
    //    _secretKey = secretKey;
    //}

    public static string GenerateSignature(string url)
    {
        var urlBytes = Encoding.UTF8.GetBytes(url);
        var keyBytes = Encoding.UTF8.GetBytes(_secretKey);

        using (var hmacsha256 = new HMACSHA256(keyBytes))
        {
            var hash = hmacsha256.ComputeHash(urlBytes);
            var signature = Convert.ToBase64String(hash);

            // Replace any unsafe characters in the signature
            signature = signature.Replace("+", "-").Replace("/", "_").Replace("=", "");

            return signature;
        }
    }

    public static bool VerifySignature(string url)   // (string url, string secretKey)
    {
        Uri uri = new Uri(url);
        string signature = HttpUtility.ParseQueryString(uri.Query).Get("signature");
        string signatureBaseString = uri.GetLeftPart(UriPartial.Path) + "?" + uri.Query.Replace("&signature=" + signature, "");

        byte[] keyBytes = Encoding.UTF8.GetBytes("macbookpro");
        byte[] signatureBytes = Encoding.UTF8.GetBytes(signatureBaseString);
        using (var hmac = new HMACSHA256(keyBytes))
        {
            byte[] hash = hmac.ComputeHash(signatureBytes);
            string computedSignature = Convert.ToBase64String(hash);
            return signature == computedSignature;
        }
    }

    static void Main(string[] args)
    {
        var hashedSignature = GenerateSignature("https://myapi.net");

        Console.WriteLine(hashedSignature);

        var validateSignature = VerifySignature("https://myapi.net", "macbookpro");

        Console.WriteLine(validateSignature);
    }
}
