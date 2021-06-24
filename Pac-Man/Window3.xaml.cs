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
using System.Windows.Shapes;

namespace Pac_Man
{
    
    public partial class Window3 : Window
    {
        #region Anzeige_Sachen
        int lebenanzahl = 3;
        int punkteanzahl = 0;
        #endregion

        #region Bewegungen

        //Wenn die Bewegung möglich ist
        //bool hoch, runter, rechts, links;

        //Wenn die Bewegung nicht möglich ist
        //bool hochnicht, runternicht, rechtsnicht, linksnicht;
        #endregion

        //irgenwas für eine hitbox
        #region

        //Hier muss Code hin :D
        #endregion

        public Window3()
        {
            InitializeComponent();

            #region
            Leben.Content = "Deine Leben: " + lebenanzahl;
            Punkte.Content = "Deine Punkte: " + punkteanzahl;
            #endregion

            setup();
        }

        private void setup()
        {

        }
    }
}
