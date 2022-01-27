using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptoLearn
{
    class Test
    {
        void TestRaskrivanja ()
        {
            string poruka = "AUJIP ACSTE BEREU DRKUJ OSNjAV USNIM V";
            string ključ = "MUZEJSKI";
            Premještajni premještajni = new Premještajni();
            premještajni.zakritak.Clear();
            foreach (char slovo in poruka)
            {
                premještajni.zakritak.Add(slovo.ToString());
            }
            //foreach (char slovo in ključ)
            //{
            //    premještajni.ključ.Add(slovo.ToString());
            //}
        }
    }
}
