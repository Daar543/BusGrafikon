using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafikon_Busy
{
    class JdfParser
    {
        string[][] XZastavky;
        string[][] XSpoje;
        string[][] XZasLinky;
        string[][] XZasSpoje;
        
        string[][] PevneKody;
        Dictionary<string, string> PevneKodyDict;

        Zastavka[] Zastavky;
        Spoj[] Spoje;
        ZasLinka[] ZasLinky;
        ZasSpoj[] ZasSpoje;
        public void NactiVse(string slozka)
        {
            XZastavky = SheetLoader.ReadCsvInput(slozka + "/Zastavky.txt", ';');
            XZasSpoje = SheetLoader.ReadCsvInput(slozka + "/Zasspoje.txt", ';');
            XZasLinky = SheetLoader.ReadCsvInput(slozka + "/Zaslinky.txt", ';');
            PevneKody = SheetLoader.ReadCsvInput(slozka + "/Pevnykod.txt", ';');
            XSpoje = SheetLoader.ReadCsvInput(slozka + "/Spoje.txt", ';');

            
        }
        public void VytvorObjekty()
        {
            Zastavky = new Zastavka[XZastavky.Length];
            for(int i = 0; i<XZastavky.Length;++i)
            {
                Zastavky[i] = new Zastavka(XZastavky[i]);
            }
            ZasSpoje = new ZasSpoj[XZasSpoje.Length];
            for (int i = 0; i < XZastavky.Length; ++i)
            {
                ZasSpoje[i] = new ZasSpoj(XZasSpoje[i]);
            }
            ZasLinky = new ZasLinka[XZasLinky.Length];
            for (int i = 0; i < XZasLinky.Length; ++i)
            {
                ZasLinky[i] = new ZasLinka(XZasLinky[i]);
            }
            Spoje = new Spoj[XSpoje.Length];
            for (int i = 0; i < XSpoje.Length; ++i)
            {
                Spoje[i] = new Spoj(XSpoje[i]);
            }
            /*Zastavky = new Zastavka[XZastavky.Length];
            for (int i = 0; i < XZastavky.Length; ++i)
            {
                Zastavky[i] = new Zastavka(XZastavky[i]);
            }*/
        }
        public void MapujPevneKody()
        {
            PevneKodyDict = new Dictionary<string, string>();
            foreach(string[]line in PevneKody)
            {
                string kod = line[0];
                string znacka = line[1];
                PevneKodyDict[kod] = znacka;
            }
            foreach(var sp in Spoje)
            {
                for(int i = 0; i < sp.Kody.Count; ++i)
                {
                    sp.Kody[i] = PevneKodyDict[sp.Kody[i]];
                }
            }
        }
    }
    interface IDLinka
    {
        int CisloLinky { get; }
        int RozliseniLinky { get; }
        (int, int) IdLinky { get; }
    }
    class Zastavka
    {

        int CisloZastavky { get; }
        string nazevObec { get; }
        string nazevCastObce { get; }
        string nazevBlizsiMisto { get; }
        string JmenoZastavky { get => $"{nazevObec},{nazevCastObce},{nazevBlizsiMisto}".Trim(','); }
        public Zastavka(string[] zast)
        {
            CisloZastavky = int.Parse(zast[0]);
            nazevObec = zast[1];
            nazevCastObce = zast[2];
            nazevBlizsiMisto = zast[3];
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
    }
    class ZasSpoj : IDLinka
    {
        public int CisloLinky { get; }
        public int RozliseniLinky { get; }
        public (int, int) IdLinky { get; }
        public int CisloSpoje { get; }
        public int TarifniCislo { get; }
        public int IdZastavky { get; }
        public string Cas { get; }
        public ZasSpoj(string[] zasp)
        {
            CisloLinky = int.Parse(zasp[0]);
            RozliseniLinky = int.Parse(zasp[14]);
            IdLinky = new ValueTuple<int, int>(CisloLinky, RozliseniLinky);
            CisloSpoje = int.Parse(zasp[1]);
            TarifniCislo = int.Parse(zasp[2]);
            IdZastavky = int.Parse(zasp[3]);
            Cas = zasp[11];
        }
    }
}
