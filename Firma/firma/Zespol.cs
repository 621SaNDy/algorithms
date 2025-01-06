using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Firma;

[Serializable]
public class Zespol : ICloneable, IZapisywalna
{
    private int liczbaCzlonkow;
    private string nazwa;
    private KierownikZespolu kierownik;
    private List<CzlonekZespolu> czlonkowie;

    public Zespol()
    {
        this.liczbaCzlonkow = 0;
        this.nazwa = null;
        this.kierownik = null;
        this.czlonkowie = new List<CzlonekZespolu>();
    }

    public Zespol(string nazwa, KierownikZespolu kierownik) : this()
    {
        this.nazwa = nazwa;
        this.kierownik = kierownik;
    }

    public int LiczbaCzlonkow
    {
        get { return liczbaCzlonkow; }
        set { liczbaCzlonkow = value; }
    }

    public string Nazwa
    {
        get { return nazwa; }
        set { nazwa = value; }
    }

    public KierownikZespolu Kierownik
    {
        get { return kierownik; }
        set { kierownik = value; }
    }

    public void DodajCzlonka(CzlonekZespolu czlonek)
    {
        czlonkowie.Add(czlonek);
        liczbaCzlonkow++;
    }

    public List<CzlonekZespolu> Czlonkowie
    {
        get { return czlonkowie; }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Zespół: {Nazwa}");
        sb.AppendLine($"Kierownik: {Kierownik}");
        sb.AppendLine("Członkowie zespołu: ");

        foreach (CzlonekZespolu czlonek in Czlonkowie)
        {
            sb.AppendLine(czlonek.ToString());
        }
        return sb.ToString();
    }

    public bool JestCzlonkiem(string PESEL)
    {
        foreach (CzlonekZespolu czlonek in Czlonkowie)
        {
            if (czlonek.Pesel == PESEL)
                return true;
        }
        return false;
    }

    public bool JestCzlonkiem(string imie, string nazwisko)
    {
        foreach (CzlonekZespolu czlonek in czlonkowie)
        {
            if (czlonek.Imie == imie && czlonek.Nazwisko == nazwisko) 
                return true;
        }
        return false;
    }

    public void UsunCzlonka(string PESEL)
    {
        foreach (CzlonekZespolu czlonek in czlonkowie)
        {
            if (czlonek.Pesel == PESEL)
            {
                czlonkowie.Remove(czlonek);
                liczbaCzlonkow--;
                break;
            }
        }
    }

    public void UsunCzlonka(string imie, string nazwisko)
    {
        foreach (CzlonekZespolu czlonek in czlonkowie)
        {
            if (czlonek.Imie == imie && czlonek.Nazwisko == nazwisko)
            {
                czlonkowie.Remove(czlonek);
                liczbaCzlonkow--;
                break;
            }
        }
    }

    public void UsunWszystkich()
    {
        czlonkowie.Clear();
        liczbaCzlonkow = 0;
    }

    public List<CzlonekZespolu> WyszukajCzlonkow(string funkcja)
    {
        List<CzlonekZespolu> lista = new List<CzlonekZespolu>();

        czlonkowie.FindAll(czlonek => czlonek.funkcja == funkcja).ForEach(czlonek => lista.Add(czlonek)); 
        return lista;
    }

    public List<CzlonekZespolu> WyszukajCzlonkow(int miesiac)    
    {
        List<CzlonekZespolu> lista = new List<CzlonekZespolu>();

        czlonkowie.FindAll(czlonek => czlonek.DataZapisu.Month == miesiac).ForEach( czlonek => lista.Add(czlonek));
        return lista;
    }

    public object Clone()
    {
        Zespol other = (Zespol)this.MemberwiseClone();
        other.Kierownik = (KierownikZespolu)this.Kierownik.Clone();
        other.czlonkowie = new List<CzlonekZespolu>();
        foreach (CzlonekZespolu czlonek in this.czlonkowie)
        {
            other.czlonkowie.Add((CzlonekZespolu)czlonek.Clone());
        }
        other.nazwa = String.Copy(this.nazwa);
        return other;
    }
    public void Sortuj()
    {
        czlonkowie.Sort();
    }

    internal class PESELComparator : IComparer<CzlonekZespolu>
    {

        public int Compare(CzlonekZespolu czlonek1, CzlonekZespolu czlonek2)
        {
            if (czlonek1 == null || czlonek2 == null) throw new ArgumentNullException("czlonek1 / czlonek2 is null");
            return czlonek1.Pesel.CompareTo(czlonek2.Pesel);
        }
    }

    public void SortujPoPESEL()
    {
        czlonkowie.Sort(new PESELComparator());
    }

    public bool JestCzlonkiem(CzlonekZespolu other)
    {
        foreach (CzlonekZespolu c in czlonkowie)
            if (c.Equals(other)) return true;
        return false;
    }

    public void ZapiszBin(string nazwa)
    {
        System.IO.FileStream file = new System.IO.FileStream(nazwa, System.IO.FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, this);
        file.Close();
    }

    public Object OdczytajBin(string nazwa)
    {
        System.IO.FileStream file = new System.IO.FileStream(nazwa, System.IO.FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        Object obj = bf.Deserialize(file);
        file.Close();
        return obj;
    }

    
    private string ToXML()
    {
        var xml = new XmlSerializer(typeof(Zespol));
        using (StringWriter textWriter = new StringWriter())
        {
            xml.Serialize(textWriter, this);
            return textWriter.ToString();
        }
    }

    public bool Equals(Zespol that)
    {
        return this.ToXML() == that.ToXML();
    }

    public static void ZapiszXML(string nazwa, Zespol z)
    {
        using (var stream = new FileStream(nazwa, FileMode.Create))
        {
            var xml = new XmlSerializer(typeof(Zespol));
            xml.Serialize(stream, z);
        }
    }

    public static Zespol OdczytajXML(string nazwa)
    {
        using (var stream = new FileStream(nazwa, FileMode.Open))
        {
            var xml = new XmlSerializer(typeof(Zespol));
            return (Zespol)xml.Deserialize(stream);
        }
    }
}