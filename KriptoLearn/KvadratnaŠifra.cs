using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class KvadratnaŠifra : KritopisniSustav
    {
        List<string> noviKljuč = new List<string>();
        private void IspisNiza(List<string> niz)
        {
            foreach (string slovo in niz)
            {
                Console.Write(slovo + " ");
            }
            Console.WriteLine();
        }
        private void KreiranjeNovogKljuča(List<string> poruka, List<string> ključ)
        {
            noviKljuč.Clear();
            //upisivanje slova ključa dok ne dobijemo duljinu poruke
            int j = 0;
            for (int i = 0; i < poruka.Count(); i++)
            {
                if (jasnopisniSlovored.IndexOf(poruka[i]) > -1) { noviKljuč.Add(ključ[j % ključ.Count()]); j++; }
                else { noviKljuč.Add(poruka[i]); }
            }
        }
        private void IzračunIndeksa(List<string> poruka, bool zakrivanje)
        {
            for (int i = 0; i < poruka.Count(); i++)
            {
                if (jasnopisniSlovored.IndexOf(poruka[i]) > -1)
                {
                    int indeks;
                    int indeks1 = jasnopisniSlovored.IndexOf(poruka[i]);
                    Console.WriteLine("{0}:{1}", poruka[i], noviKljuč[i]);
                    int indeks2 = jasnopisniSlovored.IndexOf(noviKljuč[i]); //NE RADI
                    if (zakrivanje) { indeks = (indeks1 + indeks2) % 30; }
                    else
                    {
                        indeks = indeks1 - indeks2;
                        if (indeks < 0) { indeks += 30; }
                    }
                    zakritak.Add(jasnopisniSlovored[indeks]);
                }
                else { zakritak.Add(poruka[i]); }
            }
        }
        private void IspisPrimjeraIzračuna(List<string> poruka, bool zakrivanje)
        {
            Console.WriteLine("\nPrimjer izračuna nekoliko znakova:");
            int krajnjiIndeks;
            if (poruka.Count() < 3) { krajnjiIndeks = poruka.Count(); }
            else { krajnjiIndeks = 3; }
            for (int i = 0; i < krajnjiIndeks; i++)
            {
                if (zakrivanje) { Console.WriteLine("{0}->{1} + {2}->{3} mod(30) = {4}->{5}", poruka[i], jasnopisniSlovored.IndexOf(poruka[i]), noviKljuč[i], jasnopisniSlovored.IndexOf(noviKljuč[i]), jasnopisniSlovored.IndexOf(zakritak[i]), zakritak[i]); }
                else { Console.WriteLine("{0}->{1} - {2}->{3} mod(30) = {4}->{5}", poruka[i], jasnopisniSlovored.IndexOf(poruka[i]), noviKljuč[i], jasnopisniSlovored.IndexOf(noviKljuč[i]), jasnopisniSlovored.IndexOf(jasnopis[i]), jasnopis[i]); }
            }
        }
        public void ZakrijKvadratnomŠifrom(List<string> ključ, bool zakrivanje)
        {
            #region Upute
            Console.WriteLine("\nPostupak zakrivanja kvadratnom šifrom sastoji se od:");
            Console.WriteLine("1. Zapisivanja poruke,");
            Console.WriteLine("2. Ispod poruke zapisujemo slova ključa dok ne dođemo do kraja poruke,");
            Console.WriteLine("3. Zbrajamo cjelobrojne vrijednosti slova (indekse, A-0, Ž-29) koristeći modularnu aritmetiku (mod 30).\n");
            #endregion

            KreiranjeNovogKljuča(jasnopis, ključ);
            IzračunIndeksa(jasnopis, zakrivanje);

            Console.WriteLine("Ispis jasnopisa i ključa:");
            IspisNiza(jasnopis);
            IspisNiza(noviKljuč);

            IspisPrimjeraIzračuna(jasnopis, zakrivanje);

            //noviKljuč.Clear();
        }
        public void RaskrijKvadratnomŠifrom(List<string> ključ, bool zakrivanje)
        {
            #region Upute
            //postupak raskrivanja kvadratnom šifrom
            Console.WriteLine("\nPostupak raskrivanja kvadratnom šifrom sastoji se od:");
            Console.WriteLine("1. Zapisivanja poruke,");
            Console.WriteLine("2. Ispod poruke zapisujemo slova ključa dok ne dođemo do kraja poruke,");
            Console.WriteLine("3. Oduzimamo cjelobrojne vrijednosti slova (indekse, A-0, Ž-29) te dodamo 30 (broj slova abecedi) i konačno mod(30). \n");
            #endregion

            KreiranjeNovogKljuča(zakritak, ključ);
            //test
            IspisNiza(zakritak);
            IspisNiza(noviKljuč);

            IzračunIndeksa(zakritak, zakrivanje);

            Console.WriteLine("Ispis zakritka i ključa:");
            IspisNiza(zakritak);
            IspisNiza(noviKljuč);

            IspisPrimjeraIzračuna(zakritak, zakrivanje);
            //noviKljuč.Clear();
        }
    }
}
