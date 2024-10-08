using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Steamworks;

namespace Steam_Booster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.Sleep(2000);
                try
                {

                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (!SteamAPI.Init())
                        {
                            MessageBox.Show("Failed to initialize Steam API. Make sure Steam is running and logged in");
                            Environment.Exit(0);
                            return;
                        }
                        if (!SteamAPI.IsSteamRunning())
                        {
                            MessageBox.Show("Steam is not running.");
                            Environment.Exit(0);
                            return;
                        }
                        ua.Content = "Active";
                    });

                    while (true)
                    {
                        SteamUserStats.RequestCurrentStats();
                        SteamAPI.RunCallbacks();
                        Thread.Sleep(40000);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }).Start();
        }

    }
}