using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        List<Jatszma> jatszmak = new List<Jatszma>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Fájlbeolvasás
            string[] lines = File.ReadAllLines("nyeremeny.txt");

            foreach (var item in lines)
            {
                string[] values = item.Split(';');
                Jatszma jatszma_object = new Jatszma(int.Parse(values[0]), values[1], int.Parse(values[2]), int.Parse(values[3]), values[4]);
                jatszmak.Add(jatszma_object);
            }

            //legtöbb játszma, ár
            List<string> jatekosok = new List<string>();
            foreach (var item in jatszmak)
            {
                jatekosok.Add(item.nev);
            }
            jatekosok.Distinct();
            int max = 0;
            string maxNev = "";
            int maxOsszeg = 0;
            int aOsszeg = 0;
            int aMax = 0;
            foreach (var item in jatekosok)
            {
                foreach (var item2 in jatszmak)
                {
                    if (item == item2.nev)
                    {
                        aMax++;
                        aOsszeg += item2.tet;
                    }
                }
                if (aMax > max)
                {
                    max = aMax;
                    maxNev = item;
                    maxOsszeg = aOsszeg;
                }
                aMax = 0;
            }
            label3.Text = maxNev +": "+ max.ToString() + " alkalom, " + maxOsszeg + " tét összesen.";

            //vesztes játszmák
            int vjatszma = 0;
            foreach (var item in jatszmak)
            {
                if (item.kimenet== "vesztes")
                {
                    vjatszma++;
                }
            }
            label4.Text = vjatszma + " db vesztes játszma van összesen.";

            //legnagyobb nyeremény
            int nyMin = int.MaxValue;
            int nyAct = 0;
            string nevMin = "";
            int tetMin = 0;
            int szorzoMin = 0;

            foreach (var item in jatszmak)
            {
                if (item.kimenet=="nyertes")
                {
                    nyAct = item.tet * item.szorzo;
                    if (nyMin > nyAct)
                    {
                        nyMin = nyAct;
                        nevMin = item.nev;
                        tetMin = item.tet;
                        szorzoMin = item.szorzo;
                    }
                }
                
            }
            label5.Text =  nevMin +": " + tetMin.ToString() +" " + szorzoMin.ToString() + " Összesen: " + (tetMin*szorzoMin);

            //a betűs nevek
            int aDb = 0;
            string nevek = "";
            foreach (var item in jatekosok)
            {
                if (item.ToLower()[0] == 'a')
                {
                    aDb++;
                    if (aDb < 6)
                        nevek +="\n - "+item;
                }
            }
            label7.Text = aDb.ToString() + "db 'a' betűs játékos van."+nevek;

            //xy felhasználó
            int antiNy = 0;
            int antiV = 0;
            foreach (var item in jatszmak)
            {
                if (item.nev == "anti12")
                    if (item.kimenet == "nyertes")
                        antiNy += item.tet * item.szorzo;
                    else
                        antiV += item.tet * item.szorzo;
                
            }
            label8.Text = "anti12 nevű felhasználó " + (antiNy - antiV).ToString() + " pénzzel zárt.";

            //összes nyertes
            int osszNy = 0;
            foreach (var item in jatszmak)
            {
                if (item.kimenet=="nyertes")
                {
                    osszNy += item.tet * item.szorzo;
                }

            }
            label9.Text = "Az össszes nyertes kör összege: " + osszNy;

            //táblázat
            foreach (var item in jatszmak)
            {
                if (item.kimenet=="nyertes")
                {
                    dataGridView2.Rows.Add(item.sorszam.ToString(),item.nev,(item.tet*item.szorzo).ToString());
                }

            }
            
        }
        //1.feladat
        private void button1_Click(object sender, EventArgs e)
        {
            int sorszam = int.Parse(textBox1.Text);
            Jatszma aJ = jatszmak[sorszam - 1];
            label2.Text = aJ.nev+": "+ aJ.tet + ", " + aJ.szorzo + ", " + aJ.kimenet + "";
        }

        //ellenőrzés
        private void button2_Click(object sender, EventArgs e)
        {
            string nev = textBox2.Text;
            foreach (var item in jatszmak)
            {
                if (nev==item.nev)
                {
                    dataGridView1.Rows.Add(item.nev, item.tet, item.szorzo);
                }
            }
        }
    }
}
