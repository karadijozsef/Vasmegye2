using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasmegye2
{
    internal class SzemelySzam
    {
        readonly string szam;

        public string Szam => szam;

        public SzemelySzam(string szam)
        {
            this.szam = szam;
        }
        public int evszam()
        {
            int ev = int.Parse(szam.Substring(2, 2));
            ev = szam[0] == '1' || szam[0] == '2' ? 1900 + ev : 2000 + ev;
            return ev;
        }
    }
}
