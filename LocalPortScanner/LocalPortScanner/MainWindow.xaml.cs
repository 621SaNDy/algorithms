using System;
using System.Net.Sockets;
using System.Net;
using System.Windows;
using System.Threading;
using System.Windows.Forms;

namespace LocalPortScanner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbFrom.Text = "1";
            tbTo.Text = "65535";
        }

        TcpListener server;
        Thread scanner;

        private void Scan()
        {
            int from = 0;
            int to = 0;

            tbFrom.Dispatcher.Invoke(new MethodInvoker(delegate
            {
                from = Int32.Parse(tbFrom.Text);
            }));;

            tbTo.Dispatcher.Invoke(new MethodInvoker(delegate
            {
                to = Int32.Parse(tbTo.Text);
            }));

            libResults.Dispatcher.Invoke(new MethodInvoker(delegate {
                libResults.Items.Add("Start a scan...");
            }));

            for (int i = from; i <= to; i++)
            {
                lbInfo.Dispatcher.Invoke(new MethodInvoker(delegate {
                    lbInfo.Content = "Currently, the port scan: " + i;
                }));
                try
                {
                    server = new TcpListener(IPAddress.Loopback, i);
                    server.Start();
                    server.Stop();
                }
                catch
                {
                    libResults.Dispatcher.Invoke(new MethodInvoker(delegate {
                        libResults.Items.Add("Port: " + i + " is closed");
                    }));
                }
            }
            lbInfo.Dispatcher.Invoke(new MethodInvoker(delegate
            {
                lbInfo.Content = "Port scan completed";
            }));

            libResults.Dispatcher.Invoke(new MethodInvoker(delegate
            {
                libResults.Items.Add("Port scan completed");
            }));

            btnAction.Dispatcher.Invoke(new MethodInvoker(delegate
            {
                btnAction.Content = "Start";
            }));
            
            btnStop.Dispatcher.Invoke(new MethodInvoker(delegate
            {
                btnStop.IsEnabled = false;
            }));
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            switch (btnAction.Content)
            {
                case "Start":
                    if (tbFrom.Text != "" && tbTo.Text != "" && Int32.Parse(tbFrom.Text) < Int32.Parse(tbTo.Text) && Int32.Parse(tbFrom.Text) > 0)
                    {
                        libResults.Items.Clear();
                        scanner = new Thread(Scan);
                        scanner.Start();
                        btnAction.Content = "Pause";
                        btnStop.IsEnabled = true;
                    }
                    break;
                case "Pause":
                    btnAction.Content = "Resume";
                    btnStop.IsEnabled = false;
                    scanner.Suspend();
                    libResults.Items.Add("Port scan paused");
                    break;
                case "Resume":
                    scanner.Resume();
                    btnAction.Content = "Pause";
                    btnStop.IsEnabled = true;
                    break;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            scanner.Abort();
            btnAction.Content = "Start";
            libResults.Items.Add("Port scan stopped");
            btnStop.IsEnabled = false;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (scanner != null && btnAction.Content != "Resume")
            {
                scanner.Abort();
            } else if(btnAction.Content == "Resume") {
                scanner.Resume();
                scanner.Abort();
            }
        }
    }
}
