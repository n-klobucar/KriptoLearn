﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace KriptoLearn
{
    class Des : KritopisniSustav
    {
        public string sKljuč;
        public string sIV;
        public void Zakrij(string poruka)
        {
            if (String.IsNullOrEmpty(poruka))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }

            //Create a new DES object.
            DES DESalg = DES.Create();
            byte[] iv = DESalg.IV;
            byte[] key = DESalg.Key;

            string string_iv = Convert.ToBase64String(iv);
            string string_key = Convert.ToBase64String(key);
            Console.WriteLine("IV:{0}" + "<--kraj iv", string_iv);
            Console.WriteLine("Key:{0}" + "<--kraj ključa", string_key);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(poruka);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            string rezultat = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            foreach (char znak in rezultat) //FUNKCIJA KOJA SAMA ODLUČUJE JASNOPIS ILI ZAKRITAK
            {
                zakritak.Add(znak.ToString());
            }
        }

        public void Raskrij(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }

            byte[] IV = Convert.FromBase64String(sIV);
            byte[] ključ = Convert.FromBase64String(sKljuč);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(ključ, IV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            string rezultat = reader.ReadToEnd();
            foreach (char znak in rezultat)
            {
                jasnopis.Add(znak.ToString());
            }
        }
    }
}
