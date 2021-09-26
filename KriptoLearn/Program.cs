using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace KriptoLearn
{
    class Program
    {
        /// <summary>
        /// Pomoć pri učenju kritopisnih sustava
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //unicode u konzoli
            Console.OutputEncoding = Encoding.UTF8;

            //deklaracija varijabli
            List<string> abeceda = new List<string>() {"A", "B", "C", "Č", "Ć", "D", "Dž", "Đ", "E", "F", "G", "H", "I", "J", "K",
                                            "L", "Lj", "M", "N", "Nj", "O", "P", "R", "S", "Š", "T", "U", "V", "Z", "Ž"};
            List<string> poruka = new List<string>();
            List<string> ključ = new List<string>();
            string jasnopis;

            //funkcije
            string Odabir()
            {
                Console.WriteLine("Odaberite vrstu zakrivanja (z-zamjenski, p-premještajni, s-složeni, q-kvadratna šifra, d-DES*, r-RSA, x-izlaz)");
                string odabir = Console.ReadLine();
                return odabir;
            }
            List<string> NormalizacijaDvoglasa(string tekst)
            {
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
            string provjeraPostupka()
            {
                Console.Write("Upišite 'r' za raskrivanje ili 'z' za zakrivanje: ");
                string radnja = Console.ReadLine().ToLower();
                while (radnja != "r" && radnja != "z")
                {
                    Console.WriteLine("Niste unijeli valjani odabir.");
                    Console.Write("Upišite 'r' za raskrivanje ili 'z' za zakrivanje: ");
                    radnja = Console.ReadLine().ToLower();
                }
                return radnja;
            }

            string odgovor = Odabir().ToLower();
            while (odgovor != "x")
            {
                switch (odgovor)
                {
                    #region Zamjenski
                    case "z":
                        Zamjenski zamjenski = new Zamjenski();
                        Console.WriteLine("Odabrali ste zamjenski kritopis.");

                        string raskrivanje = provjeraPostupka();

                        Console.Write("Unesite poruku: ");
                        jasnopis = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (jasnopis == "x" || jasnopis == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        poruka = NormalizacijaDvoglasa(jasnopis);
                        while (poruka.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu poruke.");
                            Console.Write("Unesite poruku: ");
                            jasnopis = Console.ReadLine();
                            poruka = NormalizacijaDvoglasa(jasnopis);
                        }
                        //provjera ključa
                        Console.Write("Unesite broj ili ključ za pomak: ");
                        string unosZamjenski = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (unosZamjenski == "x" || unosZamjenski == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        bool greška = true;
                        while (greška)
                        {
                            if (unosZamjenski.All(char.IsDigit)) //ako je unos brojčani
                            {
                                int pomak = int.Parse(unosZamjenski);
                                if (pomak > 30 || pomak < 0)
                                {
                                    Console.WriteLine("Niste unijeli valjani broj. [0-29]");
                                    Console.Write("Unesite broj ili ključ za pomak: ");
                                    unosZamjenski = Console.ReadLine();
                                }
                                else
                                {
                                    greška = false;
                                    zamjenski.ZamijeniBrojem(pomak, poruka, raskrivanje);
                                }
                            }
                            else if (unosZamjenski.All(char.IsLetter)) //ako su uneseni samo znakovi
                            {
                                ključ = NormalizacijaDvoglasa(unosZamjenski);
                                while (ključ.Count() == 0)
                                {
                                    Console.WriteLine("Pogreška u formatu ključa.");
                                    Console.Write("Unesite broj ili ključ za pomak: ");
                                    unosZamjenski = Console.ReadLine();
                                    ključ = NormalizacijaDvoglasa(unosZamjenski);
                                }
                                greška = false;
                                zamjenski.ZamijeniKljučem(ključ, poruka, raskrivanje);
                            }
                            else //ako je unesena kombinacija slova i brojeva
                            {
                                Console.WriteLine("Niste unijeli dobar ključ.");
                                Console.Write("Unesite broj ili ključ za pomak: ");
                                unosZamjenski = Console.ReadLine();
                            }
                        }
                        ključ.Clear();
                        poruka.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region Premještajni
                    case "p":
                        Premještajni premještajni = new Premještajni();
                        Console.WriteLine("Odabrali ste premještajni");
                        Console.Write("Upišite 'j' za jednostruki ili 'd' za dvostruki stupačni sustav: ");
                        string vrsta = Console.ReadLine().ToLower();
                        //korisnik odustaje od programa
                        if (vrsta == "x" || vrsta == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        while (vrsta != "j" && vrsta != "d")
                        {
                            Console.WriteLine("Pokušajte ponovo!");
                            Console.Write("Upišite 'j' za jednostruki ili 'd' za dvostruki stupačni sustav: ");
                            vrsta = Console.ReadLine().ToLower();
                        }
                        if (vrsta == "j") { Console.WriteLine("Odabrali ste jednostruki stupačni premještaj."); }
                        else { Console.WriteLine("Odabrali ste dvostruki stupačni premještaj."); }

                        string postupak = provjeraPostupka(); //raskrivanje ili zakrivanje

                        Console.Write("Unesite poruku: ");
                        jasnopis = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (jasnopis == "x" || jasnopis == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        poruka = NormalizacijaDvoglasa(jasnopis);
                        while (poruka.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu poruke.");
                            Console.Write("Unesite poruku: ");
                            jasnopis = Console.ReadLine();
                            poruka = NormalizacijaDvoglasa(jasnopis);
                        }

                        //string ključPremještajni = provjeraKljuča();
                        Console.Write("Unesite ključ: ");
                        string ključPremještajni = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (ključPremještajni == "x" || ključPremještajni == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        ključ = NormalizacijaDvoglasa(ključPremještajni);
                        while (ključ.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu ključa.");
                            Console.Write("Unesite ključ: ");
                            ključPremještajni = Console.ReadLine();
                            ključ = NormalizacijaDvoglasa(ključPremještajni);
                        }

                        if (postupak == "z")
                        {
                            if (vrsta == "d") { premještajni.Zakrij(ključ, poruka, true); }
                            else { premještajni.Zakrij(ključ, poruka); }
                        }
                        else
                        {
                            if (vrsta == "d") { premještajni.Raskrij(ključ, poruka, true); }
                            else { premještajni.Raskrij(ključ, poruka); }
                        }
                        poruka.Clear();
                        ključ.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region Složeni
                    case "s":
                        Složeni složeni = new Složeni();
                        Console.WriteLine("Odabrali ste složeni");

                        string radnja = provjeraPostupka();

                        Console.Write("Unesite poruku: ");
                        jasnopis = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (jasnopis == "x" || jasnopis == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        poruka = NormalizacijaDvoglasa(jasnopis);
                        while (poruka.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu poruke.");
                            Console.Write("Unesite poruku: ");
                            jasnopis = Console.ReadLine();
                            poruka = NormalizacijaDvoglasa(jasnopis);
                        }

                        Console.Write("Unesite ključ: ");
                        string ključSloženi = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (ključSloženi == "x" || ključSloženi == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        ključ = NormalizacijaDvoglasa(ključSloženi);
                        while (ključ.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu ključa.");
                            Console.Write("Unesite ključ: ");
                            ključSloženi = Console.ReadLine();
                            ključ = NormalizacijaDvoglasa(ključSloženi);
                        }

                        if (radnja == "z") { složeni.Zakrij(ključ, poruka, ""); }
                        else { složeni.Raskrij(ključ, poruka, ""); }

                        ključ.Clear();
                        poruka.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region Kvadratna šifra
                    case "q":
                        KvadratnaŠifra kvadratnaŠifra = new KvadratnaŠifra();
                        Console.WriteLine("Odabrali ste kvadratnu šifru.");

                        string odabir = provjeraPostupka();

                        Console.Write("Unesite poruku: ");
                        jasnopis = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (jasnopis == "x" || jasnopis == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        poruka = NormalizacijaDvoglasa(jasnopis);
                        while (poruka.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu poruke.");
                            Console.Write("Unesite poruku: ");
                            jasnopis = Console.ReadLine();
                            poruka = NormalizacijaDvoglasa(jasnopis);
                        }

                        Console.Write("Unesite ključ: ");
                        string ključKvadratnaŠifra = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (ključKvadratnaŠifra == "x" || ključKvadratnaŠifra == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        ključ = NormalizacijaDvoglasa(ključKvadratnaŠifra);
                        while (ključ.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu ključa.");
                            Console.Write("Unesite ključ: ");
                            ključKvadratnaŠifra = Console.ReadLine();
                            ključ = NormalizacijaDvoglasa(ključKvadratnaŠifra);
                        }

                        if (odabir == "z") { kvadratnaŠifra.Zakrij(ključ, poruka); }
                        else { kvadratnaŠifra.Raskrij(ključ, poruka); }

                        ključ.Clear();
                        poruka.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    //#region DES
                    case "d":
                    #region stari des
                    //DES des = new DES();
                    //Console.WriteLine("Odabrali ste DES.");

                    ////zakrivanje ili raskrivanje
                    //odabir = provjeraPostupka();

                    //Console.Write("Unesite poruku: ");
                    //jasnopis = Console.ReadLine();
                    ////korisnik odustaje od programa
                    //if (jasnopis == "x" || jasnopis == "X")
                    //{
                    //    odgovor = "k";
                    //    break;
                    //}
                    //poruka = NormalizacijaDvoglasa(jasnopis);
                    //bool ascii = false;
                    //foreach (string slovo in poruka)
                    //{
                    //    if (slovo == "Č" || slovo == "Ć"  || slovo == "Đ" || slovo == "Dž" || slovo == "Nj" || slovo == "Lj" || slovo == "Š" || slovo == "Ž")
                    //    {
                    //        ascii = true;
                    //    }
                    //}
                    //if (ascii) { poruka.Clear(); }
                    //while (poruka.Count() == 0)
                    //{
                    //    Console.WriteLine("Pogreška u formatu poruke.");
                    //    Console.Write("Unesite poruku: ");
                    //    jasnopis = Console.ReadLine();
                    //    poruka = NormalizacijaDvoglasa(jasnopis);
                    //}
                    //des.Zakrij(poruka);
                    #endregion

                    #region DES
                    //DES des = DES.Create();
                    //Console.WriteLine("Odabrali ste DES.");

                    ////zakrivanje ili raskrivanje
                    //odabir = provjeraPostupka();

                    //Console.Write("Unesite poruku koristeći slova engleske abecede: ");
                    //jasnopis = Console.ReadLine();

                    ////korisnik odustaje od programa
                    //if (jasnopis == "x" || jasnopis == "X")
                    //{
                    //    odgovor = "k";
                    //    break;
                    //}

                    ////provjera slova engleske abecede
                    //poruka = NormalizacijaDvoglasa(jasnopis);
                    //bool ascii = false;
                    //foreach (string slovo in poruka)
                    //{
                    //    if (slovo == "Č" || slovo == "Ć" || slovo == "Đ" || slovo == "Dž" || slovo == "Nj" || slovo == "Lj" || slovo == "Š" || slovo == "Ž")
                    //    {
                    //        ascii = true;
                    //    }
                    //}
                    //if (ascii) { poruka.Clear(); } //ako postoje slova koja nisu ASCII, onda obriši cijelu poruku

                    //while (poruka.Count() == 0)
                    //{
                    //    Console.WriteLine("Pogreška u formatu poruke.");
                    //    Console.Write("Unesite poruku koristeći SAMO znakove engleske abecede: ");
                    //    jasnopis = Console.ReadLine();
                    //    poruka = NormalizacijaDvoglasa(jasnopis);
                    //}

                    ////pretvaranje poruke iz liste u string
                    //string porukaString = "";
                    //foreach (string slovo in poruka)
                    //{
                    //    porukaString += slovo;
                    //}

                    ////ako je odabrano zakrivanje
                    //if(odabir == "z")
                    //{
                    //    byte[] porukaPodaci = Des.EncryptTextToMemory(porukaString, des.Key, des.IV);

                    //    string zakritak = string.Join(", ", porukaPodaci);
                    //    string ključDES = string.Join("",des.Key);

                    //    Console.WriteLine("Ispis ključa: {0}", ključDES);
                    //    Console.WriteLine("Ispis zakritka:");
                    //    Console.WriteLine(zakritak);
                    //}
                    //else
                    //{
                    //    Console.Write("Unesite ključ: ");
                    //    string ključDES = Console.ReadLine();
                    //    byte[] ključdes = Encoding.UTF8.GetBytes(ključDES);
                    //    des.Key = ključdes;

                    //    byte[] porukaPodaci = Encoding.ASCII.GetBytes(porukaString);
                    //    string raskritak = Des.DecryptTextFromMemory(porukaPodaci, des.Key, des.IV);
                    //    Console.WriteLine("Ispis raskritka: {0}", raskritak);
                    //    //string raskritak=Des.DecryptTextFromMemory(porukaPodaci, )
                    //}
                    //poruka.Clear();
                    //odgovor = "k";
                    //break;
                    #endregion

                    #region novi DES

                    #endregion

                    #region RSA
                    case "r":
                        RSA rsa = new RSA();
                        Console.WriteLine("Odabrali ste RSA.");
                        //unos javnog ključa
                        Console.Write("Unesite vrijednost javnog ključa (e): ");
                        string proizvoljnoe = Console.ReadLine();
                        while (!proizvoljnoe.All(char.IsDigit) || proizvoljnoe.Length==0)
                        {
                            Console.WriteLine("Niste unijeli broj.");
                            Console.Write("Unesite vrijednost javnog ključa (e): ");
                            proizvoljnoe = Console.ReadLine();
                        }
                        rsa.e = int.Parse(proizvoljnoe);

                        //unos javnog djelitelja
                        Console.Write("Unesite vrijednost javnog djelitelja (n): ");
                        string proizvoljnon = Console.ReadLine();
                        while (!proizvoljnon.All(char.IsDigit) && int.Parse(proizvoljnon) < 3)
                        {
                            Console.WriteLine("Niste unijeli valjanu vrijednost. [>3]");
                            Console.Write("Unesite vrijednost javnog djelitelja (n): ");
                            proizvoljnon = Console.ReadLine();
                        }
                        rsa.n = int.Parse(proizvoljnon);

                        //zakrivanje ili raskrivanje
                        odabir = provjeraPostupka();

                        //unos poruke
                        Console.Write("Unesite poruku: ");
                        jasnopis = Console.ReadLine();
                        //korisnik odustaje od programa
                        if (jasnopis == "x" || jasnopis == "X")
                        {
                            odgovor = "k";
                            break;
                        }
                        poruka = NormalizacijaDvoglasa(jasnopis);
                        while (poruka.Count() == 0)
                        {
                            Console.WriteLine("Pogreška u formatu poruke.");
                            Console.Write("Unesite poruku: ");
                            jasnopis = Console.ReadLine();
                            poruka = NormalizacijaDvoglasa(jasnopis);
                        }
                        if (odabir == "z") { rsa.Zakrij(poruka); }
                        else { rsa.Raskrij(poruka); }

                        poruka.Clear();
                        odgovor = "k";
                        break;
                    #endregion

                    #region kraj
                    case "k":
                        Console.WriteLine("\n");
                        odgovor = Odabir().ToLower();
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
            }
            Console.WriteLine("Pozdrav!");
            
        }
    }
}
