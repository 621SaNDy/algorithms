using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Firma
{
    [Serializable]
    public abstract class Osoba
    {
        private string PESEL;
        private Plcie plec;
        private DateTime dataUrodzenia;
        private string imie;
        public string Nazwisko { get; set; }

        private string numerTelefonu;

        public string NumerTelefonu
        {
            get { return numerTelefonu; }
            set { numerTelefonu = value; }
        }

        public string Imie
        {
            get { return imie; }
            set { imie = value; }
        }

        [XmlAttribute("Pesel")]
        public string Pesel
        {
            get { return PESEL; }
            set { PESEL = value; }
        }

        public Plcie Plec
        {
            get { return plec; }
            set { plec = value; }
        }

        public DateTime DataUrodzenia
        {
            get => dataUrodzenia; set => dataUrodzenia = value;
        }


        public Osoba()
        {
            this.Imie = "";
            this.Nazwisko = "";
            this.dataUrodzenia = DateTime.MinValue;
            this.PESEL = "00000000000";
        }

        public Osoba(string imie, string nazwisko)
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
        }

        public Osoba(string imie, string nazwisko, string data_urodzenia, string pesel, Plcie plec) 
        {
            this.PESEL = pesel;
            this.Plec = plec;
            this.Nazwisko = nazwisko;
            this.Imie = imie;
            DateTime.TryParseExact(data_urodzenia, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yy", "dd.mm.yyyy" }, null, DateTimeStyles.None, out var result);
            DataUrodzenia = result;
        }

        public Osoba(string imie, string nazwisko, string data_urodzenia, string pesel, Plcie plec, string numerTelefonu)
        {
            this.Nazwisko = nazwisko;
            this.Imie = imie;
            DateTime.TryParseExact(data_urodzenia, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy", "dd.mm.yyyy" }, null, DateTimeStyles.None, out var result);
            DataUrodzenia = result;
            this.PESEL = pesel;
            this.Plec = plec;
            this.NumerTelefonu = numerTelefonu;
        }

        public int Wiek()
        {
            DateTime dzisiaj = DateTime.Now;
            int wiek = dzisiaj.Year - dataUrodzenia.Year;

            if (dzisiaj.Month < dataUrodzenia.Month || (dzisiaj.Month == dataUrodzenia.Month && dzisiaj.Day < dataUrodzenia.Day))
            {
                wiek--;
            }

            return wiek;
        }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({Wiek()} lat) {DataUrodzenia.ToShortDateString()} {Pesel} {Plec} {NumerTelefonu} ";
        }

        public void Litery()
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            imie = textInfo.ToTitleCase(imie.ToLower());
            Nazwisko = textInfo.ToTitleCase(Nazwisko.ToLower());

            Console.WriteLine($"\n{Imie} {Nazwisko}");
        }

        public void PrzezyteGodziny()
        {
            Console.WriteLine("\n---------------- Przeżyte godziny: ");
            int godzina = 0;
            Int32.TryParse(Console.ReadLine(), out godzina);

            TimeSpan hours = new TimeSpan(godzina, 0, 0);
            dataUrodzenia = dataUrodzenia + hours;

            Console.WriteLine(Math.Floor((DateTime.Now - dataUrodzenia).TotalHours) + " przeżytych godzin\n");            
        }
        
        public void PoprawnyPesel()
        {
            Console.WriteLine("---------------- Sprawdzanie numeru PESEL: ");
            bool poprawny = PESEL.Length == 11;
            string niepoprawnyTxt = "zła długość";
            if (poprawny)
                foreach (char c in PESEL)
                    if (!char.IsDigit(c))
                    {
                        poprawny = false;
                        niepoprawnyTxt = "inne znaki niż cyfry";
                        break;
                    }

            if (poprawny)
            {
                int[] wagi = new int[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
                int suma = 0;
                for (int i = 0; i < 10; i++)
                {
                    suma += wagi[i] * (PESEL[i] - '0');
                }
                poprawny = ((10 - suma % 10) % 10) == (PESEL[PESEL.Length - 1] - '0');
                niepoprawnyTxt = "niepoprawna suma kontrolna";
            }
            if (poprawny)
                Console.WriteLine("Pesel jest poprawny");
            else
                Console.WriteLine($"Pesel jest niepoprawny - {niepoprawnyTxt}");
        }

        public bool Equals(Osoba other)
        {
            return other != null && this.PESEL == other.PESEL;
        }
    }
}
