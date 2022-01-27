using System;
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
        public void Zakrij(string jasnopisnaPoruka)
        {
            if (String.IsNullOrEmpty(jasnopisnaPoruka)) { throw new ArgumentNullException("Poruka ne smije biti duljine 0."); }

            DES DESalg = DES.Create();
            byte[] iv = DESalg.IV;
            byte[] ključ = DESalg.Key;

            string string_iv = Convert.ToBase64String(iv);
            string string_ključ = Convert.ToBase64String(ključ);
            Console.WriteLine("IV:{0}" + "<--kraj iv", string_iv);
            Console.WriteLine("Ključ:{0}" + "<--kraj ključa", string_ključ);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(ključ, iv), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(jasnopisnaPoruka);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            string rezultat = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            foreach (char znak in rezultat) { zakritak.Add(znak.ToString()); }
        }

        public void Raskrij(string zakrivenaPoruka)
        {
            if (String.IsNullOrEmpty(zakrivenaPoruka)) { throw new ArgumentNullException("Poruka ne smije biti duljine 0."); }

            byte[] IV = Convert.FromBase64String(sIV);
            byte[] ključ = Convert.FromBase64String(sKljuč);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(zakrivenaPoruka));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(ključ, IV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            string rezultat = reader.ReadToEnd();
            foreach (char znak in rezultat) { jasnopis.Add(znak.ToString()); }
        }
    }
}
