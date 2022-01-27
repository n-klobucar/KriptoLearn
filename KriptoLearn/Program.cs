using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class Program
    {
        /// <summary>
        /// Pomoć pri učenju kritopisnih sustava
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //unicode u konzoli
            Console.OutputEncoding = Encoding.UTF8;

            string poruka;
            string odgovor;
            List<string> provjereniKljuč = new List<string>();

            //funkcije
            string Odabir()
            {
                Console.WriteLine("Odaberite vrstu zakrivanja (z-zamjenski, p-premještajni, s-složeni, q-kvadratna šifra, d-DES, r-RSA, x-izlaz)");
                string odabir = Console.ReadLine();
                return odabir;
            }
            List<string> ProvjeraFormataUnosa(string tekst)
            {
                List<string> abeceda = new List<string>() {"A", "B", "C", "Č", "Ć", "D", "Dž", "Đ", "E", "F", "G", "H", "I", "J", "K",
                                            "L", "Lj", "M", "N", "Nj", "O", "P", "R", "S", "Š", "T", "U", "V", "Z", "Ž"};
                List<string> uređeniTekst = new List<string>();
                for (int i = 0; i < tekst.Length; i++)
                {
                    if (i != tekst.Length - 1) // TUUU
                    {
                        if (tekst[i + 1].ToString() == "ž" && tekst[i].ToString() == "D") { uređeniTekst.Add("Dž"); i++; }
                        else if (tekst[i + 1].ToString() == "j" && (tekst[i].ToString() == "N" || tekst[i].ToString() == "L")) { uređeniTekst.Add(tekst[i].ToString() + tekst[i + 1].ToString()); i++; }
                        else { uređeniTekst.Add(tekst[i].ToString()); }
                    }
                    else { uređeniTekst.Add(tekst[i].ToString()); }
                }
                bool greška = false;
                foreach (string slovo in uređeniTekst)
                {
                    if(!abeceda.Contains(slovo) && abeceda.Contains(slovo.ToUpper()))
                    {
                        greška = true;
                        break;
                    }
                }
                if (greška) { uređeniTekst.Clear(); }
                return uređeniTekst;
            }
            void UnosIProvjeraPoruke(KritopisniSustav kritopisniSustav, bool zakrivanje)
            {
                Console.Write("Unesite poruku: ");
                poruka = Console.ReadLine();

                if (zakrivanje)
                {
                    kritopisniSustav.jasnopis = ProvjeraFormataUnosa(poruka);
                    while (kritopisniSustav.jasnopis.Count() == 0)
                    {
                        Console.WriteLine("Pogreška u formatu poruke.");
                        Console.Write("Unesite poruku: ");
                        poruka = Console.ReadLine();
                        kritopisniSustav.jasnopis = ProvjeraFormataUnosa(poruka);
                    }
                }
                else
                {
                    kritopisniSustav.zakritak = ProvjeraFormataUnosa(poruka);
                    while (kritopisniSustav.zakritak.Count() == 0)
                    {
                        Console.WriteLine("Pogreška u formatu poruke.");
                        Console.Write("Unesite poruku: ");
                        poruka = Console.ReadLine();
                        kritopisniSustav.zakritak = ProvjeraFormataUnosa(poruka);
                    }
                }
            }
            List<string> UnosIProvjeraKljuča()
            {
                bool greška = true;
                do
                {
                    Console.Write("Unesite ključ: ");
                    string k = Console.ReadLine();
                    if (k.All(char.IsDigit) && int.Parse(k) < 30 && int.Parse(k) >= 0)
                    {
                        provjereniKljuč.Add(k);
                        greška = false;
                    }
                    else if (k.All(char.IsDigit)) { Console.WriteLine("Greška! Niste unijeli valjani broj. [0-29]"); continue; }
                    else
                    {
                        provjereniKljuč = ProvjeraFormataUnosa(k);
                        if (provjereniKljuč.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu ključa.");
                        }
                        else { greška = false; }
                    }
                } while (greška);
                return provjereniKljuč;
            }
            bool ProvjeraPostupka()
            {
                Console.Write("Upišite 'r' za raskrivanje ili 'z' za zakrivanje: ");
                string radnja = Console.ReadLine().ToLower();
                while (radnja != "r" && radnja != "z")
                {
                    Console.WriteLine("Niste unijeli valjani odabir.");
                    Console.Write("Upišite 'r' za raskrivanje ili 'z' za zakrivanje: ");
                    radnja = Console.ReadLine().ToLower();
                }
                if (radnja == "z") { return true; }
                else { return false; }
            }
            void IspisRješenja(bool zakrivanje, List<string> jasnopis, List<string> zakritak)
            {
                if (zakrivanje)
                {
                    Console.WriteLine("\nzakritak:");
                    foreach (string slovo in zakritak)
                    {
                        Console.Write(slovo);
                    }
                }
                else
                {
                    Console.WriteLine("\njasnopis:");
                    foreach (string slovo in jasnopis)
                    {
                        Console.Write(slovo);
                    }
                }
            }
            bool KorisnikOdustao(string unos)
            {
                if (unos.ToLower() == "x") { return true; }
                else { return false; }
            }
            bool ProvjeraVrstePremještajnog()
            {
                Console.Write("Upišite 'j' za jednostruki ili 'd' za dvostruki stupačni sustav: ");
                string vrsta = Console.ReadLine().ToLower();
                while (vrsta != "j" && vrsta != "d")
                {
                    Console.WriteLine("Pokušajte ponovo!");
                    Console.Write("Upišite 'j' za jednostruki ili 'd' za dvostruki stupačni sustav: ");
                    vrsta = Console.ReadLine().ToLower();
                }
                if (vrsta == "j") { Console.WriteLine("Odabrali ste jednostruki stupačni premještaj."); return false; }
                else { Console.WriteLine("Odabrali ste dvostruki stupačni premještaj."); return true; }
            }

            odgovor = Odabir().ToLower();
            do
            {
                switch (odgovor)
                {
                    #region Zamjenski
                    case "z":
                        Zamjenski zamjenski = new Zamjenski();
                        Console.WriteLine("Odabrali ste zamjenski kritopis.");
                        bool zakrivanje = ProvjeraPostupka();

                        UnosIProvjeraPoruke(zamjenski, zakrivanje);
                        if (KorisnikOdustao(poruka)) { odgovor = "k"; break; }

                        provjereniKljuč = UnosIProvjeraKljuča();
                        if (KorisnikOdustao(provjereniKljuč[0])) { odgovor = "k"; break; }
                        

                        Console.Write("Unesite broj ili ključ za pomak: ");
                        string unosZamjenski = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (unosZamjenski == "x" || unosZamjenski == "X")
                        {
                            odgovor = "k";
                            break;
                            int pomak = int.Parse(provjereniKljuč[0]);
                            if (zakrivanje) { zamjenski.ZakrijZamjenskim(zamjenski.jasnopis, pomak); }
                            else { zamjenski.RaskrijZamjenskim(zamjenski.zakritak, pomak); }
                        }
                        else
                        {
                            if (zakrivanje) { zamjenski.ZakrijZamjenskim(zamjenski.jasnopis, provjereniKljuč); }
                            else { zamjenski.RaskrijZamjenskim(zamjenski.zakritak, provjereniKljuč); }
                        }

                        IspisRješenja(zakrivanje, zamjenski.jasnopis, zamjenski.zakritak);

                        provjereniKljuč.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region Premještajni
                    case "p":
                        Premještajni premještajni = new Premještajni();
                        Console.WriteLine("Odabrali ste premještajni");

                        bool dvostruki = ProvjeraVrstePremještajnog();
                        zakrivanje = ProvjeraPostupka();

                        UnosIProvjeraPoruke(premještajni, zakrivanje);
                        if (KorisnikOdustao(poruka)) { odgovor = "k"; break; }

                        provjereniKljuč = UnosIProvjeraKljuča();
                        if (KorisnikOdustao(provjereniKljuč.First())) { odgovor = "k"; break; }

                        if (zakrivanje) { premještajni.ZakrijPremještajnim(provjereniKljuč, dvostruki); }
                        else { premještajni.RaskrijPremještajnim(provjereniKljuč, dvostruki); }

                        provjereniKljuč.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region Složeni
                    case "s":
                        Složeni složeni = new Složeni();
                        Console.WriteLine("Odabrali ste složeni");

                        zakrivanje = ProvjeraPostupka();

                        UnosIProvjeraPoruke(složeni, zakrivanje);
                        if (KorisnikOdustao(poruka)) { odgovor = "k"; break; }

                        provjereniKljuč = UnosIProvjeraKljuča();
                        if (KorisnikOdustao(provjereniKljuč.First())) { odgovor = "k"; break; }

                        if (zakrivanje) { složeni.ZakrijSloženim(provjereniKljuč); IspisRješenja(zakrivanje, složeni.jasnopis, složeni.zakritak); }
                        else { složeni.RaskrijSloženim(provjereniKljuč); }

                        provjereniKljuč.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region Kvadratna šifra
                    case "q":
                        KvadratnaŠifra kvadratnaŠifra = new KvadratnaŠifra();
                        Console.WriteLine("Odabrali ste kvadratnu šifru.");

                        zakrivanje = ProvjeraPostupka();

                        UnosIProvjeraPoruke(kvadratnaŠifra, zakrivanje);
                        if (KorisnikOdustao(poruka)) { odgovor = "k"; break; }

                        provjereniKljuč = UnosIProvjeraKljuča();
                        if (KorisnikOdustao(provjereniKljuč.First())) { odgovor = "k"; break; }

                        if (zakrivanje) { kvadratnaŠifra.ZakrijKvadratnomŠifrom(provjereniKljuč,zakrivanje); }
                        else { kvadratnaŠifra.RaskrijKvadratnomŠifrom(provjereniKljuč, zakrivanje); }

                        IspisRješenja(zakrivanje, kvadratnaŠifra.jasnopis, kvadratnaŠifra.zakritak);

                        provjereniKljuč.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region DES
                    case "d":
                        Des des = new Des();
                        Console.WriteLine("Odabrali ste DES.");

                        zakrivanje = ProvjeraPostupka();

                        Console.Write("Unesite poruku: ");
                        poruka = Console.ReadLine();

                        if (KorisnikOdustao(poruka)) { odgovor = "k"; break; }

                        if (zakrivanje) { des.Zakrij(poruka); }
                        else 
                        {
                            Console.Write("Unesite ključ: ");
                            des.sKljuč = Console.ReadLine();
                            Console.Write("Unesite IV: ");
                            des.sIV = Console.ReadLine();
                            des.Raskrij(poruka); 
                        }

                        IspisRješenja(zakrivanje, des.jasnopis, des.zakritak);

                        odgovor = "k";
                        break;
                    #endregion

                    #region RSA
                    case "r":
                        RSA rsa = new RSA();
                        Console.WriteLine("Odabrali ste RSA.");

                        rsa.UnosIProvjeraJavnogKljučaE();
                        rsa.UnosIProvjeraJavnogDjeliteljaN();

                        zakrivanje = ProvjeraPostupka();
                        UnosIProvjeraPoruke(rsa, zakrivanje);
                        if (KorisnikOdustao(poruka)) { odgovor = "k"; break; }
                        if (zakrivanje) { rsa.ZakrijRSA(); }
                        else { rsa.RaskrijRSA(); }
                        IspisRješenja(zakrivanje, rsa.jasnopis, rsa.zakritak);

                        provjereniKljuč.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region kraj
                    case "k":
                        Console.WriteLine("\n");
                        odgovor = Odabir().ToLower();
                        break;
                    #endregion

                    #region izlaz
                    case "x":
                        odgovor = "k";
                        break;
                    #endregion

                    #region default
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Pokušajte ponovno.");
                        odgovor = Odabir().ToLower();
                        break;
                        #endregion
                }
            } while (odgovor != "x");
            Console.WriteLine("\n\nPozdrav!");
        }
    }
}
