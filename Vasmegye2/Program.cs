using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Vasmegye2
{
    internal class Program
    {
        static List<SzemelySzam> szemelySzamok = new List<SzemelySzam>();

        static void Main(string[] args)
        {
            Console.WriteLine("2.feladat: Adatok belvasása, tárolása.");
            adatokBeolvasasa("vas.txt");
            Console.WriteLine("\n4.feladat: Ellenőrzés");
            feladat04();
            Console.WriteLine($"\n5.feladat: Vas megyében a vizsgált évek alatt {szemelySzamok.Count} csecsemő született.");
            Console.WriteLine($"\n6.feladat: Fiúk száma {szemelySzamok.FindAll(a => a.Szam[0]=='1' || a.Szam[0]=='3').Count}");
            if (szokoEvbenSzuletett())
            {
                Console.WriteLine("8.feladat: Szökőnapon született baba!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            else
            {
                Console.WriteLine("8.feladat: Szökőnapon nem született baba!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            feladat09();
            Console.WriteLine("\nPrgram vége!");
            Console.ReadLine();
        }
        private static void feladat09()
        {
            Console.WriteLine("9.feladat: Statisztika");
            var statisztika = szemelySzamok.GroupBy(a => a.evszam()).Select(b => new { ev = b.Key, fo = b.Count() });
            foreach (var item in statisztika)
            {
                Console.WriteLine($"{item.ev} - {item.fo} fő");
            }
        }
        private static bool szokoEvbenSzuletett()
        {
            var szokoEvi = szemelySzamok.Find(a => a.evszam() % 4 == 0 && a.Szam.Substring(4, 4).Equals("0224"));
            return szokoEvi != null;
        }

        private static void feladat04()
        {
            List<SzemelySzam> hibasSzamok = szemelySzamok.FindAll(a => !CdvEll(a.Szam));
            foreach (SzemelySzam item in hibasSzamok)
            {
                Console.WriteLine($"Hibás a {item.Szam} személyi azonosító!!!");
                szemelySzamok.Remove(item);
            }
        }

        public static bool CdvEll(string szam)
        {
            //--3.feladat
            string szamNumeric = new string(szam.Where(a => char.IsDigit(a)).ToArray());
            if (szamNumeric.Length != 11)
            {
                return false;
            }
            double szum = 0;
            for (int i = 0; i < szamNumeric.Length-1; i++)
            {
                szum += char.GetNumericValue(szamNumeric[i])*(10-i);
            }
            return char.GetNumericValue(szamNumeric[10]) == szum % 10;
        }

        private static void adatokBeolvasasa(string adatFile)
        {
            if (!File.Exists(adatFile))
            {
                Console.WriteLine("A forrás adatok hiányoznak!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            using (StreamReader sr = new StreamReader(adatFile))
            {
                while (!sr.EndOfStream)
                {
                    szemelySzamok.Add(new SzemelySzam(sr.ReadLine()));
                }
            }
        }
    }
}
