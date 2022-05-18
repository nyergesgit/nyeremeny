using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Jatszma
    {
        public int sorszam;
        public string nev;
        public int tet;
        public int szorzo;
        public string kimenet;
        public Jatszma(int sorszam, string nev, int tet, int szorzo, string kimenet) {
            this.sorszam = sorszam;
            this.nev = nev;
            this.tet = tet;
            this.szorzo = szorzo;
            this.kimenet = kimenet;
        }
    }
}
