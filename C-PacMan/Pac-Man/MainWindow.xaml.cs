using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pac_Man
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Spiel1.Visibility = Visibility.Hidden;
            Button3.Content = "Quellen etc.";
        }

        void button1_click(object sender, RoutedEventArgs e)
        {
            
            Window3 window3 = new Window3();
            window3.Show();
        }

        void button2_click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();

        }

        void button3_click(object sender, RoutedEventArgs e)
        {
            string browser = "https://docs.google.com/document/d/1peTHOFrWBh6jtBsXBjmdgWjRwLELo6mfUTwUWQz3DZg/edit?usp=sharing";
            try
            {
                System.Diagnostics.Process.Start(browser);
            }
            catch
            {

            }
        }
    }
}
