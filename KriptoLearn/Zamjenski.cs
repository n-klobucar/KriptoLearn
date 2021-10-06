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
        void KreirajZakritniSlovoredBrojem(int broj)
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
        }
        void KreirajZakritniSlovoredKljučem(List<string> pomak)
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
        void KreirajJasnopisZamjenskim()
        {
            foreach (string slovo in zakritak)
            {
                try
                {
                    int indeks = zakritniSlovored.IndexOf(slovo);
                    jasnopis.Add(jasnopisniSlovored.ElementAt(indeks));
                }
                catch (Exception)
                {
                    jasnopis.Add(slovo);
                }
            }
            Console.WriteLine("\nJasnopis se dobije tako da se svako slovo zakritka zamijeni pripadajućim slovom jasnopisnog slovoreda, a ostali posebni znakovi se samo prepišu.");
        }
        public void ZakrijZamjenskim(List<string> poruka, int pomak)
        {
            KreirajZakritniSlovoredBrojem(pomak);
            KreirajZakritakZamjenskim();
        }
        public void ZakrijZamjenskim(List<string> poruka, List<string> pomak)
        {
            KreirajZakritniSlovoredKljučem(pomak);
            KreirajZakritakZamjenskim();
        }
        public void RaskrijZamjenskim(List<string> poruka, int broj)
        {
            KreirajZakritniSlovoredBrojem(broj);
            KreirajJasnopisZamjenskim();
        }
        public void RaskrijZamjenskim(List<string> poruka, List<string> pomak)
        {
            KreirajZakritniSlovoredKljučem(pomak);
            KreirajJasnopisZamjenskim();
        }
    }
}
