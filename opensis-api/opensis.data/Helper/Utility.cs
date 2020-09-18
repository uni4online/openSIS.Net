using opensis.data.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace opensis.data.Helper
{
    
    public class Utility
    {
        /// <summary>
        /// This method returns a int primarykeyId  for an entity.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="cRMContext"></param>
        /// <param name="columnSelector"></param>
        /// <returns></returns>
        public static int? GetMaxPK<TEntity>(CRMContext cRMContext, Func<TEntity, int> columnSelector) where TEntity : class
        {
            int? GetMaxId = 0;
           
                
                var entityClass = cRMContext?.Set<TEntity>();
                if (entityClass.Count() == 0)
                {
                    GetMaxId = 1;
                }
                else
                {
                    GetMaxId = cRMContext?.Set<TEntity>().Max(columnSelector);
                    if (GetMaxId == null || GetMaxId <= 0)
                    {
                        GetMaxId = 1;
                    }
                    else
                    {
                        GetMaxId = GetMaxId + 1;
                    }
                }


            return GetMaxId;
        }
        /// <summary>
        /// This method returns a decrypt string for a password.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            string passwordKey = "oPen$!$.b14Ca5898a4e4133b!";
            byte[] cipherBytes = Convert.FromBase64String(cipherText); using (Aes encryptor = Aes.Create())
            {
                var salt = cipherBytes.Take(16).ToArray();
                var iv = cipherBytes.Skip(16).Take(16).ToArray();
                var encrypted = cipherBytes.Skip(32).ToArray();
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(passwordKey, salt, 100); encryptor.Key = pdb.GetBytes(32);
                encryptor.Padding = PaddingMode.PKCS7;
                encryptor.Mode = CipherMode.CBC;
                encryptor.IV = iv; using (MemoryStream ms = new MemoryStream(encrypted))
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs, Encoding.UTF8))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method returns a hashed string for a password.
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string GetHashedPassword(string Input)
        {
            string passwordSecurityKey = "oPenSIsV2.0lOGinS1c0R3t8K61";
            var sha1 = System.Security.Cryptography.SHA256.Create();
            var inputBytes = Encoding.ASCII.GetBytes(Input + passwordSecurityKey);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
