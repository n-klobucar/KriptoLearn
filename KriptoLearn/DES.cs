using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace KriptoLearn
{
    //class DES
    //{
    //    private List<string> ključ = new List<string>();
    //    Random random = new Random();
    //    public void Zakrij(List<string> poruka)
    //    {
    //        #region rad s ključem
    //        //kreiranje ključa
    //        for (int i = 0; i < 8; i++)
    //        {
    //            string k = "";
    //            for (int j = 0; j < 7; j++)
    //            {
    //                k += random.Next(2);
    //            }
    //            Console.WriteLine(k);
    //            ključ.Add(k);
    //        }

    //        //proširivanje ključa (parity bit)
    //        //izračun bita parnosti
    //        string dodatak = "";
    //        foreach (string dio in ključ)
    //        {
    //            int parnost = dio.Where(o => o.ToString().Contains("1")).Count();
    //            Console.WriteLine("Broj jedinica: {0}", parnost);
    //            if (parnost % 2 == 0) { dodatak += "1"; }
    //            else { dodatak += "0"; }
    //        }
    //        Console.WriteLine("Dodatak: {0}", dodatak);
    //        //dodavanje bitova parnosti
    //        for (int i = 0; i < ključ.Count(); i++)
    //        {
    //            ključ[i] += dodatak[i];
    //        }
    //        //ispis proširenog ključa
    //        foreach (string dio in ključ)
    //        {
    //            Console.WriteLine(dio);
    //        }
    //        //po tablicama kreirati 16 podključeva
    //        #endregion
    //    }
    //}
    class Des : KritopisniSustav
    {
        public static byte[] EncryptTextToMemory(string Data, byte[] Key, byte[] IV)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                // Create a new DES object.
                DES DESalg = DES.Create();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    DESalg.CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                // Convert the passed string to a byte array.
                byte[] toEncrypt = new ASCIIEncoding().GetBytes(Data);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the
                // MemoryStream that holds the
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Kriptografska greška: {0}", e.Message);
                return null;
            }
        }

        public static string DecryptTextFromMemory(byte[] Data, byte[] Key, byte[] IV)
        {
            try
            {
                // Create a new MemoryStream using the passed
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(Data);

                // Create a new DES object.
                DES DESalg = DES.Create();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    DESalg.CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[Data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return new ASCIIEncoding().GetString(fromEncrypt);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Kriptografska greška: {0}", e.Message);
                return null;
            }
        }
    }
}
