﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafikon_Busy
{
    class JdfParser
    {
        string[][] XLinky;
        string[][] XZastavky;
        string[][] XSpoje;
        string[][] XZasLinky;
        string[][] XZasSpoje;
        
        string[][] PevneKody;
        Dictionary<string, string> PevneKodyDict;

        Linka[] Linky;
        Zastavka[] Zastavky;
        Spoj[] Spoje;
        ZasLinka[] ZasLinky;
        ZasSpoj[] ZasSpoje;
        private void NactiVse(string slozka)
        {
            XLinky = SheetLoader.ReadQuotedCSV(slozka + "/Linky.txt", ',', ';');
            XZastavky = SheetLoader.ReadQuotedCSV(slozka + "/Zastavky.txt", ',', ';');
            XZasSpoje = SheetLoader.ReadQuotedCSV(slozka + "/Zasspoje.txt", ',', ';');
            XZasLinky = SheetLoader.ReadQuotedCSV(slozka + "/Zaslinky.txt", ',', ';');
            PevneKody = SheetLoader.ReadQuotedCSV(slozka + "/Pevnykod.txt", ',', ';');
            XSpoje = SheetLoader.ReadQuotedCSV(slozka + "/Spoje.txt", ',', ';');
            

        }
        /// <summary>
        /// Converts csv into array of objects; 
        /// due to C# inability to provide non-default constructor for generic arguments, uses factory method as delegate
        /// </summary>
        /// <typeparam name="O"></typeparam>
        /// <param name="csv"></param>
        /// <param name="Create"></param>
        /// <returns></returns>
        public static O[] NahazejObjekt<O>(string[][] csv, Func<string[], O> Create)
        {
            var vysledek = new O[csv.Length];
            for (int i = 0; i < csv.Length; ++i)
            {
                vysledek[i] = Create(csv[i]);
            }
            return vysledek;
        }
        private void VytvorObjekty()
        {
            Linky = NahazejObjekt(XLinky, Linka.Create);
            Zastavky = NahazejObjekt(XZastavky, Zastavka.Create);
            Spoje = NahazejObjekt(XSpoje, Spoj.Create);
            ZasLinky = NahazejObjekt(XZasLinky, ZasLinka.Create);
            ZasSpoje = NahazejObjekt(XZasSpoje, ZasSpoj.Create);
        }
        private void MapujPevneKody()
        {
            PevneKodyDict = new Dictionary<string, string>();
            foreach (string[] line in PevneKody)
            {
                string kod = line[0];
                string znacka = line[1];
                PevneKodyDict[kod] = znacka;
            }
            foreach (var sp in Spoje)
            {
                for (int i = 0; i < sp.Kody.Count; ++i)
                {
                    sp.Kody[i] = PevneKodyDict[sp.Kody[i]];
                }
            }
        }
        private void SeradVse()
        {
            //seard linky
            //serad spoje
            //serad zastavky pod linkou
            //serad zastavky na spojich
            Linky = this.Linky.OrderBy(linka => linka.IdLinky).ToArray();
            Spoje = this.Spoje.OrderBy(spoj => spoj.IdLinky).ThenByDescending(spoj => spoj.Dopredu)
                .ThenBy(spoj => spoj.CisloSpoje).ToArray();
            ZasLinky = this.ZasLinky.OrderBy(zasl => zasl.IdLinky).ThenBy(zasl => zasl.TarifniCislo).ToArray();
            ZasSpoje = this.ZasSpoje.OrderBy(zasp => zasp.IdLinky).ThenBy(zasp => zasp.CisloSpoje)
                .ThenBy(zasp => zasp.Dopredu? zasp.TarifniCislo : int.MaxValue - zasp.TarifniCislo) // = vzestupne podle sledu zastavek
                .ToArray();
        }
        public string[] VypisLinkyStr()
        {
            if (this.Linky == null)
                return null;
            var ln = new string[Linky.Length];
            for(int i = 0; i < Linky.Length; ++i)
            {
                ln[i] = Linky[i].ToString();
            }
            return ln;
        }
        public (int,int)[] VypisLinkyInt()
        {
            if (this.Linky == null)
                return null;
            var ln = new (int,int)[Linky.Length];
            for (int i = 0; i < Linky.Length; ++i)
            {
                ln[i] = Linky[i].IdLinky;
            }
            return ln;
        }
        public string[][] PostavTabulku(ValueTuple<int,int>linka,bool dopredu) //toto je v podstate uplne nejhloupejsi reseni - nadela tabulku z tech spoju, ktere jsme parsovali v jdf... ale kdyz funguje...
        {
            //rozmery tabulky:
            //2 + pocet zastavek (radky)
            //2 + pocet spoju (sloupce)
            const int velikostZahlavi = 2;
            var zastLinky = (from zl in ZasLinky
                            where zl.IdLinky == linka
                            join za in Zastavky on zl.IdZastavky equals za.CisloZastavky
                            select za.JmenoZastavky)
                            .ToArray();
            int pocetZastavek = zastLinky.Length;
            int velikostRadku = pocetZastavek + velikostZahlavi;

            var spojeTam = from sp in Spoje where sp.IdLinky == linka && sp.Dopredu == dopredu
                           select new { sp.CisloSpoje, sp.Kody };
            var pocetSpojuTam = spojeTam.Count();

            var TabulkaTam = new List<string[]>();
            if (dopredu)
            {
                TabulkaTam.Add(Enumerable.Range(1 - velikostZahlavi, velikostRadku).Select(x => x.ToString()).ToArray());
            }
            else
            {
                TabulkaTam.Add(Enumerable.Range(1, velikostRadku).Select(x => x.ToString()).Reverse().ToArray());
            }
            
            TabulkaTam[0][0] = "Tč";
            TabulkaTam[0][1] = "";

            TabulkaTam.Add(new string[velikostRadku].PopulateWith(""));
            Array.Copy(dopredu?zastLinky:zastLinky.Reverse().ToArray(), 0, TabulkaTam[1], 2, pocetZastavek);

            var zacatkySpoju = (
                from zs in ZasSpoje
                where zs.IdLinky == linka && zs.Dopredu == dopredu //orderby zs.CisloSpoje
                group zs by zs.CisloSpoje into zsp
                select zsp.Min(zp => zp.Dopredu ? zp.TarifniCislo : pocetZastavek + 1 - zp.TarifniCislo)
                ).ToArray();

            var konceSpoju = (
                from zs in ZasSpoje
                where zs.IdLinky == linka && zs.Dopredu == dopredu //orderby zs.CisloSpoje
                group zs by zs.CisloSpoje into zsp
                select zsp.Max(zp => zp.Dopredu ? zp.TarifniCislo : pocetZastavek + 1 - zp.TarifniCislo)
                ).ToArray();

            //Pro kazdy spoj nyni udelame casove a kilometricke udaje (km udaje mohou byt stejne pro ruzne spoje
            //proto nejprve delam  hashset), prvni dva radky nechame prazdne

            var kilometraze = new HashSet<string[]>(new StringArrComparer()); //pole je ref typ, takze musim dat vlastni porovnani

            int i = 0;
            foreach (var sp in spojeTam)
            {
                var casyTam = 
                from zs in ZasSpoje
                where zs.IdLinky == linka && sp.CisloSpoje == zs.CisloSpoje
                select new { zs.TarifniCislo, zs.Cas, zs.Kilometry };

                var zacatek = zacatkySpoju[i]+1;
                var konec = konceSpoju[i++]+1;

                var radek = new string[velikostRadku].PopulateWith("");
                var kilometry = new string[velikostRadku].PopulateWith("");
                kilometry[0] = "km";// for (int j = zacatek; j <= konec; ++j) kilometry[j] = "<";

                foreach(var x in casyTam)
                {
                    int j = dopredu ? x.TarifniCislo + velikostZahlavi - 1 
                        : pocetZastavek - x.TarifniCislo + velikostZahlavi;
                    radek[j] = x.Cas;
                    kilometry[j] = x.Kilometry==""?"<":x.Kilometry;
                }
                TabulkaTam.Add(radek);
                kilometraze.Add(kilometry);
            }
            foreach(var km in kilometraze)
            {
                TabulkaTam.Add(km);
            }

            //Pridej zahlavi
            i = 2;
            foreach(var sp in spojeTam)
            {
                TabulkaTam[i][0] = "Spoj " + sp.CisloSpoje.ToString();
                TabulkaTam[i][1] = String.Join(" ", sp.Kody);
                i += 1;
            }

            return TabulkaTam.ToArray();
        }
        /// <summary>
        /// Nacte cely JDF podle jednotlivych funkci
        /// </summary>
        /// <returns></returns>
        public bool NactiAPriprav(string slozka)
        {
            NactiVse(slozka);
            VytvorObjekty();
            MapujPevneKody();
            SeradVse();

            return true;
        }
    }
    interface IDLinka
    {
        int CisloLinky { get; }
        int RozliseniLinky { get; }
        (int, int) IdLinky { get; }
    }
    class Linka : IDLinka
    {
        public int CisloLinky { get; }
        public int RozliseniLinky { get; }
        public (int, int) IdLinky { get => new ValueTuple<int, int>(CisloLinky, RozliseniLinky); }

        public Linka(string[] link)
        {
            CisloLinky = int.Parse(link[0]);
            RozliseniLinky = int.Parse(link[16]);
        }

        internal static Linka Create(string[] arg)
        {
            return new Linka(arg);
        }
        public override string ToString()
        {
            return IdLinky.ToString();
        }
    }
    class Zastavka
    {

        public int CisloZastavky { get; }
        string nazevObec { get; }
        string nazevCastObce { get; }
        string nazevBlizsiMisto { get; }
        public string JmenoZastavky { get => $"{nazevObec},{nazevCastObce},{nazevBlizsiMisto}".Trim(','); }
        public Zastavka(string[] zast)
        {
            CisloZastavky = int.Parse(zast[0]);
            nazevObec = zast[1];
            nazevCastObce = zast[2];
            nazevBlizsiMisto = zast[3];
        }
        public override string ToString()
        {
            return this.JmenoZastavky;
        }

        internal static Zastavka Create(string[] arg)
        {
            return new Zastavka(arg);
        }
    }
    class Spoj : IDLinka
    {
        public int CisloLinky { get; }
        public int RozliseniLinky { get; }
        public (int, int) IdLinky { get; }
        public int CisloSpoje { get; }
        public List<string> Kody { get; }
        public bool Dopredu { get => CisloSpoje % 2 == 1; }
        public Spoj(string[] spoj)
        {
            CisloLinky = int.Parse(spoj[0]);
            RozliseniLinky = int.Parse(spoj[13]);
            IdLinky = new ValueTuple<int, int>(CisloLinky, RozliseniLinky);
            CisloSpoje = int.Parse(spoj[1]);
            Kody = new List<string>();
            for (int i = 2; i < 11; ++i)
            {
                if (spoj[i] != "")
                {
                    Kody.Add(spoj[i]);
                }
            }
        }
        public override string ToString()
        {
            return $"Spoj {IdLinky}/{CisloSpoje}";
        }

        internal static Spoj Create(string[] arg)
        {
            return new Spoj(arg);
        }
    }
    class ZasLinka : IDLinka
    {
        public int CisloLinky { get; }
        public int RozliseniLinky { get; }
        public (int, int) IdLinky { get; }
        public int TarifniCislo { get; }
        public int IdZastavky { get; }
        public ZasLinka(string[] zasl)
        {
            CisloLinky = int.Parse(zasl[0]);
            RozliseniLinky = int.Parse(zasl[8]);
            IdLinky = new ValueTuple<int, int>(CisloLinky, RozliseniLinky);
            TarifniCislo = int.Parse(zasl[1]);
            IdZastavky = int.Parse(zasl[3]);
        }

        internal static ZasLinka Create(string[] arg)
        {
            return new ZasLinka(arg);
        }
    }
    class ZasSpoj : IDLinka
    {
        public int CisloLinky { get; }
        public int RozliseniLinky { get; }
        public (int, int) IdLinky { get; }
        public int CisloSpoje { get; }
        public bool Dopredu { get => CisloSpoje % 2 == 1; }
        public int TarifniCislo { get; }
        public int IdZastavky { get; }
        public string Cas { get; }
        public string Kilometry { get; }
        public ZasSpoj(string[] zasp)
        {
            CisloLinky = int.Parse(zasp[0]);
            RozliseniLinky = int.Parse(zasp[14]);
            IdLinky = new ValueTuple<int, int>(CisloLinky, RozliseniLinky);
            CisloSpoje = int.Parse(zasp[1]);
            TarifniCislo = int.Parse(zasp[2]);
            IdZastavky = int.Parse(zasp[3]);
            Kilometry = zasp[9];

            if (zasp[11] != "")
                Cas = zasp[11];
            else if (zasp[10] != "")
                Cas = zasp[10];
            else
                throw new ArgumentException("Neni cas uveden");
            if(3 <= Cas.Length && Cas.Length <= 4)
                Cas = Cas.Insert(Cas.Length - 2, ":");
            
        }

        internal static ZasSpoj Create(string[] arg)
        {
            return new ZasSpoj(arg);
        }
    }

}
