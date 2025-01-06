using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Firma;

namespace ZespolGUI
{
    public partial class MainWindow : Window
    {
        private Zespol zespol;
        private Zespol originalZespol;
        private ObservableCollection<CzlonekZespolu> lista;
        public MainWindow()
        {
            InitializeComponent();
            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(192, 185, 221), Color.FromRgb(117, 201, 200), new Point(0.5, 0), new Point(0.5, 1));
            Background = gradientBrush;

            btnZmienKierownika.Background = new SolidColorBrush(Color.FromRgb(222, 217, 226));
            btnDodaj.Background = new SolidColorBrush(Color.FromRgb(222, 217, 226));
            btnUsun.Background = new SolidColorBrush(Color.FromRgb(222, 217, 226));
            btnZmienCzlonka.Background = new SolidColorBrush(Color.FromRgb(222, 217, 226));
            tbKierownik.IsReadOnly = true;

            mHeader.Background = new SolidColorBrush(Color.FromRgb(222, 217, 226));

            Otworz("zespol.xml");
        }

        private void btnZmienKierownika_Click(object sender, RoutedEventArgs e)
        {
            OsobaWindow okno = new OsobaWindow(zespol.Kierownik);
            okno.ShowDialog();
            tbKierownik.Text = zespol.Kierownik.ToString();
        }
        private void btnZmienCzlonka_Click(object sender, RoutedEventArgs e)
        {
            if(libCzlonkowieZespolu.SelectedIndex == -1)
                MessageBox.Show("Nie wybrano członka zespołu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                OsobaWindow okno = new OsobaWindow((Osoba)libCzlonkowieZespolu.SelectedItem);
                okno.ShowDialog();
                libCzlonkowieZespolu.Items.Refresh();
            }
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            CzlonekZespolu cz = new CzlonekZespolu();
            OsobaWindow okno = new OsobaWindow(cz);
            okno.ShowDialog();
            if (okno.DialogResult == true)
            {
                zespol.DodajCzlonka(cz);
                lista.Add(cz);
            }
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            int zaznaczony = libCzlonkowieZespolu.SelectedIndex;
            if(zaznaczony >= 0)
            {
                lista.RemoveAt(zaznaczony);
                zespol.Czlonkowie.RemoveAt(zaznaczony);
                zespol.LiczbaCzlonkow--;
            }
            else
                MessageBox.Show("Nie wybrano członka zespołu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void MenuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Zapisz();
        }
        private void Zapisz()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.FileName = zespol.Nazwa;
            dlg.Filter = "XML documents (.xml)|*.xml";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() == true)
            {
                Zespol.ZapiszXML(dlg.FileName, zespol);
                originalZespol = (Zespol)zespol.Clone();
            }
        }
        private void MenuOtworz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents (.xml)|*.xml";
            dlg.FilterIndex = 1;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == true)
                Otworz(dlg.FileName);
        }

        private void Otworz(string filepath)
        {
            zespol = Zespol.OdczytajXML(filepath);
            originalZespol = (Zespol)zespol.Clone();

            lista = new ObservableCollection<CzlonekZespolu>(zespol.Czlonkowie);
            libCzlonkowieZespolu.ItemsSource = lista;
            
            tbNazwaZespolu.Text = zespol.Nazwa;
            tbKierownik.Text = zespol.Kierownik.ToString();
        }

        private void MenuWyjdz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(CzyZmodyfikowany(zespol))
            {
                MessageBoxResult result = MessageBox.Show("Czy chcesz zapisać zmiany?", "Zapisywanie", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                    Zapisz();
                else if(result == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }
        private bool CzyZmodyfikowany(Zespol zespol)
        {
            return zespol != null && !zespol.Equals(originalZespol);
        }
        private void tbNazwaZespolu_TextChanged(object sender, TextChangedEventArgs e)
        {
            zespol.Nazwa = tbNazwaZespolu.Text;
        }
    }
}