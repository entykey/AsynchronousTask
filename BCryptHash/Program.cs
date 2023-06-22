using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace HashingAlgorithmsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Enter the password you want to hash
                string password = "myPassword123@#Abcd";

                // Generate the hashes
                string md5Hash = HashPasswordWithMD5(password);
                string argon2Hash = HashPasswordWithArgon2(password);
                string bcryptHash = HashPasswordWithBCrypt(password);
                string sha256Hash = HashPasswordWithSHA256(password);
                string sha256HashSalt = HashPasswordWithSHA256WithSalt(password);
                string identityHash = HashPasswordWithIdentity(password);

                // Print the hashes
                Console.WriteLine($"Original password: {password}");
                Console.WriteLine($"MD5 Hash: {md5Hash}");
                Console.WriteLine($"Argon2 Hash: {argon2Hash}");
                Console.WriteLine($"BCrypt Hash: {bcryptHash}");
                Console.WriteLine($"SHA256 Hash: {sha256Hash}");
                Console.WriteLine($"SHA256 Salt Hash: {sha256HashSalt}");
                Console.WriteLine($"Identity Hash: {identityHash}");

                bool isMatch = false;
                Stopwatch stopwatch = new Stopwatch();

                while (!isMatch)
                {
                    // Prompt the user to enter a password to check
                    Console.Write("Enter a password to check: ");
                    string userInput = Console.ReadLine();

                    // Verify the passwords and measure the time taken
                    stopwatch.Restart();
                    bool isMD5Match = VerifyPasswordWithMD5(userInput, md5Hash);
                    stopwatch.Stop();
                    long md5Time = stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    bool isArgon2Match = VerifyPasswordWithArgon2(userInput, argon2Hash);
                    stopwatch.Stop();
                    long argon2Time = stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    bool isBCryptMatch = VerifyPasswordWithBCrypt(userInput, bcryptHash);
                    stopwatch.Stop();
                    long bcryptTime = stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    bool isSHA256Match = VerifyPasswordWithSHA256(userInput, sha256Hash);
                    stopwatch.Stop();
                    long sha256Time = stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    bool isIdentityMatch = VerifyPasswordWithIdentity(userInput, identityHash);
                    stopwatch.Stop();
                    long identityTime = stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    bool isSHA256SaltMatch = VerifyPasswordWithSHA256WithSalt(userInput, sha256HashSalt);
                    stopwatch.Stop();
                    long sha256SaltTime = stopwatch.ElapsedMilliseconds;

                    // Print the results
                    Console.WriteLine($"MD5 Verification: {(isMD5Match ? "Match" : "Mismatch")}".PadRight(35) + $"- Time: {md5Time} milliseconds");
                    Console.WriteLine($"Argon2 Verification: {(isArgon2Match ? "Match" : "Mismatch")}".PadRight(35) + $"- Time: {argon2Time} milliseconds");
                    Console.WriteLine($"BCrypt Verification: {(isBCryptMatch ? "Match" : "Mismatch")}".PadRight(35) + $"- Time: {bcryptTime} milliseconds");
                    Console.WriteLine($"SHA256 Verification: {(isSHA256Match ? "Match" : "Mismatch")}".PadRight(35) + $"- Time: {sha256Time} milliseconds");
                    Console.WriteLine($"Identity Verification: {(isIdentityMatch ? "Match" : "Mismatch")}".PadRight(35) + $"- Time: {identityTime} milliseconds");
                    Console.WriteLine($"sha256Salt Verification: {(isSHA256SaltMatch ? "Match" : "Mismatch")}".PadRight(35) + $"- Time: {sha256SaltTime} milliseconds\n\n");

                    if (!isArgon2Match || !isBCryptMatch || !isSHA256Match || !isIdentityMatch || !isSHA256SaltMatch)
                    {
                        Console.WriteLine("Password mismatch. Please try again.");
                    }
                    else
                    {
                        isMatch = true;
                    }
                }

                Console.WriteLine("Press Enter to continue or 'q' to quit.");
                string input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
                Console.WriteLine();
            }
        }


        static string HashPasswordWithMD5(string password)
        {
            byte[] salt = GenerateRandomSalt(); // Generate a random salt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPasswordBytes, passwordBytes.Length, salt.Length);

            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        static bool VerifyPasswordWithMD5(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            byte[] salt = new byte[hashBytes.Length - 16];
            Buffer.BlockCopy(hashBytes, 16, salt, 0, salt.Length);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPasswordBytes, passwordBytes.Length, salt.Length);

            using (var md5 = MD5.Create())
            {
                byte[] hashToCheck = md5.ComputeHash(saltedPasswordBytes);
                bool isMatch = hashBytes.AsSpan().SequenceEqual(hashToCheck);
                return isMatch;
            }
        }

        // for md5 salt:
        static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        static byte[] GetSaltFromHashedPassword(string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[hashBytes.Length - 16];
            Buffer.BlockCopy(hashBytes, 16, salt, 0, salt.Length);
            return salt;
        }


        static string HashPasswordWithArgon2(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = new byte[16]; // Generate a random salt here
            argon2.Iterations = 4; // Set the number of iterations here
            argon2.MemorySize = 1024; // Set the memory size in KiB here
            argon2.DegreeOfParallelism = 4; // Set the degree of parallelism here

            byte[] hash = argon2.GetBytes(32); // Generate a 32-byte hash

            return Convert.ToBase64String(hash);
        }

        static bool VerifyPasswordWithArgon2(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = new byte[16]; // Retrieve the salt used during hashing
            argon2.Iterations = 4; // Retrieve the number of iterations used during hashing
            argon2.MemorySize = 1024; // Retrieve the memory size used during hashing
            argon2.DegreeOfParallelism = 4; // Retrieve the degree of parallelism used during hashing

            byte[] hashToCheck = argon2.GetBytes(32); // Generate a 32-byte hash

            bool isMatch = hashBytes.AsSpan().SequenceEqual(hashToCheck);
            return isMatch;
        }

        static string HashPasswordWithBCrypt(string password)
        {
            var hasher = new PasswordHasher<object>();
            return hasher.HashPassword(null, password);
        }

        static bool VerifyPasswordWithBCrypt(string password, string hashedPassword)
        {
            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null, hashedPassword, password);
            bool isMatch = result == PasswordVerificationResult.Success;
            return isMatch;
        }

        static string HashPasswordWithSHA256(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }


        static bool VerifyPasswordWithSHA256(string password, string hashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashToCheck = Convert.ToBase64String(hashBytes);

                bool isMatch = hashedPassword == hashToCheck;
                return isMatch;
            }
        }

        static string HashPasswordWithIdentity(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            string hashedPassword = passwordHasher.HashPassword(null, password);
            return hashedPassword;
        }

        static bool VerifyPasswordWithIdentity(string password, string hashedPassword)
        {
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            bool isMatch = result == PasswordVerificationResult.Success;

            return isMatch;
        }

        static string HashPasswordWithSHA256WithSalt(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPasswordBytes, passwordBytes.Length, salt.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);

                // Concatenate the salt and hash bytes
                byte[] saltedHashBytes = new byte[salt.Length + hashBytes.Length];
                Buffer.BlockCopy(salt, 0, saltedHashBytes, 0, salt.Length);
                Buffer.BlockCopy(hashBytes, 0, saltedHashBytes, salt.Length, hashBytes.Length);

                return Convert.ToBase64String(saltedHashBytes);
            }
        }

        // for sha256 salt
        static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        static bool VerifyPasswordWithSHA256WithSalt(string password, string hashedPassword)
        {
            byte[] saltedHashBytes = Convert.FromBase64String(hashedPassword);

            // Retrieve the salt from the stored hashed password
            byte[] salt = new byte[16];
            Buffer.BlockCopy(saltedHashBytes, 0, salt, 0, salt.Length);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPasswordBytes, passwordBytes.Length, salt.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashToCheck = sha256.ComputeHash(saltedPasswordBytes);

                // Concatenate the salt and hash bytes for verification
                byte[] saltedHashToCheckBytes = new byte[salt.Length + hashToCheck.Length];
                Buffer.BlockCopy(salt, 0, saltedHashToCheckBytes, 0, salt.Length);
                Buffer.BlockCopy(hashToCheck, 0, saltedHashToCheckBytes, salt.Length, hashToCheck.Length);

                bool isMatch = saltedHashBytes.AsSpan().SequenceEqual(saltedHashToCheckBytes);
                return isMatch;
            }

        }
    }
}






/*
// argon2 : around 5ms to verify password
using System;
using System.Diagnostics;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Identity;


namespace BCryptConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter the password you want to hash
            string password = "myPassword123";

            // Generate the hash
            string hashedPassword = HashPassword(password);

            // Print the hashed password
            Console.WriteLine($"Hashed password: {hashedPassword}");

            bool isMatch = false;

            while (!isMatch)
            {
                // Prompt the user to enter a password to check
                Console.Write("Enter a password to check: ");
                string userInput = Console.ReadLine();

                // Verify the password and measure the time taken
                Stopwatch stopwatch = Stopwatch.StartNew();
                isMatch = VerifyPassword(userInput, hashedPassword);
                stopwatch.Stop();

                // Print the results
                Console.WriteLine($"Password verification: {(isMatch ? "Match" : "Mismatch")}");
                Console.WriteLine($"Time taken to verify: {stopwatch.ElapsedMilliseconds} milliseconds");

                if (!isMatch)
                {
                    Console.WriteLine("Password mismatch. Please try again.");
                }
            }

            Console.ReadLine();
        }

        static string HashPassword(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = new byte[16]; // Generate a random salt here
            argon2.Iterations = 4; // Set the number of iterations here
            argon2.MemorySize = 1024; // Set the memory size in KiB here
            argon2.DegreeOfParallelism = 4; // Set the degree of parallelism here

            byte[] hash = argon2.GetBytes(32); // Generate a 32-byte hash

            return Convert.ToBase64String(hash);
        }

        static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = new byte[16]; // Retrieve the salt used during hashing
            argon2.Iterations = 4; // Retrieve the number of iterations used during hashing
            argon2.MemorySize = 1024; // Retrieve the memory size used during hashing
            argon2.DegreeOfParallelism = 4; // Retrieve the degree of parallelism used during hashing

            byte[] hashToCheck = argon2.GetBytes(32); // Generate a 32-byte hash

            bool isMatch = hashBytes.SequenceEqual(hashToCheck);
            return isMatch;
        }
    }
}
*/


/*
// around 83ms to verify password
 
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace BCryptConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter the password you want to hash
            string password = "myPassword123";

            // Generate the hash
            string hashedPassword = HashPassword(password);

            // Print the hashed password
            Console.WriteLine($"Hashed password: {hashedPassword}");

            bool isMatch = false;

            while (!isMatch)
            {
                // Prompt the user to enter a password to check
                Console.Write("Enter a password to check: ");
                string userInput = Console.ReadLine();

                // Verify the password and measure the time taken
                Stopwatch stopwatch = Stopwatch.StartNew();
                isMatch = VerifyPassword(userInput, hashedPassword);
                stopwatch.Stop();

                // Print the results
                Console.WriteLine($"Password verification: {(isMatch ? "Match" : "Mismatch")}");
                Console.WriteLine($"Time taken to verify: {stopwatch.ElapsedMilliseconds} milliseconds");

                if (!isMatch)
                {
                    Console.WriteLine("Password mismatch. Please try again.");
                }
            }

            Console.ReadLine();
        }

        static string HashPassword(string password)
        {
            var hasher = new PasswordHasher<object>();
            return hasher.HashPassword(null, password);
        }

        static bool VerifyPassword(string password, string hashedPassword)
        {
            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}

*/