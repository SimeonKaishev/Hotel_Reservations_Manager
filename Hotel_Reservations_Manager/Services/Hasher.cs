using Hotel_Reservations_Manager.Exeptions;
using HotelData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Services
{
    public static class Hasher
    {
        /// <summary>
        /// Generates a salt to be iserted into the password
        /// </summary>
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        /// <summary>
        /// Generates a hashed password
        /// </summary>

        public static string GetHash(string password)
        {
            byte[] salt = GenerateSalt();
            using (var hasher = new Rfc2898DeriveBytes(password, salt, 1000))
            {
                byte[] hashedPass = hasher.GetBytes(20);
                byte[] saltyHashedPassword = new byte[36];
                Array.Copy(hashedPass, 0, saltyHashedPassword, 0, 20);
                Array.Copy(salt, 0, saltyHashedPassword, 20, 16);
                string base64Password = Convert.ToBase64String(saltyHashedPassword);
                return base64Password;
            }
        }
        /// <summary>
        /// compares a password to the hash saved into the database
        /// </summary>
        public static void CheckPass(User usr, string imputPass)
        {
            byte[] hashbytes = Convert.FromBase64String(usr.Password);
            byte[] saltExtracted = new byte[16];
            Array.Copy(hashbytes, 20, saltExtracted, 0, 16);
            using (var loginHasher = new Rfc2898DeriveBytes(imputPass, saltExtracted, 1000))
            {
                byte[] hashExtracted = loginHasher.GetBytes(20);
                for (int i = 0; i < 20; i++)
                {
                    if (hashbytes[i] != hashExtracted[i])
                    {
                        throw new IncorrectPassExeption();
                    }
                }
            }
        }
    }
}

