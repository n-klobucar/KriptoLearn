using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class RSA : KritopisniSustav
    {
        //p i q - prosti brojevi
        //fin - Eulerov toličnik, (p-1)(q-1)
        //e - javni ključ
        //d - tajni ključ
        //n - javni djelitelj, n=p*q (može biti 1 ili mora biti veći od 1?)
        //e*d mod fin = 1

        public int e, d, n;
        private int fin = 1;
        private List<int> faktori = new List<int>();

        private void RastavljanjeNaFaktore(int n)
        {
            //kreiranje liste prostih brojeva
            List<int> prostiBrojevi = new List<int>();
            for (int i = 2; i < 1000; i++)
            {
                int brojač = 0;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0) { brojač++; }
                }
                if (brojač == 0) { prostiBrojevi.Add(i);  }
            }
            Console.WriteLine("Kreirao sam listu prostih brojeva do 1000.");
            //pronalazak p i q
            Console.WriteLine("Prolazim kroz listu prostih brojeva i tražim faktore.");
            int trajanje = n / 2;
            for (int i = 0; i < trajanje; i++)
            {
                if (n % prostiBrojevi[i] == 0)
                {
                    faktori.Add(prostiBrojevi[i]);
                    n /= prostiBrojevi[i];
                    i = 0;
                }
            }
        }
        public void UnosIProvjeraJavnogKljučaE()
        {
            Console.Write("Unesite vrijednost javnog ključa (e): ");
            string proizvoljnoe = Console.ReadLine();
            while (!proizvoljnoe.All(char.IsDigit) || proizvoljnoe.Length == 0)
            {
                Console.WriteLine("Niste unijeli broj.");
                Console.Write("Unesite vrijednost javnog ključa (e): ");
                proizvoljnoe = Console.ReadLine();
            }
            e = int.Parse(proizvoljnoe);
        }
        public void UnosIProvjeraJavnogDjeliteljaN()
        {
            Console.Write("Unesite vrijednost javnog djelitelja (n): ");
            string proizvoljnon = Console.ReadLine();
            while (!proizvoljnon.All(char.IsDigit) && int.Parse(proizvoljnon) < 3)
            {
                Console.WriteLine("Niste unijeli valjanu vrijednost. [>3]");
                Console.Write("Unesite vrijednost javnog djelitelja (n): ");
                proizvoljnon = Console.ReadLine();
            }
            n = int.Parse(proizvoljnon);
        }
        private void IzračunFin()
        {
            Console.Write("Javni djelitelj je umnožak ovih prostih brojeva:");
            foreach (int broj in faktori)
            {
                Console.Write(" {0}", broj);
                fin *= broj - 1;
            }
            Console.WriteLine("\nIznos Eulerovog toličnika: {0}", fin);
        }
        private void IzračunTajnogKljučaD()
        {
            for (int i = 0; i < 21; i++)
            {
                int t = i * fin + 1;
                if (t % e == 0) { d = t / e; break; }
                else { continue; }
            }
        }

        public void ZakrijRSA() 
        {
            //javni ključ i javni djelitelj (e i n)
            //Zi=Ji^e mod n

            #region Upute
            //indeks slova poruke potenciram s e te mod n, dobiveni broj je indeks zakritnog slova
            //upute za raskrivanje koristeći RSA
            Console.WriteLine("\nPostupak zakrivanja poruke RSA sustavom sastoji se od:");
            Console.WriteLine("1. pronalazak indeksa svakog slova jasnopisa (A->0, B->1, ...),");
            Console.WriteLine("2. potenciranje tog indeksa vrijednošću javnog ključa (e),");
            Console.WriteLine("3. završno, ostatak cijelobrojnog dijeljenja javnim djeliteljem (n) čini indeks slova zakritka.\n");
            #endregion

            //kreiranje zakritka
            foreach (string slovo in jasnopis)
            {
                try
                {
                    //POPRAVI
                    Console.WriteLine("slovo jasnopisa: {0}", slovo);
                    double indeks = jasnopisniSlovored.IndexOf(slovo); //indeks slova jasnopisa
                    Console.WriteLine("indeks jasnopisa: {0}", indeks);
                    indeks = Math.Pow(indeks, e); //potenciranje indeksa s e
                    Console.WriteLine("potencirani indeks: {0}", indeks);
                    indeks %= n; //ostatak cijelobrojnog dijeljenja s n
                    Console.WriteLine("ostatak cjelobrojnog dijeljenja s n: {0}", indeks);
                    Console.WriteLine("dodavanje slova {0} u zakritak",jasnopisniSlovored[(int)indeks]);
                    zakritak.Add(jasnopisniSlovored[(int)indeks]); //dodavanje slova zakritka
                }
                catch { Console.WriteLine("Posebne znakove samo prepisujem u zakritak."); zakritak.Add(slovo); }
                Console.WriteLine();
            }
        }

        public void RaskrijRSA() //uredi ispis za cijeli sustav
        {
            //tajni ključ i javni djelitelj (d i n)
            //Ji=Zi^d mod n

            #region Upute
            //upute za raskrivanje koristeći RSA
            Console.WriteLine("\nPostupak raskrivanja poruke RSA sustavom sastoji se od:");
            Console.WriteLine("1. prvo faktoriziramo javni djelitelj (n),");
            Console.WriteLine("2. izračunavamo vrijednost Eulerovog toličnika po formuli: fi[n]=(p-1)(q-1)...,");
            Console.WriteLine("3. po formuli za definiranje odnosa javnog i tajnog ključa 'e*dmod(fi[n])=1' izračunamo vrijednost tajnog ključa (d),");
            Console.WriteLine("4. pronalazak indeksa svakog slova zakritka (A->0, B->1, ...),");
            Console.WriteLine("5. potenciranje tog indeksa vrijednošću tajnog ključa (d),");
            Console.WriteLine("6. završno, ostatak cijelobrojnog dijeljenja javnim djeliteljem (n) čini indeks slova zakritka.\n");
            #endregion

            RastavljanjeNaFaktore(n);
            while (faktori.Count()<2)
            {
                Console.WriteLine("Niste upisali valjani djelitelj (n).");
                UnosIProvjeraJavnogDjeliteljaN();
                RastavljanjeNaFaktore(n);
            }

            IzračunFin();
            IzračunTajnogKljučaD();
            if (d.ToString().Length == 0) { Console.WriteLine("Nisam uspio pronaći vrijednost tajnog ključa. :("); Console.WriteLine("Pokušajte ponovo."); }
            else
            {
                Console.WriteLine("d:{0}", d);
                //kreiranje jasnopisa
                foreach (string slovo in zakritak)
                {
                    try
                    {
                        //POPRAVI
                        Console.WriteLine("slovo jasnopisa: {0}", slovo);
                        int indeks = jasnopisniSlovored.IndexOf(slovo); //indeks slova jasnopisa
                        Console.WriteLine("Indeks jasnopisa: {0}", indeks);
                        indeks = (int)Math.Pow(indeks, d); //potenciranje indeksa s d
                        Console.WriteLine("potencirani indeks: {0}", indeks);
                        indeks %= n; //ostatak cijelobrojnog dijeljenja s n
                        Console.WriteLine("ostatak cjelobrojnog dijeljenja s n: {0}", indeks);
                        Console.WriteLine("dodavanje slova {0} u zakritak", jasnopisniSlovored[indeks]);
                        jasnopis.Add(jasnopisniSlovored[indeks]); //dodavanje slova raskritka
                    }
                    catch { jasnopis.Add(slovo); }
                    Console.WriteLine();
                }
            }
        }
    }
}
