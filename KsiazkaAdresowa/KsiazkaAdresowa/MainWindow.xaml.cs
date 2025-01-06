using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace KsiazkaAdresowa
{
    public struct Contact
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbServer.Text = "localhost";
            tbDataBase.Text = "contacts";
            tbUID.Text = "root";
        }

        public MySqlConnection connection;
        string myconnection;
        bool ifConnected = false;

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(btnConnect.Content.ToString() != "Disconnect")
            {
                myconnection =
                    "SERVER=" + tbServer.Text + ";" +
                    "DATABASE=" + tbDataBase.Text + ";" +
                    "UID=" + tbUID.Text + ";" +
                    "PASSWORD=" + tbPassword.Text + ";" +
                    "SslMode=none;" + ";";

                connection = new MySqlConnection(myconnection);

                try
                {
                    connection.Open();
                    ifConnected = true;
                    btnConnect.Content = "Disconnect";
                    lbConnection.Content = "Connected";
                    btnGetData.IsEnabled = true;
                    btnAddRows.IsEnabled = true;
                    btnEditRow.IsEnabled = true;
                    btnDeleteRow.IsEnabled = true;
                }

                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString(), "Bład Połączenia", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                btnConnect.Content = "Connect";
                lbConnection.Content = "No connect";
                btnGetData.IsEnabled = false;
                btnAddRows.IsEnabled = false;
                btnEditRow.IsEnabled = false;
                btnDeleteRow.IsEnabled = false;

                connection.Close();
                ifConnected = false;
                dgContacts.Items.Clear();
            }
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            dgContacts.Items.Clear();

            if (ifConnected == false)
            {
                connection.Open();
                ifConnected = true;
            }

            MySqlCommand command = new MySqlCommand("SELECT * FROM contacts", connection);

            using (var record = command.ExecuteReader())
            {
                while (record.Read())
                {
                    Contact contact = new Contact();
                    contact.ID = int.Parse(record["ID"].ToString());
                    contact.Name = record["Name"].ToString();
                    contact.Age = int.Parse(record["Age"].ToString());
                    dgContacts.Items.Add(contact);
                }
            }
            connection.Close();
            ifConnected = false;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Dodaj okno = new Dodaj(connection, ifConnected);
            okno.ShowDialog();

            if(okno.DialogResult == true)
            {
                btnGetData_Click(sender, e);
            }
        }

        private void btnEditRow_Click(object sender, RoutedEventArgs e)
        {
            if (dgContacts.SelectedItem != null)
            {
                Contact contact = (Contact)dgContacts.SelectedItem;
                Edit okno = new Edit(connection, ifConnected, contact);
                okno.ShowDialog();

                if (okno.DialogResult == true)
                {
                    btnGetData_Click(sender, e);
                }
            }
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (dgContacts.SelectedItem != null)
            {
                Contact contact = (Contact)dgContacts.SelectedItem;

                string query = "DELETE FROM contacts WHERE ID = " + contact.ID;

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                ifConnected = true;
                command.ExecuteReader();
                connection.Close();
                ifConnected = false;
                btnGetData_Click(sender, e);
            }
        }
    }
}
