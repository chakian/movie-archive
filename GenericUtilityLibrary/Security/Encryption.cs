using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace com.cagdaskorkut.utility.Security {
	public class Encryption {
		private static string passPhrase = ConfigurationManager.AppSettings["passPhrase"]; // "passPhrase";
		private static string saltValue = ConfigurationManager.AppSettings["saltValue"];// "saltIt";
		private const string hashAlgorithm = "SHA1";
		private const int passwordIterations = 5;
		private static string initVector = ConfigurationManager.AppSettings["initVector"]; //"3CabiK@!orealnKc";
		private static bool encryptionEnabled = bool.Parse( ConfigurationManager.AppSettings["encryptionEnabled"] );
		private const int keySize = 256;

		public static string Encrypt( string plainText ) {
			if( encryptionEnabled == false )
				return plainText;

			string cipherText = Rijndael.Encrypt( plainText,
														passPhrase,
														saltValue,
														hashAlgorithm,
														passwordIterations,
														initVector,
														keySize );

			return cipherText;
		}

		public static string Decrypt( string cipherText ) {
			if( encryptionEnabled == false )
				return cipherText;

			string plainText = Rijndael.Decrypt( cipherText,
														passPhrase,
														saltValue,
														hashAlgorithm,
														passwordIterations,
														initVector,
														keySize );

			return plainText;
		}

        public static string getMD5Hash(string input)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(input);
            try
            {
                MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw new Exception("Şifreleme sırasında bir hata oluştu.");
            }
        }

        /// <summary>
		/// Returns SHA1 hash value of the given input
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private static string ToSHA1(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Parse.StringToByteArray(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
    }
}
