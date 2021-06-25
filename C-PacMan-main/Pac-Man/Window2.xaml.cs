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
    /// <summary>
    /// Interaktionslogik für Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        #region Anzeige_Sachen
        int lebenanzahl = 3;
        int punkteanzahl = 0;
        #endregion

        #region Bewegungen

        //Wenn die Bewegung möglich ist
        public bool hoch, runter, rechts, links;

        public double geschwindigkeit = 5;
        //double geistergeschwindigkeit = 3;
        public bool intersection;

        #endregion

        //irgenwas für eine Hitbox
        #region
        /*
        Die erste Ueberlegung ist ein weiteres XAML Rectangle über das vom Pac-Man zu legen welches geprüft wird.
        Möglich wäre dies mit dem Ueberpruefen, ob diese "Hitbox" mit deinem Object mit dem Tag "Wall" in Beruehrung
        ist und damit dann die Bewegung in diese Richtung zu deaktivieren.
        

        */

        /*
        if(hitbox (hat die Gleiche position wie) Tag.Wand)
        {
            hoch, runter, rechts, link = false
            
        }
        */

        #endregion


        public Window2()
        {
            InitializeComponent();
            #region
            Leben.Content = "Deine Leben: " + lebenanzahl;
            Punkte.Content = "Deine Punkte: " + punkteanzahl;
            #endregion


            //Irgendwas um die Bilder zu Importieren
            #region Bilder / Models

            //Roter Geist
            ImageBrush rotergeist = new ImageBrush();
            //Irgendwas für ImageSource oder so

            //Lila Geist
            ImageBrush lilageist = new ImageBrush();
            //Irgendwas für ImageSource oder so

            //Blauer Geist
            ImageBrush blauergeist = new ImageBrush();
            //Irgendwas für ImageSource oder so

            //Pac-Man
            ImageBrush pacman = new ImageBrush();
            //Irgendwas für ImageSource oder so

            //Kirsche
            ImageBrush kirsche = new ImageBrush();
            //Irgendwas für ImageSource oder so

            //Punkt
            ImageBrush punkt = new ImageBrush();
            //Irgendwas für ImageSource oder so

            //Punktg
            ImageBrush punktg = new ImageBrush();
            //Irgendwas für ImageSource oder so


            #endregion

            Spielfeld.Focus();

        }


        private void aktivesspiel()
        {

            #region Bewegung
            //Canvas.Left und Canvas.Top in XAML code vorhanden evtl in C# anwendbar?

            /*
            Ja ist anwendbar aber muss mit der groeße des Fensters abgestimmt werden,
            damit der Spieler nicht vom Bildschirm runter kann
            */

            //Möglich über Rect.IntersectWith(Rect)
            Rect spielerrect = new Rect();
            
            Rect wandrect = new Rect();

            if (links == true && Canvas.GetLeft(spieler) > 0)
            {
                Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) - geschwindigkeit));
                if (spielerrect.IntersectsWith(wandrect) == false)
                {
                    Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) + geschwindigkeit));
                }
            }

            if(rechts == true && Canvas.GetLeft(spieler) < 670)
            {
                Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) + geschwindigkeit));
                if (spielerrect.IntersectsWith(wandrect) == false)
                {
                    Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) - geschwindigkeit));
                }
            }

            if(hoch == true && Canvas.GetTop(spieler) > 0)
            {
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) - geschwindigkeit));
                if (spielerrect.IntersectsWith(wandrect) == false)
                {
                    Canvas.SetTop(spieler, (Canvas.GetTop(spieler) + geschwindigkeit));
                }
            }

            if(runter == true && Canvas.GetTop(spieler) < 750)
            {
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) + geschwindigkeit));
                if (spielerrect.IntersectsWith(wandrect) == false)
                {
                    Canvas.SetTop(spieler, (Canvas.GetTop(spieler) - geschwindigkeit));
                }
            }
            #endregion

        }

        private void drehung(Object sender, KeyEventArgs e)
        {
            
            #region Bewegung Selbst

            //Evtl möglich über if
            /*
            Ja ist möglich die Abfrage für das KeyEventArgs müss dem Entsprechenden den Key angepasst werden
            Die Booleans die nicht benötigt werden, werden auf false gesetzt um fehler zu vermeiden.

            P.S:
            Der Spieler kann trotz das die Booleans auf false gesetzt werden gleichzeitig z.B. rechts und hoch drücken
            Hierdurch bewegt sich der Spieler Quer über den Bildschirm.
             */


            if(e.Key == Key.Left)
            {
                //Abfrage für nicht links?
                hoch   = false;
                runter = false;
                rechts = false;

                links  = true;

                //PacMan drehen?
                spieler.RenderTransform = new RotateTransform(-180);
                aktivesspiel();

            }

            if (e.Key == Key.Right)
            {
                //Abfrage für nicht links?
                hoch = false;
                runter = false;
                links = false;

                rechts = true;

                //PacMan drehen?
                spieler.RenderTransform = new RotateTransform(180);
                aktivesspiel();

            }

            if (e.Key == Key.Up)
            {
                //Abfrage für nicht links?
                rechts = false;
                runter = false;
                links = false;

                hoch = true;

                //PacMan drehen?
                spieler.RenderTransform = new RotateTransform(180);
                aktivesspiel();

            }

            if (e.Key == Key.Down)
            {
                //Abfrage für nicht links?
                hoch = false;
                rechts = false;
                links = false;

                runter = true;

                //PacMan drehen?
                spieler.RenderTransform = new RotateTransform(180);
                aktivesspiel();

            }

            #endregion
            
        }
        
    }
}
