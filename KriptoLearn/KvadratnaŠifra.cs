using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class KvadratnaŠifra
    {
        private List<string> abeceda = new List<string>() {"A", "B", "C", "Č", "Ć", "D", "Dž", "Đ", "E", "F", "G", "H", "I", "J", "K", "L", "Lj", "M", "N", "Nj", "O", "P", "R", "S", "Š", "T", "U", "V", "Z", "Ž"};
        List<string> jasnopis = new List<string>();
        public void Zakrij(List<string> ključ, List<string> poruka) //radi
        {
            List<string> noviKljuč = new List<string>();
            List<string> zakritak = new List<string>();

            //postupak zakrivanja kvadratnom šifrom
            Console.WriteLine("\nPostupak zakrivanja kvadratnom šifrom sastoji se od:");
            Console.WriteLine("1. Zapisivanja poruke,");
            Console.WriteLine("2. Ispod poruke zapisujemo slova ključa dok ne dođemo do kraja poruke,");
            Console.WriteLine("3. Zbrajamo cjelobrojne vrijednosti slova (indekse, A-0, Ž-29) koristeći modularnu aritmetiku (mod 30).\n");

            //upisivanje slova ključa dok ne dobijemo duljinu poruke
            int j = 0;
            for (int i = 0; i < poruka.Count(); i++)
            {
                if (abeceda.IndexOf(poruka[i]) > -1) { noviKljuč.Add(ključ[j % ključ.Count()]); }
                else { j--; noviKljuč.Add(poruka[i]); }
                j++;
            }

            //pribavljanje indeksa slova poruke, ključa te izračun indeksa zakritka
            for (int i = 0; i < poruka.Count(); i++)
            {
                if (abeceda.IndexOf(poruka[i]) > -1)
                {
                    int indeks1 = abeceda.IndexOf(poruka[i]);
                    int indeks2 = abeceda.IndexOf(noviKljuč[i]);
                    int indeks = (indeks1 + indeks2) % 30; //formula za izračun indeksa
                    zakritak.Add(abeceda[indeks]);
                }
                else { zakritak.Add(poruka[i]); }
            }

            Console.WriteLine("Ispis jasnopisa i ključa:");
            //ispis jasnopisa
            foreach (string slovo in poruka)
            {
                Console.Write(slovo);
            }
            Console.WriteLine();
            //ispis ključa
            foreach (string slovo in noviKljuč)
            {
                Console.Write(slovo);
            }
            Console.WriteLine();

            //ispis primjera izračuna
            Console.WriteLine("\nPrimjer izračuna nekoliko znakova zakritka:");
            int krajnjiIndeks;
            if (poruka.Count() < 3) { krajnjiIndeks = poruka.Count(); }
            else { krajnjiIndeks = 3; }
            for (int i = 0; i < krajnjiIndeks; i++)
            {
                Console.WriteLine("{0}->{1} + {2}->{3} mod(30) = {4}->{5}", poruka[i], abeceda.IndexOf(poruka[i]), noviKljuč[i], abeceda.IndexOf(noviKljuč[i]), abeceda.IndexOf(zakritak[i]), zakritak[i]);
            }

            //ispis zakritka
            Console.WriteLine("\nIspis zakritka:");
            foreach (string slovo in zakritak)
            {
                Console.Write(slovo);
            }
        }
        public void Raskrij(List<string> ključ, List<string> poruka) //radi
        {
            List<string> noviKljuč = new List<string>();
            List<string> jasnopis = new List<string>();

            //postupak raskrivanja kvadratnom šifrom
            Console.WriteLine("\nPostupak raskrivanja kvadratnom šifrom sastoji se od:");
            Console.WriteLine("1. Zapisivanja poruke,");
            Console.WriteLine("2. Ispod poruke zapisujemo slova ključa dok ne dođemo do kraja poruke,");
            Console.WriteLine("3. Oduzimamo cjelobrojne vrijednosti slova (indekse, A-0, Ž-29) te dodamo 30 (broj slova abecedi) i konačno mod(30). \n");

            //upisivanje slova ključa dok ne dobijemo duljinu poruke
            int j = 0;
            for (int i = 0; i < poruka.Count(); i++)
            {
                if (abeceda.IndexOf(poruka[i]) > -1) { noviKljuč.Add(ključ[j % ključ.Count()]); }
                else { j--; noviKljuč.Add(poruka[i]); }
                j++;
            }

            //pribavljanje indeksa slova zakritka, ključa te izračun indeksa jasnopisa
            for (int i = 0; i < poruka.Count(); i++)
            {
                if (abeceda.IndexOf(poruka[i]) > -1)
                {
                    int indeks1 = abeceda.IndexOf(poruka[i]);
                    int indeks2 = abeceda.IndexOf(noviKljuč[i]);
                    //formula za izračun indeksa
                    int indeks = indeks1 - indeks2;
                    if (indeks < 0) { indeks += 30; }
                    jasnopis.Add(abeceda[indeks]);
                }
                else { jasnopis.Add(poruka[i]); }
            }

            Console.WriteLine("\nIspis zakritka i ključa:");
            //ispis zakritka
            foreach (string slovo in poruka)
            {
                Console.Write(slovo);
            }
            Console.WriteLine();
            //ispis ključa
            foreach (string slovo in noviKljuč)
            {
                Console.Write(slovo);
            }
            Console.WriteLine();

            //ispis primjera izračuna
            Console.WriteLine("\nPrimjer izračuna nekoliko znakova raskritka:");
            int krajnjiIndeks;
            if (poruka.Count() < 3) { krajnjiIndeks = poruka.Count(); }
            else { krajnjiIndeks = 3; }
            for (int i = 0; i < krajnjiIndeks; i++)
            {
                Console.WriteLine("{0}->{1} - {2}->{3} mod(30) = {4}->{5}", poruka[i], abeceda.IndexOf(poruka[i]), noviKljuč[i], abeceda.IndexOf(noviKljuč[i]), abeceda.IndexOf(jasnopis[i]), jasnopis[i]);
            }

            //ispis jasnopisa
            Console.WriteLine("\nIspis jasnopisa:");
            foreach (string slovo in jasnopis)
            {
                Console.Write(slovo);
            }
        }
    }
}
