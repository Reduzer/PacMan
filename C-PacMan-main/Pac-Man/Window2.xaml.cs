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
        int felderBewegt = 0;
        #endregion

        #region Bewegungen

        //Wenn die Bewegung möglich ist
        public bool hoch, runter, rechts, links;
        public bool hochnicht, runternicht, rechtsnicht, linksnicht = false;

        public double geschwindigkeit = 4 * 8;
        //double geistergeschwindigkeit = 3;

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
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 0, 1, 3, 3, 3, 3, 3, 3, 1, 0, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 0, 0, 0, 0, 0},
            {1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 1},
            {1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1},
            {1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1},
            {1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 1},
            {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1},
            {1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1},
            {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        };

        #endregion

        public Window2()
        {
            InitializeComponent();
            
            #region Anzeige von Leben und Punkten
            Leben.Content = "Leben: " + lebenanzahl;
            Punkte.Content = "Punkte: " + punkteanzahl;
            #endregion


            #region Bilder

            
            // = new BitmapImage(new Uri(@"Bilder etc / Ghost_red.png"));

            #endregion

            MapZeichnen();
            Spielfeld.Focus();

        }

        private void aktivesspiel()
        {
            //  @Fyn:
            // wofür ist "RenderTransformOrigin="0.5,0.5" Im xalm vom pacman???

            #region Bewegung

            if (links == true && linksnicht == false && Canvas.GetLeft(spieler) > 32)
            {
                Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) - geschwindigkeit));
                felderBewegt++;
            }

            if (rechts == true && rechtsnicht == false && Canvas.GetLeft(spieler) < 840)
            {
                Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) + geschwindigkeit));
                felderBewegt++;
            }

            if (hoch == true && hochnicht == false && Canvas.GetTop(spieler) > 0)
            {
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) - geschwindigkeit));
                felderBewegt++;
            }

            if (runter == true && runternicht == false && Canvas.GetTop(spieler) < 976)
            {
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) + geschwindigkeit));
                felderBewegt++;
            }

            int temp = CollisionPacman(spieler);
            if (temp % 2 == 1)
            {
                hochnicht = true;
                temp--;
            }
            else
            {
                hochnicht = false;
            }
            if (temp % 4 == 2)
            {
                linksnicht = true;
                temp--;
                temp--;
            }
            else
            {
                linksnicht = false;
            }
            if (temp % 8 == 4)
            {
                runternicht = true;
                temp--;
                temp--;
                temp--;
                temp--;
            }
            else
            {
                runternicht = false;
            }
            if (temp % 16 == 8)
            {
                rechtsnicht = true;
            }
            else
            {
                rechtsnicht = false;
            }
            #endregion

            #region

            #endregion


        }

        public void Textausgabe(int zahl)
        {
            Rectangle mapTile = new Rectangle();
            Waende.Children.Add(mapTile);
            Grid.SetColumn(mapTile, 12);
            Grid.SetRow(mapTile, 14);
            mapTile.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            TextBlock num = new TextBlock();
            Waende.Children.Add(num);
            Grid.SetColumn(num, 12);
            Grid.SetRow(num, 14);
            num.Text = Convert.ToString(zahl);
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
                spieler.RenderTransform = new RotateTransform(0);
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
                spieler.RenderTransform = new RotateTransform(90);
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
                spieler.RenderTransform = new RotateTransform(270);
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

        public int CollisionPacman(Rectangle rechteck)
        {
            #region Collision Test

            #region Variablen und Berechnungen

            int temp1 = 0;                                      //decoden nach bit logic: bit 0 = kollision oben
            int temp2 = 0;                                      //bit 1 = kollision links
            int temp4 = 0;                                      //bit 2 = kollision unten
            int temp8 = 0;                                      //bit 3 = kollision rechts
            int sum = 0;                                        //Summe erstellenn
            int collom = 0;                                     //Spalte erstellen
            int row = 0;                                        //Zeile erstellen
            int x = Convert.ToInt32(Canvas.GetLeft(rechteck));  // Y-Varaible wird festgelegt
            int y = Convert.ToInt32(Canvas.GetTop(rechteck));   // X-Varaible wird festgelegt
            collom = (x - (x % 32)) / 32;                       // Spalte berechnen
            row = (y - (y % 32)) / 32;                          // Zeile berechnen
            #endregion

            #region Prüfung

            if (x % 32 == 0 && y % 32 == 0)
            {
                if (map[row - 1, collom] == 1 || map[row - 1, collom] == 3)
                {
                    temp1 = 1;
                }
                if (map[row, collom - 1] == 1 || map[row, collom - 1] == 3)
                {
                    temp2 = 2;
                }
                if (map[row + 1, collom] == 1 || map[row + 1, collom] == 3)
                {
                    temp4 = 4;
                }
                if (map[row, collom + 1] == 1 || map[row, collom +1] == 3)
                {
                    temp8 = 8;
                }
                if (map[row, collom] == 2)
                {
                    punkteanzahl++;
                    Textausgabe(punkteanzahl);
                    Rectangle mapTile = new Rectangle();
                    map[row, collom] = 0; Waende.Children.Add(mapTile);
                    Grid.SetColumn(mapTile, collom);
                    Grid.SetRow(mapTile, row);
                    mapTile.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    Punkte.Content = punkteanzahl;


                }
            }

            sum = (temp1 + temp2 + temp4 + temp8);
            return sum;
            #endregion
            #endregion
        }
    }
}