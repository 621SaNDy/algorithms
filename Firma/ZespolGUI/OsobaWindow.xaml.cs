using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Firma;

namespace ZespolGUI
{
    /// <summary>
    /// Logika interakcji dla klasy OsobaWindow.xaml
    /// </summary>
    public partial class OsobaWindow : Window
    {
        private Osoba _osoba;

        public OsobaWindow(Osoba osoba):this()
        {
            _osoba = osoba;
            tbPesel.Text = osoba.Pesel;
            tbImie.Text = osoba.Imie;
            tbNazwisko.Text = osoba.Nazwisko;
            tbDataUrodz.Text = osoba.DataUrodzenia.ToString("yyyy-MM-dd");
            //cbPlec.Text = ((osoba.Plec) == Plcie.K) ? "Kobieta" : "Mężczyzna"; <- nie działa
            cbPlec.SelectedItem = ((osoba.Plec) == Plcie.K) ? lbKobieta : lbMezczyzna;
            
            switch (osoba)
            {
                case KierownikZespolu kierownik:
                    lbZmienny.Content = "Doświadczenie";
                    tbZmienny.Text = kierownik.Doswiadczenie.ToString();
                    break;
                case CzlonekZespolu czlonek:
                    lbZmienny.Content = "Funkcja";
                    tbZmienny.Text = czlonek.funkcja;
                    break;
            }
        }

        public OsobaWindow()
        {
            InitializeComponent();

            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(117, 201, 200), Color.FromRgb(255,255,255), new Point(0.5, 0), new Point(0.5, 1));
            Background = gradientBrush;

            btnZatwierdz.Background = new SolidColorBrush(Color.FromRgb(141, 182, 179));
            btnAnuluj.Background = new SolidColorBrush(Color.FromRgb(141, 182, 179));
        }

        private void btnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (tbPesel.Text != "" && tbImie.Text != "" && tbNazwisko.Text != "")
            {
                _osoba.Pesel = tbPesel.Text;
                _osoba.Imie = tbImie.Text;
                _osoba.Nazwisko = tbNazwisko.Text;
                string[] fdate = { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy" };
                // Użycie try i catch jest zdecydowanie dłuższe, ponieważ TryParseExact zwraca bool, a nie ma Exception dla niepoprawnej daty
                if (DateTime.TryParseExact(tbDataUrodz.Text, fdate, null, DateTimeStyles.None, out DateTime date))
                    _osoba.DataUrodzenia = date;
                else
                MessageBox.Show("Niepoprawny format daty", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                _osoba.Plec = (cbPlec.SelectedItem == lbKobieta) ? Plcie.K : Plcie.M;

                switch(_osoba) {
                    case KierownikZespolu kierownik:
                        if (int.TryParse(tbZmienny.Text, out int doswiadczenie))
                            kierownik.Doswiadczenie = doswiadczenie;
                        else
                            MessageBox.Show("Niepoprawny format doświadczenia", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case CzlonekZespolu czlonek:
                        czlonek.funkcja = tbZmienny.Text;
                        break;
                }
            }
            DialogResult = true;
        }

        // Opcjonalnie
        private void btnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;   
        }
    }
}
