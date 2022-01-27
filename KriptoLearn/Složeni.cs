using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class Složeni : KritopisniSustav
    {
        Zamjenski zamjenski = new Zamjenski();
        Premještajni premještajni = new Premještajni();

        private void ČišćenjeFieldova()
        {
            premještajni.jasnopis.Clear();
            premještajni.zakritak.Clear();
            zamjenski.jasnopis.Clear();
            zamjenski.zakritak.Clear();
            zamjenski.zakritniSlovored.Clear();
        }

        public void ZakrijSloženim(List<string> ključ)
        {
            zamjenski.KreirajZakritniSlovoredKljučem(ključ);
            premještajni.jasnopis = jasnopis;
            premještajni.ZakrijPremještajnim(ključ);

            zamjenski.jasnopis = premještajni.zakritak;
            zamjenski.KreirajZakritakZamjenskim();

            //spremanje zakritka iz zamjenskog u složeni
            for (int i = 0; i < zamjenski.zakritak.Count(); i++)
            {
                if (i % 5 == 0 && i != 0) { zakritak.Add(" "); }
                zakritak.Add(zamjenski.zakritak[i]);
            }

            ČišćenjeFieldova();
        }
        public void RaskrijSloženim(List<string> ključ)
        {
            zamjenski.KreirajZakritniSlovoredKljučem(ključ);
            zamjenski.zakritak = zakritak;
            zamjenski.KreirajJasnopisZamjenskim();

            premještajni.zakritak = zamjenski.jasnopis;
            premještajni.RaskrijPremještajnim(ključ);

            jasnopis = premještajni.jasnopis;

            ČišćenjeFieldova();
        }
    }
}
