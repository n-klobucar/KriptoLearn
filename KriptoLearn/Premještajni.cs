using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class Premještajni : KritopisniSustav
    {
        private List<string> abeceda = new List<string>() {"A", "B", "C", "Č", "Ć", "D", "Dž", "Đ", "E", "F", "G", "H", "I", "J", "K",
                                            "L", "Lj", "M", "N", "Nj", "O", "P", "R", "S", "Š", "T", "U", "V", "Z", "Ž"};
        public List<string> premještenaPoruka = new List<string>();
        public void Zakrij(List<string> ključ, List<string> poruka, bool dvostruki = false, bool složeni = false) //radi
        {
            Dictionary<int, string> parovi = new Dictionary<int, string>();
            Dictionary<int, List<string>> poredak = new Dictionary<int, List<string>>();
            List<string> sortiraniKljuč = new List<string>();

            foreach (string slovo in ključ)
            {
                sortiraniKljuč.Add(slovo);
            }
            sortiraniKljuč.Sort();
            int pomak;
            bool dodano = false;
            foreach (string slovo in ključ)
            {
                pomak = 2;
                try
                {
                    parovi.Add(sortiraniKljuč.IndexOf(slovo) + 1, slovo);
                    //Console.Write((sortiraniKljuč.IndexOf(slovo) + 1).ToString());
                }
                catch
                {
                    //Console.WriteLine(ex.Message);
                    //ponavljanja++;
                    do
                    {
                        try
                        {
                            parovi.Add(sortiraniKljuč.IndexOf(slovo) + pomak, slovo);
                            dodano = true;
                        }
                        catch
                        {
                            pomak++;
                            dodano = false;
                        }
                    } while (!dodano);
                    //Console.Write((sortiraniKljuč.IndexOf(slovo) + 2).ToString() + slovo);
                }
            }
            //ispis uputa zakrivanja
            if (premještenaPoruka.Count==0)
            {
                if (složeni) { Console.WriteLine("\n"); }
                Console.WriteLine("\nPostupak zakrivanja premještajnim sustavom sastoji se od:");
                Console.WriteLine("1. Zapisa slova ključa,");
                Console.WriteLine("2. Zapisa brojeva redoslijednog poretka slova ključa (u red ispod),");
                Console.WriteLine("3. Zapisa poruke u retke ispod ključa te");
                Console.WriteLine("4. Išćitavanje/ispisivanje slova iz stupaca uzlaznim redoslijedom.");
                Console.WriteLine("5. Ukoliko se radi o dvostrukom raskrivanju, još jednom se ponove 3. i 4. korak.\n");
            }
            //ispis slova ključa
            if (složeni) { Console.WriteLine(); }
            foreach (string vrijednosti in parovi.Values)
            {
                Console.Write(vrijednosti + " ");
            }
            Console.WriteLine();

            //ispis brojeva abecednog poretka slova ključa
            foreach (int ključevi in parovi.Keys)
            {
                Console.Write(ključevi.ToString() + " ");
            }
            Console.WriteLine();

            //uklanjanje znakova koji nisu slova
            List<string> uređenaPoruka = new List<string>();
            foreach (string slovo in poruka)
            {
                if (!abeceda.Contains(slovo)) { continue; }
                else { uređenaPoruka.Add(slovo); }
            }

            //ispis poruke u tablici
            for (int i = 0; i < uređenaPoruka.Count; i++)
            {
                if (i % ključ.Count == 0 && i != 0)
                {
                    Console.WriteLine();
                }
                Console.Write(uređenaPoruka[i] + " ");
            }
            Console.WriteLine("\n");

            //kreiranje liste sa stupcima
            List<List<string>> stupci = new List<List<string>>();
            for (int i = 0; i < ključ.Count; i++)
            {
                List<string> stupac = new List<string>();
                for (int j = i; j < uređenaPoruka.Count; j += ključ.Count)
                {
                    stupac.Add(uređenaPoruka[j]);
                }
                stupci.Add(stupac);
            }

            //spajanje redoslijeda sa slovima pripadajućeg stupca
            int brojač = 0;
            foreach (int brojStupca in parovi.Keys)
            {
                poredak.Add(brojStupca, stupci[brojač]);
                brojač++;
            }

            //pohrana premještene poruke
            premještenaPoruka.Clear();
            foreach (KeyValuePair<int, List<string>> par in poredak.OrderBy(o => o.Key))
            {
                foreach (string slovo in par.Value)
                {
                    premještenaPoruka.Add(slovo);
                }
            }
            //ako koristimo dvostruki, ponovi postupak
            if (dvostruki) { Zakrij(ključ, premještenaPoruka); }
            //inače, ispiši premještenu poruku
            else
            {
                Console.WriteLine("Ispis zakritka:");
                for (int i = 0; i < premještenaPoruka.Count(); i++)
                {
                    if (i % 5 == 0 && i != 0) { Console.Write(" "); }
                    Console.Write(premještenaPoruka[i]);
                }
            }
        }
        public void Raskrij(List<string> ključ, List<string> poruka, bool dvostruki = false, bool složeni = false) //radi
        {
            Dictionary<int, string> parovi = new Dictionary<int, string>();
            Dictionary<int, List<string>> poredak = new Dictionary<int, List<string>>();
            List<string> sortiraniKljuč = new List<string>();

            foreach (string slovo in ključ)
            {
                sortiraniKljuč.Add(slovo);
            }
            sortiraniKljuč.Sort();
            int pomak;
            bool dodano = false;
            foreach (string slovo in ključ)
            {
                pomak = 2;
                try
                {
                    parovi.Add(sortiraniKljuč.IndexOf(slovo) + 1, slovo);
                    //Console.Write((sortiraniKljuč.IndexOf(slovo) + 1).ToString());
                }
                catch
                {
                    //Console.WriteLine(ex.Message);
                    //ponavljanja++;
                    do
                    {
                        try
                        {
                            parovi.Add(sortiraniKljuč.IndexOf(slovo) + pomak, slovo);
                            dodano = true;
                        }
                        catch
                        {
                            pomak++;
                            dodano = false;
                        }
                    } while (!dodano);
                    //Console.Write((sortiraniKljuč.IndexOf(slovo) + 2).ToString() + slovo);
                }
            }
            //ispis uputa raskrivanja
            if (premještenaPoruka.Count() == 0)
            {
                if (složeni) { Console.WriteLine("\n"); }
                Console.WriteLine("\nPostupak raskrivanja premještajnim sustavom sastoji se od:");
                Console.WriteLine("1. Zapisa slova ključa,");
                Console.WriteLine("2. Zapisa brojeva redoslijednog poretka slova ključa (u red ispod),");
                Console.WriteLine("3. Zapisa zakritka u stupce uzlaznim redoslijedom, ");
                Console.WriteLine("4. Da bi znali koliko slova upisati u pojedini stupac, potrebno je izračunati broj znakova u posljednjem retku ");
                Console.WriteLine("po ostatku cjelobrojnog dijeljenja s duljinom ključa.");
                Console.WriteLine("5. Ukoliko se radi o dvostrukom raskrivanju, još jednom se ponove 3. i 4. korak.\n");
            }

            //uklanjanje znakova koji nisu slova
            List<string> uređenaPoruka = new List<string>();
            foreach (string slovo in poruka)
            {
                if (!abeceda.Contains(slovo)) { continue; }
                else { uređenaPoruka.Add(slovo); }
            }

            if (!dvostruki)
            {
                Console.WriteLine("Broj stupaca je: {0}", ključ.Count());
                int retci = 0;
                if(uređenaPoruka.Count() % ključ.Count() != 0) { retci++; }
                retci += uređenaPoruka.Count() / ključ.Count();
                Console.WriteLine("Broj redaka je duljina poruke/duljina ključa i iznosi: {0}\n", retci);
                //ispis slova ključa
                foreach (string vrijednosti in parovi.Values)
                {
                    Console.Write(vrijednosti + " ");
                }
                Console.WriteLine();

                //ispis brojeva abecednog poretka slova ključa
                foreach (int ključevi in parovi.Keys)
                {
                    Console.Write(ključevi.ToString() + " ");
                }
                Console.WriteLine();
            }

            List<int> brStupca = new List<int>();
            foreach (int broj in parovi.Keys)
            {
                brStupca.Add(broj);
            }

            int brRedaka = uređenaPoruka.Count() / ključ.Count();
            int brSlovaPosljednjegRetka = uređenaPoruka.Count() % ključ.Count();
            int brojačSlova = 0;
            //kreiranje liste sa stupcima
            List<List<string>> stupci = new List<List<string>>();

            for (int i = 0; i < ključ.Count(); i++)
            {
                List<string> stupac = new List<string>();
                if (brSlovaPosljednjegRetka != 0)
                {
                    if (brStupca.IndexOf(i + 1) < brSlovaPosljednjegRetka)
                    {
                        for (int j = 0; j < brRedaka + 1; j++)
                        {
                            stupac.Add(uređenaPoruka[brojačSlova]);
                            brojačSlova++;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < brRedaka; j++)
                        {
                            stupac.Add(uređenaPoruka[brojačSlova]);
                            brojačSlova++;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < brRedaka; j++)
                    {
                        stupac.Add(uređenaPoruka[brojačSlova]);
                        brojačSlova++;
                    }
                }
                stupci.Add(stupac);
            }

            //spajanje redoslijeda sa slovima pripadajućeg stupca
            foreach (int brojStupca in parovi.Keys)
            {
                //Console.WriteLine("broj stupca: {0}", brojStupca);
                poredak.Add(brojStupca, stupci[brojStupca-1]);
            }
            //pohrana premještene poruke
            premještenaPoruka.Clear();
            //foreach (KeyValuePair<int, List<string>> par in poredak.OrderBy(o => o.Key))
            //{
            //    foreach (string slovo in par.Value)
            //    {
            //        premještenaPoruka.Add(slovo);
            //    }
            //}
            #region jednostruki vs. dvostruki
            if (brSlovaPosljednjegRetka != 0) { brRedaka++; }
            if (dvostruki)
            {
                    for (int i = 0; i < brRedaka; i++)
                    {
                        foreach (KeyValuePair<int, List<string>> par in poredak)
                        {
                            try
                            {
                                premještenaPoruka.Add(par.Value[i]);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                Raskrij(ključ, premještenaPoruka);
            }
            else
            {
                //ispis zakritka
                //Console.WriteLine("\n Ispis jasnopisa:");
                for (int i = 0; i < brRedaka; i++)
                {
                    foreach (KeyValuePair<int, List<string>> par in poredak)
                    {
                        try
                        {
                            Console.Write(par.Value[i]+" ");
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\nNa kraju je potrebno prepisati slova iz tablice s pravilnim interpunkcijama.");
            }
            #endregion
        }
    }
}
