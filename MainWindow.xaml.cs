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
            Textfuerquelle.Visibility = Visibility.Hidden;
        }

        void button1_click(object sender, RoutedEventArgs e)
        {
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;

            Spiel1.Visibility = Visibility.Visible;
        }

        void button2_click(object sender, RoutedEventArgs e)
        {
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;

            Spiel1.Visibility = Visibility.Visible;

        }

        void button3_click(object sender, RoutedEventArgs e)
        {
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;

            Textfuerquelle.Visibility = Visibility.Visible;

            string string1 = "Dies ist ein kleines Projekt auf der Basis, von dem Spiel PacMan aus 1980, von Namico";

            Text.Text = string1;
        }
    }
}
