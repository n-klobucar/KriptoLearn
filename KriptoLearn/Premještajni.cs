using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class Premještajni : KritopisniSustav
    {
        public List<string> premještenaPoruka = new List<string>();
        private List<string> uređenaPoruka = new List<string>();
        private Dictionary<int, string> parovi = new Dictionary<int, string>();
        private List<List<string>> stupci = new List<List<string>>();
        private Dictionary<int, List<string>> poredak = new Dictionary<int, List<string>>();
        private List<string> sortiraniKljuč = new List<string>();

        private void KreirajSortiraniKljuč(List<string> ključ)
        {
            foreach (string slovo in ključ)
            {
                sortiraniKljuč.Add(slovo);
            }
            sortiraniKljuč.Sort();
        }
        private void DodajKljučUListuParova(List<string> ključ)
        {
            int pomak;
            bool dodano = false;
            foreach (string slovo in ključ)
            {
                pomak = 1;
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
        private void IspisKljuča()
        {
            //ispis slova ključa
            //if (složeni) { Console.WriteLine(); }
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
        private void UrediPoruku()
        {
            uređenaPoruka.Clear();
            //uklanjanje znakova koji nisu slova
            foreach (string slovo in jasnopis)
            {
                if (!jasnopisniSlovored.Contains(slovo)) { continue; }
                else { uređenaPoruka.Add(slovo); }
            }
        }
        private void IspisPorukeUTablici()
        {
            //ispis poruke u tablici
            for (int i = 0; i < uređenaPoruka.Count; i++)
            {
                if (i % sortiraniKljuč.Count == 0 && i != 0)
                {
                    Console.WriteLine();
                }
                Console.Write(uređenaPoruka[i] + " ");
            }
            Console.WriteLine("\n");
        }
        private void UpisivanjeStupacaUListu()
        {
            stupci.Clear();
            //upisivanje stupaca u listu
            for (int i = 0; i < sortiraniKljuč.Count; i++)
            {
                List<string> stupac = new List<string>();
                for (int j = i; j < uređenaPoruka.Count; j += sortiraniKljuč.Count)
                {
                    stupac.Add(uređenaPoruka[j]);
                }
                stupci.Add(stupac);
            }
        }
        private void SpajanjeRedoslijedaStupacaSBrojevima()
        {
            //spajanje redoslijeda sa slovima pripadajućeg stupca
            int brojač = 0;
            foreach (int brojStupca in parovi.Keys)
            {
                //Console.WriteLine("broj stupca: {0}", brojStupca);
                poredak.Add(brojStupca, stupci[brojač]);
                brojač++;
            }
        }
        private void PohranaPremještenePoruke()
        {
            premještenaPoruka.Clear();
            //pohrana premještene poruke
            foreach (KeyValuePair<int, List<string>> par in poredak.OrderBy(o => o.Key))
            {
                foreach (string slovo in par.Value) { zakritak.Add(slovo); }
            }
        }
        private void IspisZakritka()
        {
            Console.WriteLine("Ispis zakritka:");
            for (int i = 0; i < zakritak.Count(); i++)
            {
                if (i % 5 == 0 && i != 0) { Console.Write(" "); }
                Console.Write(zakritak[i]);
            }
        }
        private void AlgoritamZakrivanja()
        {
            IspisKljuča();
            UrediPoruku();
            IspisPorukeUTablici();
            UpisivanjeStupacaUListu();
            SpajanjeRedoslijedaStupacaSBrojevima();
            PohranaPremještenePoruke();
        }
        private void AlgoritamRaskrivanja()
        {
            IspisKljuča();
            UrediPoruku();

            //pohrana podataka o zakritku
            List<int> brStupca = new List<int>();
            foreach (int broj in parovi.Keys)
            {
                brStupca.Add(broj);
            }
            int brRedaka = uređenaPoruka.Count() / sortiraniKljuč.Count();
            int brSlovaPosljednjegRetka = uređenaPoruka.Count() % sortiraniKljuč.Count();
            int brojačSlova = 0;
            stupci.Clear();

            //kreiranje liste sa stupcima
            for (int i = 0; i < sortiraniKljuč.Count(); i++)
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

            SpajanjeRedoslijedaStupacaSBrojevima();
            PohranaPremještenePoruke();

            if (brSlovaPosljednjegRetka != 0) { brRedaka++; }
        }
        public void ZakrijPremještajnim(List<string> ključ, bool dvostruki = false, bool složeni = false) 
        {
            KreirajSortiraniKljuč(ključ);
            DodajKljučUListuParova(ključ);

            //ispis uputa zakrivanja
            if (složeni) { Console.WriteLine(); }
            Console.WriteLine("\nPostupak zakrivanja premještajnim sustavom sastoji se od:");
            Console.WriteLine("1. Zapisa slova ključa,");
            Console.WriteLine("2. Zapisa brojeva redoslijednog poretka slova ključa (u red ispod),");
            Console.WriteLine("3. Zapisa poruke u retke ispod ključa te");
            Console.WriteLine("4. Išćitavanje/ispisivanje slova iz stupaca uzlaznim redoslijedom.");
            Console.WriteLine("5. Ukoliko se radi o dvostrukom raskrivanju, još jednom se ponove 3. i 4. korak.\n");

            AlgoritamZakrivanja();

            if (dvostruki)
            {
                jasnopis.Clear();
                foreach (string slovo in zakritak){ jasnopis.Add(slovo); }
                zakritak.Clear();
                poredak.Clear();

                AlgoritamZakrivanja();
            }
            IspisZakritka();
        }
        public void RaskrijPremještajnim(List<string> ključ, bool dvostruki = false, bool složeni = false) 
        {
            KreirajSortiraniKljuč(ključ);
            DodajKljučUListuParova(ključ);

            //ispis uputa raskrivanja
            if (složeni) { Console.WriteLine(); }
            Console.WriteLine("\nPostupak raskrivanja premještajnim sustavom sastoji se od:");
            Console.WriteLine("1. Zapisa slova ključa,");
            Console.WriteLine("2. Zapisa brojeva redoslijednog poretka slova ključa (u red ispod),");
            Console.WriteLine("3. Zapisa zakritka u stupce uzlaznim redoslijedom, ");
            Console.WriteLine("4. Da bi znali koliko slova upisati u pojedini stupac, potrebno je izračunati broj znakova u posljednjem retku ");
            Console.WriteLine("po ostatku cjelobrojnog dijeljenja s duljinom ključa.");
            Console.WriteLine("5. Ukoliko se radi o dvostrukom raskrivanju, još jednom se ponove 3. i 4. korak.\n");

            //ispis podataka zakritka
            Console.WriteLine("Broj stupaca je: {0}", ključ.Count());
            int retci = 0;
            if (uređenaPoruka.Count() % ključ.Count() != 0) { retci++; }
            retci += uređenaPoruka.Count() / ključ.Count();
            Console.WriteLine("Broj redaka je duljina poruke/duljina ključa i iznosi: {0}\n", retci);


            //        #region jednostruki vs. dvostruki

            //        if (dvostruki)
            //        {
            //                for (int i = 0; i < brRedaka; i++)
            //                {
            //                    foreach (KeyValuePair<int, List<string>> par in poredak)
            //                    {
            //                        try
            //                        {
            //                            premještenaPoruka.Add(par.Value[i]);
            //                        }
            //                        catch
            //                        {
            //                            continue;
            //                        }
            //                    }
            //                }
            //            jasnopis = premještenaPoruka;
            //            RaskrijPremještajnim(ključ);
            //        }
            //        else
            //        {
            //            //ispis zakritka
            //            //Console.WriteLine("\n Ispis jasnopisa:");
            //            for (int i = 0; i < brRedaka; i++)
            //            {
            //                foreach (KeyValuePair<int, List<string>> par in poredak)
            //                {
            //                    try
            //                    {
            //                        Console.Write(par.Value[i]+" ");
            //                    }
            //                    catch
            //                    {
            //                        continue;
            //                    }
            //                }
            //                Console.WriteLine();
            //            }
            //            Console.WriteLine("\nNa kraju je potrebno prepisati slova iz tablice s pravilnim interpunkcijama.");
            //        }
            //        #endregion
        }
    }
}
