using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace KsiazkaAdresowa
{
    /// <summary>
    /// Logika interakcji dla klasy Dodaj.xaml
    /// </summary>
    public partial class Dodaj : Window
    {
        MySqlConnection connection;
        bool ifConnected;

        public Dodaj(MySqlConnection connection, bool ifConnected) :this()
        {
            this.connection = connection;
            this.ifConnected = ifConnected;
        }

        public Dodaj()
        {
            InitializeComponent();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (tbImie.Text != "" && tbWiek.Text != "")
            {
                Contact contact = new Contact();
                contact.Name = tbImie.Text;
                contact.Age = Convert.ToInt32(tbWiek.Text);

                string query = "INSERT INTO contacts (Name, Age) VALUES ('" + contact.Name + "', " + contact.Age + ")";

                MySqlCommand command = new MySqlCommand(query, connection);
                
                connection.Open();
                ifConnected = true;
                command.ExecuteReader();
                connection.Close();
                ifConnected = false;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Wprowadz dane", "Bład", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
