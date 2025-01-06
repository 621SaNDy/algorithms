using MySql.Data.MySqlClient;
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

namespace KsiazkaAdresowa
{
    /// <summary>
    /// Logika interakcji dla klasy Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        MySqlConnection connection;
        bool ifConnected;
        int ID;

        public Edit(MySqlConnection connection, bool ifConnected, Contact osoba) :this()
        {
            this.connection = connection;
            this.ifConnected = ifConnected;
            tbImie.Text = osoba.Name;
            tbWiek.Text = osoba.Age.ToString();
            ID = osoba.ID;
        }

        public Edit()
        {
            InitializeComponent();
        }

        private void btnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (tbImie.Text != "" && tbWiek.Text != "")
            {
                string query = "UPDATE contacts SET Name = '" + tbImie.Text + "', Age = " + tbWiek.Text + " WHERE ID = " + ID;

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
