using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace NetHW4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReceiveButton(object sender, RoutedEventArgs e)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

            server.SendTo(Encoding.ASCII.GetBytes("screen"), serverEndPoint);
            byte[] buffer = new byte[8192 * 1024];
            while (true)
            {
                server.ReceiveFrom(buffer, ref serverEndPoint);
                if (buffer.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(buffer);
                    BitmapImage returnImage = new BitmapImage();
                    returnImage.StreamSource = ms;
                    imageBox.Source = returnImage;
                    break;
                }
            }
        }
    }
}
