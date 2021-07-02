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

        #region Vareablen

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

        public int richtung1 = 1;
        public int richtung2 = 1;
        public int richtung3 = 1;
        public int richtung4 = 1;

        Random rnd = new Random();

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
            if (links == true && Canvas.GetLeft(spieler) >= 32)
            {
                if (Canvas.GetTop(spieler) == 448 && Canvas.GetLeft(spieler) == 32)
                {
                    Canvas.SetLeft(spieler, 832);
                    felderBewegt++;
                }
                else if (links == true && linksnicht == false && Canvas.GetLeft(spieler) > 32)
                {
                    Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) - geschwindigkeit));
                    felderBewegt++;
                }
            }
            if (rechts == true&& Canvas.GetLeft(spieler) <= 840)
            {
                if (Canvas.GetTop(spieler) == 448 && Canvas.GetLeft(spieler) == 832)
                {
                    Canvas.SetLeft(spieler, 32);
                    felderBewegt++;
                }
                else if (rechts == true && rechtsnicht == false && Canvas.GetLeft(spieler) < 840)
                {
                    Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) + geschwindigkeit));
                    felderBewegt++;
                }
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
            #endregion

            #region Mögliche nächste Bewegungen
            int temp = CollisionPacman();
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

            Geist1Bewegen();
            Geist2Bewegen();
            Geist3Bewegen();
            Geist4Bewegen();

        }

        public void Textausgabe(int zahl)
        {
            Rectangle mapTile = new Rectangle();
            Waende.Children.Add(mapTile);
            Grid.SetColumn(mapTile, 11);
            Grid.SetRow(mapTile, 15);
            mapTile.Fill = new SolidColorBrush(Color.FromRgb(50, 0, 0));
            TextBlock num = new TextBlock();
            Waende.Children.Add(num);
            Grid.SetColumn(num, 11);
            Grid.SetRow(num, 15);
            num.Text = Convert.ToString(zahl);
            num.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
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
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(50, 0, 0));
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

        public void Geist1Bewegen()
        {
            if (felderBewegt == 49)
            {
                Canvas.SetTop(ghost1, 352);
                Canvas.SetLeft(ghost1, 416);
            }
            if (felderBewegt > 50)                          //geister nur bewegen wenn der spieler mehr als x felder gegangen ist
            {
                bool[] möglichkeiten = new bool[4];
                for(int i = 0; i < 4; i++)
                {
                    möglichkeiten[i] = true;
                }
                int collom = 0;                                     //Spalte erstellen
                int row = 0;                                        //Zeile erstellen
                int x = Convert.ToInt32(Canvas.GetLeft(ghost1));
                int y = Convert.ToInt32(Canvas.GetTop(ghost1));

                #region Geist Wanderkennung
                collom = (x - (x % 32)) / 32;                       // Spalte berechnen
                row = (y - (y % 32)) / 32;   
                if (x % 32 == 0 && y % 32 == 0)
                {
                    if (map[row - 1, collom] == 1 || map[row -1, collom] == 3)
                    {
                        möglichkeiten[0] = false;
                    }
                    if (map[row, collom - 1] == 1 || map[row, collom -1] == 3)
                    {
                        möglichkeiten[1] = false;
                    }
                    if (map[row + 1, collom] == 1 || map[row +1, collom ] == 3)
                    {
                        möglichkeiten[2] = false;
                    }
                    if (map[row, collom + 1] == 1 || map[row, collom + 1] == 3)
                    {
                        möglichkeiten[3] = false;
                    }

                }
                if(rnd.Next(0, 5) == 0 || möglichkeiten[richtung1 - 1] == false)                        //ungefär alle 7 schritte die richtung ändern
                {
                    richtung1 = 0;
                    while (richtung1 == 0)
                    {
                        int random = rnd.Next(0, 4);
                        if(möglichkeiten[random] == true)
                        {
                            richtung1 = random  +1;
                        }
                    }
                }

                if(richtung1 == 2 && möglichkeiten[1] == true && Canvas.GetLeft(ghost1) >= 32)
            {
                    if (Canvas.GetTop(ghost1) == 448 && Canvas.GetLeft(ghost1) < 32)
                    {
                        Canvas.SetLeft(ghost1, 832);
                    }
                    else if (Canvas.GetLeft(ghost1) > 32)
                    {
                        Canvas.SetLeft(ghost1, (Canvas.GetLeft(ghost1) - geschwindigkeit));
                    }
                }
                if (richtung1 == 4 && möglichkeiten[3] == true && Canvas.GetLeft(ghost1) <= 840)
                {
                    if (Canvas.GetTop(ghost1) == 448 && Canvas.GetLeft(ghost1) == 832)
                    {
                        Canvas.SetLeft(ghost1, 32);
                    }
                    else if (Canvas.GetLeft(ghost1) < 840)
                    {
                        Canvas.SetLeft(ghost1, (Canvas.GetLeft(ghost1) + geschwindigkeit));
                    }
                }
                if (richtung1 == 1 && möglichkeiten[0] == true && Canvas.GetTop(ghost1) > 0)
                {
                    Canvas.SetTop(ghost1, (Canvas.GetTop(ghost1) - geschwindigkeit));
                }

                if (richtung1 == 3 && möglichkeiten[2] == true && Canvas.GetTop(ghost1) < 976)
                {
                    Canvas.SetTop(ghost1, (Canvas.GetTop(ghost1) + geschwindigkeit));
                }
                #endregion
            }
        }
        public void Geist2Bewegen()
        {
            if (felderBewegt == 99)
            {
                Canvas.SetTop(ghost2, 352);
                Canvas.SetLeft(ghost2, 416);
            }
            if (felderBewegt > 100)                          //geister nur bewegen wenn der spieler mehr als x felder gegangen ist
            {
                bool[] möglichkeiten = new bool[4];
                for (int i = 0; i < 4; i++)
                {
                    möglichkeiten[i] = true;
                }
                int collom = 0;                                     //Spalte erstellen
                int row = 0;                                        //Zeile erstellen
                int x = Convert.ToInt32(Canvas.GetLeft(ghost2));
                int y = Convert.ToInt32(Canvas.GetTop(ghost2));

                #region Geist Wanderkennung
                collom = (x - (x % 32)) / 32;                       // Spalte berechnen
                row = (y - (y % 32)) / 32;
                if (x % 32 == 0 && y % 32 == 0)
                {
                    if (map[row - 1, collom] == 1 || map[row -1 , collom] == 3)
                    {
                        möglichkeiten[0] = false;
                    }
                    if (map[row, collom - 1] == 1 || map[row, collom - 1] == 3)
                    {
                        möglichkeiten[1] = false;
                    }
                    if (map[row + 1, collom] == 1 || map[row +1, collom] == 3)
                    {
                        möglichkeiten[2] = false;
                    }
                    if (map[row, collom + 1] == 1 || map[row, collom + 1] == 3)
                    {
                        möglichkeiten[3] = false;
                    }

                }
                if (rnd.Next(0, 6) == 0 || möglichkeiten[richtung2 - 1] == false)                        //ungefär alle 7 schritte die richtung ändern
                {
                    richtung2 = 0;
                    while (richtung2 == 0)
                    {
                        int random = rnd.Next(0, 4);
                        if (möglichkeiten[random] == true)
                        {
                            richtung2 = random + 1;
                        }
                    }
                }

                if (richtung2 == 2 && möglichkeiten[1] == true && Canvas.GetLeft(ghost2) >= 32)
                {
                    if (Canvas.GetTop(ghost2) == 448 && Canvas.GetLeft(ghost2) < 32)
                    {
                        Canvas.SetLeft(ghost2, 832);
                    }
                    else if (Canvas.GetLeft(ghost2) > 32)
                    {
                        Canvas.SetLeft(ghost2, (Canvas.GetLeft(ghost2) - geschwindigkeit));
                    }
                }
                if (richtung2 == 4 && möglichkeiten[3] == true && Canvas.GetLeft(ghost2) <= 840)
                {
                    if (Canvas.GetTop(ghost2) == 448 && Canvas.GetLeft(ghost2) == 832)
                    {
                        Canvas.SetLeft(ghost2, 32);
                    }
                    else if (Canvas.GetLeft(ghost2) < 840)
                    {
                        Canvas.SetLeft(ghost2, (Canvas.GetLeft(ghost2) + geschwindigkeit));
                    }
                }
                if (richtung2 == 1 && möglichkeiten[0] == true && Canvas.GetTop(ghost2) > 0)
                {
                    Canvas.SetTop(ghost2, (Canvas.GetTop(ghost2) - geschwindigkeit));
                }

                if (richtung2 == 3 && möglichkeiten[2] == true && Canvas.GetTop(ghost2) < 976)
                {
                    Canvas.SetTop(ghost2, (Canvas.GetTop(ghost2) + geschwindigkeit));
                }
                #endregion
            }
        }
        public void Geist3Bewegen()
        {
            if (felderBewegt == 149)
            {
                Canvas.SetTop(ghost3, 352);
                Canvas.SetLeft(ghost3, 416);
            }
            if (felderBewegt > 150)                          //geister nur bewegen wenn der spieler mehr als x felder gegangen ist
            {
                bool[] möglichkeiten = new bool[4];
                for (int i = 0; i < 4; i++)
                {
                    möglichkeiten[i] = true;
                }
                int collom = 0;                                     //Spalte erstellen
                int row = 0;                                        //Zeile erstellen
                int x = Convert.ToInt32(Canvas.GetLeft(ghost3));
                int y = Convert.ToInt32(Canvas.GetTop(ghost3));

                #region Geist Wanderkennung
                collom = (x - (x % 32)) / 32;                       // Spalte berechnen
                row = (y - (y % 32)) / 32;
                if (x % 32 == 0 && y % 32 == 0)
                {
                    if (map[row - 1, collom] == 1 || map[row -1, collom] == 3)
                    {
                        möglichkeiten[0] = false;
                    }
                    if (map[row, collom - 1] == 1 || map[row, collom -1] == 3)
                    {
                        möglichkeiten[1] = false;
                    }
                    if (map[row + 1, collom] == 1 || map[row +1, collom] == 3)
                    {
                        möglichkeiten[2] = false;
                    }
                    if (map[row, collom + 1] == 1 || map[row, collom + 1] == 3)
                    {
                        möglichkeiten[3] = false;
                    }

                }
                if (rnd.Next(0, 7) == 0 || möglichkeiten[richtung3 - 1] == false)                        //ungefär alle 7 schritte die richtung ändern
                {
                    richtung3 = 0;
                    while (richtung3 == 0)
                    {
                        int random = rnd.Next(0, 4);
                        if (möglichkeiten[random] == true)
                        {
                            richtung3 = random + 1;
                        }
                    }
                }

                if (richtung3 == 2 && möglichkeiten[1] == true && Canvas.GetLeft(ghost3) >= 32)
                {
                    if (Canvas.GetTop(ghost3) == 448 && Canvas.GetLeft(ghost3) < 32)
                    {
                        Canvas.SetLeft(ghost3, 832);
                    }
                    else if (Canvas.GetLeft(ghost3) > 32)
                    {
                        Canvas.SetLeft(ghost3, (Canvas.GetLeft(ghost3) - geschwindigkeit));
                    }
                }
                if (richtung3 == 4 && möglichkeiten[3] == true && Canvas.GetLeft(ghost3) <= 840)
                {
                    if (Canvas.GetTop(ghost3) == 448 && Canvas.GetLeft(ghost3) == 832)
                    {
                        Canvas.SetLeft(ghost3, 32);
                    }
                    else if (Canvas.GetLeft(ghost3) < 840)
                    {
                        Canvas.SetLeft(ghost3, (Canvas.GetLeft(ghost3) + geschwindigkeit));
                    }
                }
                if (richtung3 == 1 && möglichkeiten[0] == true && Canvas.GetTop(ghost3) > 0)
                {
                    Canvas.SetTop(ghost3, (Canvas.GetTop(ghost3) - geschwindigkeit));
                }

                if (richtung3 == 3 && möglichkeiten[2] == true && Canvas.GetTop(ghost3) < 976)
                {
                    Canvas.SetTop(ghost3, (Canvas.GetTop(ghost3) + geschwindigkeit));
                }
                #endregion
            }
        }
        public void Geist4Bewegen()
        {
            if (felderBewegt == 199)
            {
                Canvas.SetTop(ghost4, 352);
                Canvas.SetLeft(ghost4, 416);
            }
            if (felderBewegt > 200)                          //geister nur bewegen wenn der spieler mehr als x felder gegangen ist
            {
                bool[] möglichkeiten = new bool[4];
                for (int i = 0; i < 4; i++)
                {
                    möglichkeiten[i] = true;
                }
                int collom = 0;                                     //Spalte erstellen
                int row = 0;                                        //Zeile erstellen
                int x = Convert.ToInt32(Canvas.GetLeft(ghost4));
                int y = Convert.ToInt32(Canvas.GetTop(ghost4));

                #region Geist Wanderkennung
                collom = (x - (x % 32)) / 32;                       // Spalte berechnen
                row = (y - (y % 32)) / 32;
                if (x % 32 == 0 && y % 32 == 0)
                {
                    if (map[row - 1, collom] == 1 || map[row -1, collom] == 3)
                    {
                        möglichkeiten[0] = false;
                    }
                    if (map[row, collom - 1] == 1 || map[row, collom - 1] == 3)
                    {
                        möglichkeiten[1] = false;
                    }
                    if (map[row + 1, collom] == 1 || map[row +1, collom] == 3)
                    {
                        möglichkeiten[2] = false;
                    }
                    if (map[row, collom + 1] == 1 || map[row, collom + 1] == 3)
                    {
                        möglichkeiten[3] = false;
                    }

                }
                if (rnd.Next(0, 8) == 0 || möglichkeiten[richtung4 - 1] == false)                        //ungefär alle 7 schritte die richtung ändern
                {
                    richtung4 = 0;
                    while (richtung4 == 0)
                    {
                        int random = rnd.Next(0, 4);
                        if (möglichkeiten[random] == true)
                        {
                            richtung4 = random + 1;
                        }
                    }
                }

                if (richtung4 == 2 && möglichkeiten[1] == true && Canvas.GetLeft(ghost4) >= 32)
                {
                    if (Canvas.GetTop(ghost4) == 448 && Canvas.GetLeft(ghost4) < 32)
                    {
                        Canvas.SetLeft(ghost4, 832);
                    }
                    else if (Canvas.GetLeft(ghost4) > 32)
                    {
                        Canvas.SetLeft(ghost4, (Canvas.GetLeft(ghost4) - geschwindigkeit));
                    }
                }
                if (richtung4 == 4 && möglichkeiten[3] == true && Canvas.GetLeft(ghost4) <= 840)
                {
                    if (Canvas.GetTop(ghost4) == 448 && Canvas.GetLeft(ghost4) == 832)
                    {
                        Canvas.SetLeft(ghost4, 32);
                    }
                    else if (Canvas.GetLeft(ghost4) < 840)
                    {
                        Canvas.SetLeft(ghost4, (Canvas.GetLeft(ghost4) + geschwindigkeit));
                    }
                }
                if (richtung4 == 1 && möglichkeiten[0] == true && Canvas.GetTop(ghost4) > 0)
                {
                    Canvas.SetTop(ghost4, (Canvas.GetTop(ghost4) - geschwindigkeit));
                }

                if (richtung4 == 3 && möglichkeiten[2] == true && Canvas.GetTop(ghost4) < 976)
                {
                    Canvas.SetTop(ghost4, (Canvas.GetTop(ghost4) + geschwindigkeit));
                }
                #endregion
            }
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

        public int CollisionPacman()
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
            int x = Convert.ToInt32(Canvas.GetLeft(spieler));  // Y-Varaible wird festgelegt
            int y = Convert.ToInt32(Canvas.GetTop(spieler));   // X-Varaible wird festgelegt
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
                if (map[row, collom] == 2)              //punkte konsumieren
                {
                    punkteanzahl++;
                    map[row, collom] = 0;
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