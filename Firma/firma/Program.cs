namespace Firma
{
    public enum Plcie { K, M };
    internal class Program
    {

        static void Main(string[] args)
        {
            /*Osoba osoba1 = new Osoba("Beata", "Nowak", "1992-10-22", "92102201347", Plcie.K);
            Osoba osoba2 = new Osoba("Jan", "Janowski", "1993-03-15", "92031507772", Plcie.M);

            Osoba osoba3 = new Osoba("Aleksndra", "Wilkosz", "2007-06-13", "07218391011", Plcie.K, "519281901");

            Console.WriteLine(osoba1.Imie + " " + osoba1.Nazwisko + " " + osoba1.dataUrodzenia.ToShortDateString() + " " + osoba1.Pesel + " " + osoba1.Plec + ", Wiek: " + osoba1.Wiek() + " lat");
            Console.WriteLine(osoba2.Imie + " " + osoba2.Nazwisko + " " + osoba2.dataUrodzenia.ToShortDateString() + " " + osoba2.Pesel + " " + osoba2.Plec + ", Wiek: " + osoba2.Wiek() + " lat\n");

            Console.WriteLine(osoba1.ToString());
            Console.WriteLine(osoba2.ToString());

            osoba1.Litery();
            osoba1.PrzezyteGodziny();
            osoba1.PoprawnyPesel();

            Console.WriteLine(osoba3.ToString());*/


            CzlonekZespolu czlonek1 = new CzlonekZespolu("Beata", "Nowak", "1992-10-22", "92102201347", Plcie.K, "projektant", "01-sty-2020");
            CzlonekZespolu czlonek2 = new CzlonekZespolu("Jan", "Janowski", "1992-03-15", "92031507772", Plcie.M, "programista", "01-cze-2019");

            Console.WriteLine(czlonek1.ToString());
            Console.WriteLine(czlonek2.ToString());

            Console.WriteLine();

            KierownikZespolu osoba4 = new KierownikZespolu("Adam", "Kowalski", "1990-07-01", "90070100211", Plcie.M, 5);

            Console.WriteLine(osoba4.ToString());

            Console.WriteLine();


            KierownikZespolu kierownik = new KierownikZespolu("Adam", "Kowalski", "01.07.1990", "90070142412", Plcie.M, 5);
            Zespol zespol1 = new Zespol("Zespol IT", kierownik);

            CzlonekZespolu czlonek3 = new CzlonekZespolu("Witold", "Adamski", "22.10.1992", "92102266738", Plcie.M, "sekretarz", "01-sty-2020");
            CzlonekZespolu czlonek4 = new CzlonekZespolu("Jan", "Janowski", "15.03.1992", "92031532652", Plcie.M, "programista", "01-sty-2020");
            CzlonekZespolu czlonek5 = new CzlonekZespolu("Jan", "But", "16.05.1992", "92051613915", Plcie.M, "programista", "01-cze-2019");
            CzlonekZespolu czlonek6 = new CzlonekZespolu("Beata", "Nowak", "22.11.1993", "93112225023", Plcie.K, "projektant", "01-sty-2020");
            CzlonekZespolu czlonek7 = new CzlonekZespolu("Anna", "Mysza", "22.07.1991", "91072235964", Plcie.K, "projektant", "31-lip-2019");

            zespol1.DodajCzlonka(czlonek3);
            zespol1.DodajCzlonka(czlonek4);
            zespol1.DodajCzlonka(czlonek5);
            zespol1.DodajCzlonka(czlonek6);
            zespol1.DodajCzlonka(czlonek7);

            Console.WriteLine(zespol1);


            /*Console.WriteLine("Sprawdzenie: \n \n" + zespol1.JestCzlonkiem("92102266738") 
                + "\n" + zespol1.JestCzlonkiem("92255266738"));
            Console.WriteLine(zespol1.JestCzlonkiem("Jan", "Janowski") 
                + "\n" + zespol1.JestCzlonkiem("Janusz", "Kowalski" + "\n" ));*/

            /*zespol1.UsunCzlonka("92102266738");
            Console.WriteLine(zespol1.ToString());

            zespol1.UsunCzlonka("Jan", "Janowski");
            Console.WriteLine(zespol1.ToString());

            zespol1.UsunWszystkich();
            Console.WriteLine(zespol1.ToString());*/


            Console.WriteLine("Projektant: ");
            List<CzlonekZespolu> funkcja = new List<CzlonekZespolu>();
            funkcja = zespol1.WyszukajCzlonkow("projektant");
            foreach (CzlonekZespolu czlonek in funkcja)
            {
                Console.WriteLine(czlonek);
            }

            Console.WriteLine("\nStyczeń: ");
            List<CzlonekZespolu> miesiac = new List<CzlonekZespolu>();
            miesiac = zespol1.WyszukajCzlonkow(1);
            foreach (CzlonekZespolu czlonek in miesiac)
            {
                Console.WriteLine(czlonek);
            }

            Console.WriteLine();

            Zespol zespolCopy = (Zespol)zespol1.Clone();
            zespolCopy.Nazwa = "NowaGrupa";
            zespolCopy.Kierownik = new KierownikZespolu("Rafał", "Marzec", "", "88032112357", Plcie.M, 6);
            Console.WriteLine(zespolCopy.ToString());

            zespol1.Sortuj();
            Console.WriteLine(zespol1.ToString());

            zespol1.SortujPoPESEL();
            Console.WriteLine(zespol1.ToString());


            Console.WriteLine("Sprawdzenie czy czlonek jest w zespole:");
            Console.WriteLine(zespol1.JestCzlonkiem(czlonek3));
            CzlonekZespolu czlonek8 = new CzlonekZespolu("Waldek", "Kowalski", "01.01.1969", "123456789", Plcie.M, "młodszy chorąży", "(02-lut-2077)");
            Console.WriteLine(zespol1.JestCzlonkiem(czlonek8));
            Console.WriteLine();

            zespol1.ZapiszBin("zespol.bin");
            Zespol zespol2 = (Zespol)zespol1.OdczytajBin("zespol.bin");

            Console.WriteLine(zespol2.ToString());

            Zespol.ZapiszXML("zespol.xml", zespol1);
            Zespol zespol3 = (Zespol)Zespol.OdczytajXML("zespol.xml");

            Console.WriteLine(zespol3.ToString());

            Console.ReadKey();
        }
    }
}