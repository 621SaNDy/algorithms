using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma;

[Serializable]
public class CzlonekZespolu : Osoba, ICloneable, IComparable<CzlonekZespolu>
{
    private DateTime dataZapisu;

    public DateTime DataZapisu
    {
        get => dataZapisu; set => dataZapisu = value;
    }
    public string funkcja { get; set; }

    public CzlonekZespolu() { }

    public CzlonekZespolu(string imie, string nazwisko, string data_urodzenia, string pesel, Plcie plec, string funkcja, string dataZapisu) : base(imie, nazwisko, data_urodzenia, pesel, plec)
    {
        this.funkcja = funkcja;
        DateTime.TryParseExact(dataZapisu, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy", "dd-MMM-yyyy" }, null, DateTimeStyles.None, out var result);
        DataZapisu = result;
    }

    public override string ToString()
    {
        return $"{Imie} {Nazwisko} {DataUrodzenia.ToString("dd-MM-yyyy")} {Pesel} {funkcja}";
    }

    public object Clone()
    {
        CzlonekZespolu other = (CzlonekZespolu)this.MemberwiseClone();
        other.Imie = String.Copy(this.Imie);
        other.Nazwisko = String.Copy(this.Nazwisko);
        other.DataUrodzenia = new DateTime(this.DataUrodzenia.Year, this.DataUrodzenia.Month, this.DataUrodzenia.Day);
        other.Pesel = String.Copy(this.Pesel);
        other.Plec = this.Plec;
        if (NumerTelefonu != null) other.NumerTelefonu = String.Copy(this.NumerTelefonu);
        other.dataZapisu = new DateTime(this.dataZapisu.Year, this.dataZapisu.Month, this.dataZapisu.Day);
        other.funkcja = String.Copy(this.funkcja);
        return other;
    }

    public int CompareTo(CzlonekZespolu other)
    {
        if (other == null) throw new ArgumentNullException("other is null");
        int result = this.Nazwisko.CompareTo(other.Nazwisko);
        return (result != 0) ? result : this.Imie.CompareTo(other.Imie);
    }
}   