using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class Zamjenski : KritopisniSustav
    {
        public List<string> zakritniSlovored = new List<string>();

        //functions
        List<string> KreirajZakritniSlovoredBrojem(List<string> jasnopisniSlovored, int broj)
        {
            Console.WriteLine("\njasnopisni slovored:");
            foreach (string slovo in jasnopisniSlovored)
            {
                Console.Write(slovo + " ");
            }
            Console.WriteLine();
            Console.WriteLine("\nZakritni slovored se dobije pomakom jasnopisng slovoreda za {0} slova.\n", broj);
            Console.WriteLine("zakritni slovored:");
            foreach (string slovo in jasnopisniSlovored.Skip(broj))
            {
                Console.Write(slovo + " ");
                zakritniSlovored.Add(slovo);
            }
            foreach (string slovo in jasnopisniSlovored.Take(broj))
            {
                Console.Write(slovo + " ");
                zakritniSlovored.Add(slovo);
            }
            return zakritniSlovored;
        }
        List<string> KreirajZakritniSlovoredKljučem(List<string> jasnopisniSlovored, List<string> pomak)
        {
            int brZnakova = pomak.Distinct().Count();
            List<string> privremeniZakritni = new List<string>();
            List<string> privremeniJasnopisni = new List<string>();

            Console.WriteLine("\nPostupak kreiranja kritopisnog slovoreda koristeći ključ je sljedeći:");
            Console.WriteLine("1. zapišemo ključ bez duplikata (npr. 'krava'->'krav'),");
            Console.WriteLine("2. u te stupce dopisujemo ostala slova abecede abecednim redom,");
            Console.WriteLine("3. zapisujemo slova čitajući ih odozgo prema dolje (stupac po stupac, a ne lijevo-desno).");

            Console.WriteLine("\nprivremeni slovored:");
            foreach (string slovo in jasnopisniSlovored)
            {
                privremeniJasnopisni.Add(slovo);
            }
            foreach (string slovo in pomak.Distinct())
            {
                privremeniZakritni.Add(slovo);
                privremeniJasnopisni.Remove(slovo);
            }
            foreach (string slovo in privremeniJasnopisni)
            {
                privremeniZakritni.Add(slovo);
            }
            for (int i = 0; i < privremeniZakritni.Count(); i++)
            {
                Console.Write(privremeniZakritni[i] + " ");
                if ((i + 1) % brZnakova == 0 && i > 0)
                {
                    Console.WriteLine();
                }
            }
            int brRedaka = privremeniZakritni.Count() % brZnakova == 0 ? privremeniZakritni.Count() / brZnakova : privremeniZakritni.Count() / brZnakova + 1;
            for (int i = 0; i < brZnakova; i++)
            {
                for (int j = 0; j < brRedaka; j++)
                {
                    try
                    {
                        zakritniSlovored.Add(privremeniZakritni.ElementAt(j * brZnakova + i));
                    }
                    catch { continue; }
                }
            }
            Console.WriteLine("\n\njasnopisni slovored:");
            foreach (string slovo in jasnopisniSlovored)
            {
                Console.Write(slovo + " ");
            }
            Console.WriteLine("\nzakritni slovored:");
            foreach (string slovo in zakritniSlovored)
            {
                Console.Write(slovo + " ");
            }
            return zakritniSlovored;
        }
        void KreirajZakritakZamjenskim()
        {
            foreach (string slovo in jasnopis)
            {
                try
                {
                    int indeks = jasnopisniSlovored.IndexOf(slovo);
                    zakritak.Add(zakritniSlovored.ElementAt(indeks));
                }
                catch (Exception)
                {
                    zakritak.Add(slovo);
                }
            }
            Console.WriteLine("\n\nZakritak se dobije tako da se svako slovo jasnopisa zamijeni pripadajućim slovom kritopisnog slovoreda, a interpunkcije i ostali posebni znakovi se samo prepišu.\n");
        }
        public List<string> ZakrijZamjenskim(List<string> jasnopis, int broj)
        {
            List<string> zakritniSlovored = KreirajZakritniSlovoredBrojem(jasnopisniSlovored, broj);
            
            KreirajZakritakZamjenskim();
            return zakritak;
        }
        public List<string> ZakrijZamjenskim(List<string> poruka, List<string> pomak)
        {
            List<string> zakritniSlovored = KreirajZakritniSlovoredKljučem(jasnopisniSlovored, pomak);

            KreirajZakritakZamjenskim();
            return zakritak;
        }
        public List<string> RaskrijZamjenskim(List<string> poruka, int broj)
        {
            List<string> zakritniSlovored = KreirajZakritniSlovoredBrojem(jasnopisniSlovored, broj);

            return zakritak;
        }
        public List<string> RaskrijZamjenskim(List<string> poruka, List<string> pomak)
        {
            List<string> zakritniSlovored = KreirajZakritniSlovoredKljučem(jasnopisniSlovored, pomak);

            return zakritak;
        }

        //public void Zakrij(List<string> kritopisniSlovored, List<string> poruka) //radi
        //{
        //    string zakritak = "";
        //    Console.WriteLine();
        //    //ispis zakritka
        //    foreach (string slovo in poruka)
        //    {
        //        try
        //        {
        //            //Console.WriteLine(slovo);
        //            int indeks = jasnopisniSlovored.IndexOf(slovo);
        //            //Console.WriteLine("indeks: {0}", indeks);
        //            zakritak += kritopisniSlovored.ElementAt(indeks);
        //        }
        //        catch (Exception)
        //        {
        //            //Console.WriteLine("u exceptionu sam za posebne znakove.");
        //            zakritak += slovo;
        //        }
        //    }
        //    Console.WriteLine("\nZakritak se dobije tako da se svako slovo jasnopisa zamijeni pripadajućim slovom kritopisnog slovoreda, a interpunkcije i ostali posebni znakovi se samo prepišu.");
        //    Console.WriteLine("\nZakritak glasi:");
        //    Console.Write(zakritak);
        //}
        //public void Raskrij(List<string> kritopisniSlovored, List<string> poruka, bool složeni = false) //radi
        //{
        //    Console.WriteLine();
        //    foreach (string slovo in poruka)
        //    {
        //        try
        //        {
        //            //Console.WriteLine(slovo);
        //            int indeks = kritopisniSlovored.IndexOf(slovo);
        //            //Console.WriteLine("indeks: {0}", indeks);
        //            jasnopis.Add(jasnopisniSlovored.ElementAt(indeks));
        //        }
        //        catch (Exception)
        //        {
        //            //Console.WriteLine("u exceptionu sam za posebne znakove.");
        //            jasnopis.Add(slovo);
        //        }
        //    }
        //    if (!složeni)
        //    {
        //        Console.WriteLine("\nJasnopis se dobije tako da se svako slovo zakritka zamijeni pripadajućim slovom jasnopisnog slovoreda, a ostali posebni znakovi se samo prepišu.");
        //        Console.WriteLine("\nJasnopis glasi:");
        //        foreach (string slovo in jasnopis)
        //        {
        //            Console.Write(slovo);
        //        }
        //    }
        //}
    }
}
