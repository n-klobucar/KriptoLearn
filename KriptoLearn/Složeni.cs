//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KriptoLearn
//{
//    class Složeni : KritopisniSustav
//    {
//        private List<string> zakritniSlovored = new List<string>();

//        public void Zakrij(List<string> ključ, List<string> poruka, string opcija = "") //radi
//        {
//            //upute za zakrivanje složenim kritopisom
//            Console.WriteLine("\nPostupak zakrivanja poruke složenim kritopisnim sustavom sastoji se od:");
//            Console.WriteLine("1. Kreiranje kritopisnog slovoreda zamjenskim kritopisnim sustavom,");
//            Console.WriteLine("2. Kreiranje privremenog zakritka premještajnim kritopisnim sustavom,");
//            Console.WriteLine("3. Te završno zakrivanje privremenog zakritka novim kritopisnim slovoredom koristeći zamjenski sustav.");
//            //stvaram zakritni slovored iz ključa
//            Zamjenski zamjenski = new Zamjenski();
//            zamjenski.ZamijeniKljučem(ključ, poruka, opcija);
//            foreach (string slovo in zamjenski.zakritniSlovored)
//            {
//                zakritniSlovored.Add(slovo);
//            }

//            //stvaram premještenu poruku
//            Premještajni premještajni = new Premještajni();
//            premještajni.Zakrij(ključ, poruka, false, true);
//            poruka.Clear();
//            for (int i = 0; i < premještajni.premještenaPoruka.Count(); i++)
//            {
//                if (i != 0 && i % 5 == 0) { poruka.Add(" "); }
//                poruka.Add(premještajni.premještenaPoruka[i]);
//            }
//            zamjenski.Zakrij(zakritniSlovored, poruka);
//            zakritniSlovored.Clear();
//        }
//        public void Raskrij(List<string> ključ, List<string> poruka, string opcija = "") //radi
//        {
//            //upute za raskrivanje složenim kritopisom
//            Console.WriteLine("\nPostupak raskrivanja poruke složenim kritopisnim sustavom sastoji se od:");
//            Console.WriteLine("1. Kreiranje kritopisnog slovoreda zamjenskim kritopisnim sustavom,");
//            Console.WriteLine("2. Raskrivanje zakritka koristeći kreirani slovored,");
//            Console.WriteLine("3. Te završno raskrivanje dobivene poruke premještajnim sustavom.");
//            //raskrivanje poruke zamjenskim kritopisom
//            Zamjenski zamjenski = new Zamjenski();
//            zamjenski.ZamijeniKljučem(ključ, poruka, opcija);
//            foreach (string slovo in zamjenski.zakritniSlovored)
//            {
//                zakritniSlovored.Add(slovo);
//            }
//            zamjenski.Raskrij(zakritniSlovored, poruka, true);
//            poruka.Clear();
//            foreach (string slovo in zamjenski.jasnopis)
//            {
//                poruka.Add(slovo);
//            }

//            //raskrivanje premještajnim kritopisom
//            Premještajni premještajni = new Premještajni();
//            premještajni.Raskrij(ključ, poruka, false, true);
//        }
//    }
//}
