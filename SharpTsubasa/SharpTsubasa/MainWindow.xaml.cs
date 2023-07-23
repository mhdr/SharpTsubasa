using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdvancedSharpAdbClient;
using SharpTsubasa.Libs;

namespace SharpTsubasa
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Log(string msg, Enums.LogColor color = Enums.LogColor.Default)
        {
            DateTime localTime = DateTime.Now;
            TextBlock textBlockDate = new TextBlock();
            textBlockDate.Margin = new Thickness(5, 5, 1, 0);
            textBlockDate.FontWeight = FontWeights.Bold;
            textBlockDate.Text = localTime.ToString(CultureInfo.InvariantCulture) + ":";

            TextBlock textBlockValue = new TextBlock();
            textBlockValue.Margin = new Thickness(1, 5, 0, 0);

            if (color == Enums.LogColor.Green)
            {
                textBlockValue.Foreground = Brushes.Green;
            }
            else if (color == Enums.LogColor.Red)
            {
                textBlockValue.Foreground = Brushes.Red;
            }

            textBlockValue.Text = msg;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(textBlockDate);
            stackPanel.Children.Add(textBlockValue);
            StackPanelLog.Children.Insert(0, stackPanel);
        }

        private void ButtonStartAdb_Click(object sender, RoutedEventArgs e)
        {
            if (!AdbServer.Instance.GetStatus().IsRunning)
            {
                AdbServer server = new AdbServer();
                StartServerResult result = server.StartServer(@"C:\Program Files (x86)\Nox\bin\adb.exe", false);
                if (result != StartServerResult.Started)
                {
                    Log("Can't start adb server", Enums.LogColor.Red);
                }
                else
                {
                    Log("Adb server is started", Enums.LogColor.Green);
                }
            }
            else
            {
                Log("Running", Enums.LogColor.Green);
            }
        }
    }
}