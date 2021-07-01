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
        public bool hochnicht, runternicht, rechtsnicht, linksnicht = false;

        public double geschwindigkeit = 4;
        //double geistergeschwindigkeit = 3;
        public bool intersection;

        public bool mapDone = false;

        public int[,] map = new int[31, 28]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 0, 0, 1, 2, 1, 0, 0, 0, 1, 2, 1, 1, 2, 1, 0, 0, 0, 1, 2, 1, 0, 0, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 0, 1, 1, 1, 3, 3, 1, 1, 1, 0, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 1},
            {1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1},
            {1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        };


        #endregion

        public Window2()
        {
            InitializeComponent();
            #region
            Leben.Content = "Deine Leben: " + lebenanzahl;
            Punkte.Content = "Deine Punkte: " + punkteanzahl;
            #endregion

            MapZeichnen();
            Spielfeld.Focus();

        }


        private void aktivesspiel()
        {

            #region Wand oder keine Wand
            //Canvas.Left und Canvas.Top in XAML code vorhanden evtl in C# anwendbar?

            /*
            Ja ist anwendbar aber muss mit der groeße des Fensters abgestimmt werden,
            damit der Spieler nicht vom Bildschirm runter kann
            */

            //Möglich über Rect.IntersectWith(Rect)



            /*
            int array1 = new int();
            array1 = {
            "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1",
 
            };
            */

            
            #endregion

            #region Bewegung
            if (links == true && linksnicht == false && Canvas.GetLeft(spieler) > 0)
            {
                Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) - geschwindigkeit));
            }

            if (rechts == true && rechtsnicht == false && Canvas.GetLeft(spieler) < 872)
            {
                Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) + geschwindigkeit));
            }

            if (hoch == true && hochnicht == false && Canvas.GetTop(spieler) > 0)
            {
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) - geschwindigkeit));
            }

            if (runter == true && runternicht == false && Canvas.GetTop(spieler) < 976)
            {
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) + geschwindigkeit));
            }
            #endregion

        }

        public void MapZeichnen()
        {
            
            if (mapDone == false)
            {
                for (int i = 0; i < 28; i++)
                {
                    for (int j = 0; j < 31; j++)
                    {
                        Rectangle mapTile = new Rectangle();
                        switch (map[j, i])
                        {
                            case 0:
                                Waende.Children.Add(mapTile);
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                                break;
                            case 1:
                                Waende.Children.Add(mapTile);
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 70));
                                mapTile.StrokeThickness = 12;
                                mapTile.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 100));
                                break;
                            case 2:
                                Waende.Children.Add(mapTile);
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 50));
                                mapTile.StrokeThickness = 14;
                                mapTile.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                                break;
                            case 3:
                                Waende.Children.Add(mapTile);
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                                break;
                            default:
                                Waende.Children.Add(mapTile);
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                                break;
                        }
                        mapDone = true;
                    }
                }
            }
        }

        private void drehung(Object sender, KeyEventArgs e)
        {

            #region Bewegung Selbst
            if (e.Key == Key.Left)
            {
                //Abfrage für nicht links?
                hoch = false;
                runter = false;
                rechts = false;

                links = true;

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
        public int DotsRemaining()
        {
            int dots = 0;
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if(map[j,i] == 2)
                    {
                        dots++;
                    }
                }
            }
            return dots;
        }

        public int[] Collision(rechteckname)
        {
            int collision = 0;
            int x;
            int y;
            int collom = 0;
            int row = 0;
            x = Canvas.GetLeft();
            y = Canvas.GetTop();
            collom = (x - (x % 32)) / 32;
            row = (x - (x % 32)) / 32;
            if (map[row,collom] == 1)
            {

            }
        }
    }
}