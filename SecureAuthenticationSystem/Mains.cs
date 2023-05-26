using SecureAuthenticationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecureAuthenticationSystem
{
    public class Mains
    {
        Model model = new Model();
        List<User> users = new List<User>();


        public static string Encrypt(string plainText, byte[] encryptionKeyBytes)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKeyBytes;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new())
                {
                    using (CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private static readonly byte[] Salt = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };
        public static byte[] CreateKey(string password, int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, Salt, Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
        
        public void Add(string email, string password)
        {
            //get an encryption key from the password
            var passbyte = CreateKey(password);

            var passhash = Encrypt(password, passbyte);
            model.Store(email, passhash);
        }
        public void Show()
        {
            var i = 0;

            foreach (var user in users)
            {
                if (user.email != null)
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine("ID : " + i++);
                    Console.WriteLine("User Email : " + user.email);
                    Console.WriteLine("Password: " + user.password);
                    Console.WriteLine("=========================");
                }
            }

            OtherMenu();
        }
        public void Search()
        {
            Console.WriteLine("Enter email to reset password:");
        }
        public void Login()
        {
            /*      Email[] email = model.getAllEmail();
                    Password[] password = model.getAllPassword();*/

            Console.WriteLine("==LOGIN==");
            Console.Write("EMAIL : ");
            var inputUserName = Input();
            Console.Write("PASSWORD: ");
            var inputPassword = Input();

            foreach (var user in users)
            {
                if (inputUserName == (user.email) && inputPassword == user.password)
                {
                    Console.WriteLine("Login Successful!");
                    OtherMenu();
                }
                else
                {
                    Console.WriteLine("Login Failed!");
                    Menu();
                }
            }

        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Secure Authentication System==");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Reset Password");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Input : ");

                var input = Input();

                Console.Clear();

                if (input.Equals("1"))
                {
                    Login();
                }
                else if (input.Equals("2"))
                {
                    Console.Write("email : ");
                    var uE = Input();
                    Console.Write("password : ");
                    var pW = Input();

                    Validate(uE, pW);

                    Console.Clear();
                }
                else if (input.Equals("3"))
                {
                    Show();
                }
                else if (input.Equals("4"))
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please enter a valid input!");
                }
            }

        }

        public string Input()
        {
            var data = Console.ReadLine();
            return data;
        }
        public void Validate(string email, string password)
        {

            var validate = Convert.ToString(password);

            if ((validate.Length >= 8) && (validate.Any(char.IsUpper)) && (validate.Any(char.IsLower)) && (validate.Any(char.IsNumber)))
            {
                Add(email, password);
            }
            else
            {
                Console.WriteLine("\nPassword must have at least 8 characters\r\n with at least one Capital letter, at least one lower case letter and at least one number.\n");
                Console.WriteLine("Password : ");
                var setPass = Input();

                Add(email, setPass);
            }
        }

        public void OtherMenu()
        {
            while (true)
            {
                Console.WriteLine("\nProfile");
                Console.WriteLine("1. Logout");
                Console.WriteLine("2. Back");

                var profile = Input();
                if (profile.Equals("1"))
                {
                    Delete();
                }
                else if (profile.Equals("2"))
                { 
                    Menu();
                }
            }
        }

        public void Delete()
        {
            Console.WriteLine("Successfull Logout!");
        }

    }
}
