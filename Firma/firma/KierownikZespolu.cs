using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firma;

[Serializable]
public class KierownikZespolu : Osoba, ICloneable
{
    public int Doswiadczenie { get; set; }

    public KierownikZespolu() { }

    public KierownikZespolu(string imie, string nazwisko, string data_urodzenia, string pesel, Plcie plec, int doswiadczenie)
        : base(imie, nazwisko, data_urodzenia, pesel, plec)
    {
        Doswiadczenie = doswiadczenie;
    }

    public override string ToString()
    {
        return $"{Imie} {Nazwisko} ({Wiek()} lat) {DataUrodzenia.ToShortDateString()} {Pesel} {Plec} Doswiadczenie: {Doswiadczenie} lat";
    }

    public object Clone()
    {
        KierownikZespolu other = (KierownikZespolu)this.MemberwiseClone();
        other.Imie = String.Copy(this.Imie);
        other.Nazwisko = String.Copy(this.Nazwisko);
        other.DataUrodzenia = new DateTime(this.DataUrodzenia.Year, this.DataUrodzenia.Month, this.DataUrodzenia.Day);
        other.Pesel = String.Copy(this.Pesel);
        other.Plec = this.Plec;
        if (NumerTelefonu != null) other.NumerTelefonu = String.Copy(this.NumerTelefonu);
        other.Doswiadczenie = Doswiadczenie;
        return other;
    }
}