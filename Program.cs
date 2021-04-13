using System;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length % 2 == 0 || args.Length < 3)
            {
                Console.WriteLine("Enter an odd number of arguments, please");
                Environment.Exit(0);
            }
            string SecretKey = CreateSecureKey();
            Random r = new Random();
            int PCChoice = r.Next(0, args.Length - 1);
            Console.WriteLine($"HMAC: {HMACHash(args[PCChoice], SecretKey)}");

            Console.WriteLine("Available moves:");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine((i + 1) + " - " + args[i]);
            }
            Console.Write("Enter your move: ");
            int UserChoice = Int32.Parse(Console.ReadLine()) - 1;
            
            Console.WriteLine($"Your move: {args[UserChoice]}");
            Console.WriteLine("Computer move: " + args[PCChoice]);

            if (UserChoice == PCChoice)
            {
                Console.WriteLine("Draw");
            }
            else if (PCChoice >= ((UserChoice - args.Length / 2 >= 0) ? (UserChoice - args.Length / 2) : (UserChoice - args.Length / 2 + args.Length))
                && (PCChoice < ((UserChoice == 0) ? args.Length : UserChoice)))
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
            Console.WriteLine($"HMAC key: {SecretKey}");
        }

        static string HMACHash(string str, string key)
        {
            byte[] bkey = Encoding.Default.GetBytes(key);
            using (var hmac = new HMACSHA256(bkey))
            {
                byte[] bstr = Encoding.Default.GetBytes(str);
                byte[] bhash = hmac.ComputeHash(bstr);
                return BitConverter.ToString(bhash).Replace("-", string.Empty);
            }
        }

        public static string CreateSecureKey()
        {
            using (var random = RandomNumberGenerator.Create())
            {
                var key = new byte[16];
                random.GetBytes(key);
                string value = String.Concat<byte>(key);
                return value;
            }
        }
    }
}
