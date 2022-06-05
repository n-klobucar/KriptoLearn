using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class Premještajni : KritopisniSustav
    {
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
            bool dodano;
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
        public void UrediPoruku(List<string> poruka)
        {
            uređenaPoruka.Clear();
            //uklanjanje znakova koji nisu slova
            foreach (string slovo in poruka)
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
            for (int i = 0; i < sortiraniKljuč.Count(); i++)
            {
                List<string> stupac = new List<string>();
                for (int j = i; j < uređenaPoruka.Count(); j += sortiraniKljuč.Count())
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
        private void PohranaZakritka()
        {
            //pohrana zakritka
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
        private void IspisRaskritka()
        {
            for (int i = 0; i < jasnopis.Count(); i++)
            {
                if (i % sortiraniKljuč.Count() == 0 && i != 0) { Console.WriteLine(); }
                Console.Write(jasnopis[i] + " ");
            }
            Console.WriteLine();
        }
        private void AlgoritamZakrivanja()
        {
            IspisKljuča();
            UrediPoruku(jasnopis);
            IspisPorukeUTablici();
            UpisivanjeStupacaUListu();
            SpajanjeRedoslijedaStupacaSBrojevima();
            PohranaZakritka();
        }
        private void AlgoritamRaskrivanja()
        {
            IspisKljuča();
            UrediPoruku(zakritak);
            IspisPorukeUTablici();

            //pohrana podataka o zakritku
            List<int> poredakStupca = new List<int>();
            foreach (int broj in parovi.Keys)
            {
                poredakStupca.Add(broj);
            }
            int brRedaka = uređenaPoruka.Count() / sortiraniKljuč.Count();
            int brSlovaPosljednjegRetka = uređenaPoruka.Count() % sortiraniKljuč.Count();
            if (brSlovaPosljednjegRetka != 0) { brRedaka++; }
            int brojačSlova = 0;

            //kreiranje raskritka
            foreach (int element in poredakStupca.OrderBy(o=>o))
            {
                List<string> stupac = new List<string>();
                if (poredakStupca.IndexOf(element) < brSlovaPosljednjegRetka)
                {
                    for (int i = 0; i < brRedaka; i++) { stupac.Add(uređenaPoruka[brojačSlova]); brojačSlova++; }
                }
                else
                {
                    for (int i = 0; i < brRedaka - 1; i++) { stupac.Add(uređenaPoruka[brojačSlova]); brojačSlova++; }
                }
                poredak.Add(poredakStupca.IndexOf(element), stupac);
            }
            //pohrana raskritka
            jasnopis.Clear();
            for (int i = 0; i < brRedaka; i++)
            {
                foreach (KeyValuePair<int, List<string>> par in poredak.OrderBy(o => o.Key))
                {
                    try { jasnopis.Add(par.Value[i]); }
                    catch { continue; }
                }
            }
            IspisRaskritka();
        }
        public void ZakrijPremještajnim(List<string> ključ, bool dvostruki = false, bool složeni = false) 
        {
            KreirajSortiraniKljuč(ključ);
            DodajKljučUListuParova(ključ);
            UrediPoruku(jasnopis);

            #region Upute
            //ispis uputa zakrivanja
            if (složeni) { Console.WriteLine(); }
            Console.WriteLine("\nPostupak zakrivanja premještajnim sustavom sastoji se od:");
            Console.WriteLine("1. Zapisa slova ključa,");
            Console.WriteLine("2. Zapisa brojeva redoslijednog poretka slova ključa (u red ispod),");
            Console.WriteLine("3. Zapisa poruke u retke ispod ključa te");
            Console.WriteLine("4. Išćitavanje/ispisivanje slova iz stupaca uzlaznim redoslijedom.");
            Console.WriteLine("5. Ukoliko se radi o dvostrukom raskrivanju, još jednom se ponove 3. i 4. korak.\n");
            #endregion

            AlgoritamZakrivanja();

            if (dvostruki) { Console.WriteLine("\n\nOdabrali ste dvostruki pa ponavljam postupak.\n"); jasnopis.Clear(); foreach (string slovo in zakritak) { jasnopis.Add(slovo); } zakritak.Clear(); poredak.Clear(); AlgoritamZakrivanja(); }
            IspisZakritka();
        }
        public void RaskrijPremještajnim(List<string> ključ, bool dvostruki = false, bool složeni = false) 
        {
            KreirajSortiraniKljuč(ključ);
            DodajKljučUListuParova(ključ);
            UrediPoruku(zakritak);

            #region Upute
            //ispis uputa raskrivanja
            if (složeni) { Console.WriteLine(); }
            Console.WriteLine("\nPostupak raskrivanja premještajnim sustavom sastoji se od:");
            Console.WriteLine("1. Zapisa slova ključa,");
            Console.WriteLine("2. Zapisa brojeva redoslijednog poretka slova ključa (u red ispod),");
            Console.WriteLine("3. Zapisa zakritka u stupce uzlaznim redoslijedom, ");
            Console.WriteLine("4. Da bi znali koliko slova upisati u pojedini stupac, potrebno je izračunati broj znakova u posljednjem retku ");
            Console.WriteLine("po ostatku cjelobrojnog dijeljenja s duljinom ključa.");
            Console.WriteLine("5. Ukoliko se radi o dvostrukom raskrivanju, još jednom se ponove 3. i 4. korak.\n");
            #endregion

            //ispis podataka zakritka
            Console.WriteLine("Broj stupaca je: {0}", sortiraniKljuč.Count());
            int retci = uređenaPoruka.Count() / sortiraniKljuč.Count();
            if (uređenaPoruka.Count() % sortiraniKljuč.Count() != 0) { retci++; }
            Console.WriteLine("Broj redaka je duljina poruke/duljina ključa i iznosi: {0}\n", retci);
            AlgoritamRaskrivanja();

            if (dvostruki) { Console.WriteLine("\n\nOdabrali ste dvostruki pa ponavljam postupak.\n"); zakritak.Clear(); foreach (string slovo in jasnopis) { zakritak.Add(slovo); } jasnopis.Clear(); poredak.Clear(); UrediPoruku(zakritak); AlgoritamRaskrivanja(); }
            else{ Console.WriteLine("\nRaskrivenu poruku treba prepisati iz tablice s valjanim interpunkcijskim znakovima."); }
        }
    }
}
