using CliChat.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace CliChat.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ClientApp Client { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            var address = TextBox_Address.Text;
            var port = TextBox_Port.Text;
            var username = TextBox_Username.Text;

            Client = new ClientApp(address, int.Parse(port), username);
            Client.Connect();
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            var input = TextBox_Input.Text;
            TextBox_Input.Text = "";
            Client.SendMessage(input);
        }
    }
}
