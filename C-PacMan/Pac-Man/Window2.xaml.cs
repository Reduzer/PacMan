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

        //Variable für Leben
        int lebenanzahl = 1;
        //Variable für Punkte
        int punkteanzahl = 0;
        //Variable für die Anzahl der bereits bewegten Felder
        int felderBewegt = 0;

        #endregion

        #region Variablen

        //Wenn die Bewegung möglich ist
        public bool hoch, runter, rechts, links;
        //Wenn die Bewegung nicht möglich ist
        public bool hochnicht, runternicht, rechtsnicht, linksnicht = false;

        //Geschwindigkeits der Geister und des Spielers
        public double geschwindigkeit = 4 * 8;

        //Coordinaten vom Spieler und den Geistern (Jeweils X und Y Coordinate)
        
        //Spieler
        public int xspieler;
        public int yspieler;

        //X Coordinate der Geister
        public int xgeist1;
        public int xgeist2;
        public int xgeist3;
        public int xgeist4;

        //Y Coordinate der Geister
        public int ygeist1;
        public int ygeist2;
        public int ygeist3;
        public int ygeist4;

        //Bool für dafür ob die Map generiert wurde
        public bool mapDone = false;

        /*
        Map in einem Array (2 Dimensional)
        0 = Leer
        1 = Wand
        2 = Punkt
        3 = Geisterspawn
        */
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

        //Richtungen für die Bewegung der Geister
        public int richtung1 = 1;

        public int richtung2 = 1;

        public int richtung3 = 1;

        public int richtung4 = 1;

        //Random für die Bewegung der Geister
        Random rnd = new Random();

        #endregion

        public Window2()
        {
            //Automatisch generiert
            InitializeComponent();
            
            #region Anzeige von Leben und Punkten

            //In dem Label "Leben" wird die Aktuelle Lebensanzahl angezeigt
            Leben.Content = "Leben: " + lebenanzahl;

            //In dem Label "Punke" wird die Aktuelle Punktezahl angezeigt
            Punkte.Content = "Punkte: " + punkteanzahl;

            #endregion


            //Die Methode MapZeichnen wird aufgerufen
            MapZeichnen();

            //Das Spielfeld wird in den Focus gelegt
            Spielfeld.Focus();

        }

        //Die Methode des Aktiven Spieles
        private void aktivesspiel()
        {

            #region Bewegung

            //Bewegung nach Links, wenn Links gedrückt wird und der Spieler noch in der Begrenzung des Spielfeldes ist
            if (links == true && Canvas.GetLeft(spieler) >= 32)
            {
                //Prüfen ob der Spieler von Links nach Rechts (Mitte der Map) springen kann 
                if (Canvas.GetTop(spieler) == 448 && Canvas.GetLeft(spieler) == 32)
                {
                    //Der Spieler wird an eine Position gesetzt
                    Canvas.SetLeft(spieler, 832);
                    //Variable wird erhöht
                    felderBewegt++;
                }

                //Wenn nicht, dann erfolgt eine normale Bewegung
                else if (links == true && linksnicht == false && Canvas.GetLeft(spieler) > 32)
                {
                    //Der Spieler wird an eine Position gesetzt
                    Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) - geschwindigkeit));
                    //Variable wird erhöht
                    felderBewegt++;
                }
            }

            //Bewegung nach Rechts, wenn Links gedrückt wird und der Spieler noch in der Begrenzung des Spielfeldes ist
            if (rechts == true && Canvas.GetLeft(spieler) <= 840)
            {
                //Prüfung ob der Spieler von Rechts nach Links (Mitte der Map) springen kann
                if (Canvas.GetTop(spieler) == 448 && Canvas.GetLeft(spieler) == 832)
                {
                    //Der Spieler wird an eine Position gesetzt
                    Canvas.SetLeft(spieler, 32);
                    //Variable wird erhöht
                    felderBewegt++;
                }
                else if (rechts == true && rechtsnicht == false && Canvas.GetLeft(spieler) < 840)
                {
                    //Normale Bewegung
                    Canvas.SetLeft(spieler, (Canvas.GetLeft(spieler) + geschwindigkeit));
                    //Variable wird erhöht
                    felderBewegt++;
                }
            }

            //Bewegung nach Oben, wenn Links gedrückt wird und der Spieler noch in der Begrenzung des Spielfeldes ist
            if (hoch == true && hochnicht == false && Canvas.GetTop(spieler) > 0)
            {
                //Normale Bewegung
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) - geschwindigkeit));
                //Variable wird erhöht
                felderBewegt++;
            }

            //Bewegung nach Unten, wenn Links gedrückt wird und der Spieler noch in der Begrenzung des Spielfeldes ist
            if (runter == true && runternicht == false && Canvas.GetTop(spieler) < 976)
            {
                //Normale Bewegung
                Canvas.SetTop(spieler, (Canvas.GetTop(spieler) + geschwindigkeit));
                //Variable wird erhöht
                felderBewegt++;
            }
            #endregion

            #region Mögliche nächste Bewegungen

            //Prüfungen für die Nächste Bewegung, ob diese möglich ist
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

            //Aufrufen der Bewegungs Methoden der Geister
            Geist1Bewegen();
            Geist2Bewegen();
            Geist3Bewegen();
            Geist4Bewegen();

            //Aufrufen der Prüfung, ob ein Geist getroffen wurde
            geistgetroffen();

        }

        //Methode welche genutzt wurde um Probleme während der Programmierung zu finden und zu beheben
        //Wird im Richtigen Spiel nicht benötigt
        public void Textausgabe(int zahl)
        {
            //Rectangle wird erstellt
            Rectangle mapTile = new Rectangle();
            
            //Rectangle wird als Child festgelegt
            Waende.Children.Add(mapTile);

            //Rectangle wird dem Grid zugewiesen
            Grid.SetColumn(mapTile, 11);

            //Rectangle wird dem Grid zugewiesen
            Grid.SetRow(mapTile, 15);

            //Rectangle wird mit einer Farbe gerfüllt
            mapTile.Fill = new SolidColorBrush(Color.FromRgb(50, 0, 0));

            //Ein Textblock wird erstellt
            TextBlock num = new TextBlock();

            //Textblock wird als Child festgelegt
            Waende.Children.Add(num);

            //Textblock wird dem Grid zugewiesen
            Grid.SetColumn(num, 11);

            //Textblock wird dem Grid zugewiesen
            Grid.SetRow(num, 15);

            //Ein Text wird ausgegeben
            num.Text = Convert.ToString(zahl);

            //Die Schrift Farbe wird verändert
            num.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        } 

        //Methode zum "Generieren" der Map
        public void MapZeichnen()
        {
            //Das Map Array wird auf dem Canvas ausgegeben und mit dem im Array Bestimmten Zahlen befüllt
            if (mapDone == false)
            {   //For Schleife, welche so lange läuft, bis alle Spalten generiert wurden
                for (int i = 0; i < 28; i++)
                {   //For Schleife, welche so lange läuft, bis alle Zeilen generiert wurden
                    for (int j = 0; j < 31; j++)
                    {
                        //Erstellung der Rectangles, welche gefüllt werden
                        Rectangle mapTile = new Rectangle();
                        //Switch-Case für das "Generieren der Map"
                        switch (map[j, i])
                        {
                            //Wenn im Array eine Null Steht
                            case 0:
                                Waende.Children.Add(mapTile);
                                //Rectangle wird dem dem Grid zugeordnet und mit einer Farbe gefüllt
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                //Das Rectangle wird mit einer Farbe gefüllt
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                                break;
                            //Wenn im Array eine Eins steht
                            case 1:
                                Waende.Children.Add(mapTile);
                                //Rectangle wird dem dem Grid zugeordnet und mit einer Farbe gefüllt
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                //Das Rectangle wird mit einer Farbe gefüllt
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 70));
                                //Rand wird hinzugefügt
                                mapTile.StrokeThickness = 12;
                                mapTile.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 100));
                                break;
                            case 2:
                                Waende.Children.Add(mapTile);
                                //Rectangle wird dem dem Grid zugeordnet und mit einer Farbe gefüllt
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                //Das Rectangle wird mit einer Farbe gefüllt
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 50));
                                //Rand wird hinzugefügt
                                mapTile.StrokeThickness = 14;
                                mapTile.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                                break;
                            case 3:
                                Waende.Children.Add(mapTile);
                                //Rectangle wird dem dem Grid zugeordnet und mit einer Farbe gefüllt
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(50, 0, 0));
                                break;
                            default:
                                Waende.Children.Add(mapTile);
                                //Rectangle wird dem dem Grid zugeordnet und mit einer Farbe gefüllt
                                Grid.SetColumn(mapTile, i);
                                Grid.SetRow(mapTile, j);
                                //Das Rectangle wird mit einer Farbe gefüllt
                                mapTile.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                                break;
                        }
                        //Variable für das Erstellen der Map wird auf fertig gesetzt
                        mapDone = true;
                    }
                }
            }
        }

        //Methode zum Bewegen und drehen von Pac-Man
        private void drehung(Object sender, KeyEventArgs e)
        {

            #region Bewegung Selbst
            //Prüfung ob die Pfeil-Taste Links gedrückt wurde
            if (e.Key == Key.Left)
            {
                //Nicht benötigte Bewegungs Variablen werden auf False gesetzt
                hoch = false;
                runter = false;
                rechts = false;

                //Benötigte Bewegungs Variable wird auf True gesetzt 
                links = true;

                //PacMan wird gedreht
                spieler.RenderTransform = new RotateTransform(0);
                //Sprung zur aktivenspiel Methode
                aktivesspiel();

            }

            //Prüfung ob die Pfeil-Taste Rechts gedrückt wurde
            if (e.Key == Key.Right)
            {
                //Nicht benötigte Bewegungs Variablen werden auf False gesetzt
                hoch = false;
                runter = false;
                links = false;

                //Benötigte Bewegungs Variable wird auf True gesetzt
                rechts = true;

                //PacMan wird gedreht
                spieler.RenderTransform = new RotateTransform(180);
                //Sprung zur aktivenspiel Methode
                aktivesspiel();

            }

            //Prüfung ob die Pfeil-Taste Hoch gedrückt wurde
            if (e.Key == Key.Up)
            {
                //Nicht benötigte Bewegungs Variablen werden auf False gesetzt
                rechts = false;
                runter = false;
                links = false;

                //Benötigte Bewegungs Variable wird auf True gesetzt
                hoch = true;

                //PacMan wird gedreht
                spieler.RenderTransform = new RotateTransform(90);
                //Sprung zur aktivenspiel Methode
                aktivesspiel();

            }

            //Prüfung ob die Pfeil-Taste Runter gedrückt wurde
            if (e.Key == Key.Down)
            {
                //Nicht benötigte Bewegungs Variablen werden auf False gesetzt
                hoch = false;
                rechts = false;
                links = false;

                //Benötigte Bewegungs Variable wird auf True gesetzt
                runter = true;

                //PacMan wird gedreht
                spieler.RenderTransform = new RotateTransform(270);
                //Sprung zur aktivenspiel Methode
                aktivesspiel();

            }

            #endregion

        }

        //Methode für die Bewegung des ersten Geistes
        public void Geist1Bewegen()
        {
            
            //Prüfung ob die Anzahl der Bewegten Felder gleich 49 ist
            if (felderBewegt == 49)
            {
                //Der Geist wird auf die Position gesetzt
                Canvas.SetTop(ghost1, 352);
                Canvas.SetLeft(ghost1, 416);
            }

            //Prüfung ob die Anzahl der Bewegten Felder größer 50 ist
            if (felderBewegt > 50)                          
            {
                //Wenn der Spieler mehr als 50 Felder gelaufen ist
                bool[] möglichkeiten = new bool[4];
                //Generierung von vier verschiedenen Möglichkeiten der Bewegung
                for(int i = 0; i < 4; i++)
                {
                    möglichkeiten[i] = true;
                }

                //Spalte erstellen
                int collom = 0;
                //Zeile erstellen
                int row = 0;
                //Abfragen der Position des Geistes von der linken oberen Ecke aus gesehen
                int x = Convert.ToInt32(Canvas.GetLeft(ghost1));
                int y = Convert.ToInt32(Canvas.GetTop(ghost1));

                #region Geist Wanderkennung
                // Spalte berechnen
                collom = (x - (x % 32)) / 32;
                //Zeile Berechnen
                row = (y - (y % 32)) / 32;
                //Wenn die Position des Geistes 
                if (x % 32 == 0 && y % 32 == 0)
                {   
                    //Prüfung ob auf einer/mehrerer Seite/Seiten des Geistes eine Wand ist
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

                //Ungefär alle 7 Schritte, die Richtung ändern
                if (rnd.Next(0, 5) == 0 || möglichkeiten[richtung1 - 1] == false)                        
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

                //Wenn in die Richtung ohne Probleme, einer Wand Gelaufen werden kann und das Spielfeld nicht verlassen wird bewegt sich der Geist
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

        //Methode für die Bewegung des zweiten Geistes
        public void Geist2Bewegen()
        {
            //Prüfung ob die Anzahl der Bewegten Felder gleich 99 ist
            if (felderBewegt == 99)
            {
                //Der Geist wird auf die Position gesetzt
                Canvas.SetTop(ghost2, 352);
                Canvas.SetLeft(ghost2, 416);
            }
            //Prüfung ob die Anzahl der Bewegten Felder größer 100 ist
            if (felderBewegt > 100)                         
            {
                //Wenn der Spieler mehr als 50 Felder gelaufen ist
                bool[] möglichkeiten = new bool[4];
                //Generierung von vier verschiedenen Möglichkeiten der Bewegung
                for (int i = 0; i < 4; i++)
                {
                    möglichkeiten[i] = true;
                }
                //Spalte erstellen
                int collom = 0;
                //Zeile erstellen
                int row = 0;
                //Abfragen der Position des Geistes von der linken oberen Ecke aus gesehen
                int x = Convert.ToInt32(Canvas.GetLeft(ghost2));
                int y = Convert.ToInt32(Canvas.GetTop(ghost2));

                #region Geist Wanderkennung
                // Spalte berechnen
                collom = (x - (x % 32)) / 32;
                //Zeile Berechnen
                row = (y - (y % 32)) / 32;
                //Wenn die Position des Geistes 
                if (x % 32 == 0 && y % 32 == 0)
                {
                    //Prüfung ob auf einer/mehrerer Seite/Seiten des Geistes eine Wand ist
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

                //Ungefär alle 7 Schritte, die Richtung ändern
                if (rnd.Next(0, 6) == 0 || möglichkeiten[richtung2 - 1] == false)                        
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

                //Wenn in die Richtung ohne Probleme einer Wand Gelaufen werden kann und das Spielfeld nicht verlassen wird bewegt sich der Geist
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

        //Methode für die Bewegung des dritten Geistes
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

        //Methode für die Bewegung des vierten Geistes
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

        //Methode für die Bewegung von Pac-Man
        public int CollisionPacman()
        {
            #region Collision Test

            #region Variablen und Berechnungen

            int temp1 = 0;                                      //decoden nach bit logic: bit 0 = Kollision oben
            int temp2 = 0;                                      //bit 1 = Kollision links
            int temp4 = 0;                                      //bit 2 = Kollision unten
            int temp8 = 0;                                      //bit 3 = Kollision rechts
            int sum = 0;                                        //Summe erstellen
            int collom = 0;                                     //Spalte erstellen
            int row = 0;                                        //Zeile erstellen
            int x = Convert.ToInt32(Canvas.GetLeft(spieler));   //Y-Varaible wird festgelegt
            int y = Convert.ToInt32(Canvas.GetTop(spieler));    //X-Varaible wird festgelegt
            collom = (x - (x % 32)) / 32;                       //Spalte berechnen
            row = (y - (y % 32)) / 32;                          //Zeile berechnen
            #endregion

            #region Prüfung
            //Prüfung, wo sich Pac-Man auf der Map befindet
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

                //Punkte aufsammeln
                if (map[row, collom] == 2)              
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
            //Wurde während der Programmierung für die Fehlersuche genutzt
            sum = (temp1 + temp2 + temp4 + temp8);
            return sum;
            #endregion

            #endregion
        }

        //Methode für die Prüfung ob ein Geist getroffen wurde
        public void geistgetroffen()
        {
            #region Prüfung der Psotion zu Begin
            double distanztoplayerred;
            int x;
            int y;

            //Spieler Coordinaten
            xspieler = Convert.ToInt32(Canvas.GetLeft(spieler));
            yspieler = Convert.ToInt32(Canvas.GetTop(spieler));

            //Geist 1 Coordinaten
            xgeist1 = Convert.ToInt32(Canvas.GetLeft(ghost1));
            ygeist1 = Convert.ToInt32(Canvas.GetTop(ghost1));

            //Geist 2 Coordinaten
            xgeist2 = Convert.ToInt32(Canvas.GetLeft(ghost2));
            ygeist2 = Convert.ToInt32(Canvas.GetTop(ghost2));

            //Geist 3 Coordinaten
            xgeist3 = Convert.ToInt32(Canvas.GetLeft(ghost3));
            ygeist3 = Convert.ToInt32(Canvas.GetTop(ghost3));

            //Geist 4 Coordinaten
            xgeist4 = Convert.ToInt32(Canvas.GetLeft(ghost4));
            ygeist4 = Convert.ToInt32(Canvas.GetTop(ghost4));
            #endregion

            #region Distanzberechen

            //Prüfung ob der Spieler bereits einen Geist berührt 
            if (xspieler > xgeist1)
            {
                x = Convert.ToInt32(Canvas.GetLeft(spieler) - Canvas.GetLeft(ghost1));

                if (yspieler > ygeist1)
                {
                    y = Convert.ToInt32(Canvas.GetTop(spieler) - Canvas.GetTop(ghost1));
                }
                else
                {
                    y = Convert.ToInt32(Canvas.GetTop(ghost1) - Canvas.GetTop(spieler));
                }

            }

            else
            {
                x = Convert.ToInt32(Canvas.GetLeft(ghost1) - Canvas.GetLeft(spieler));

                if (yspieler > ygeist1)
                {
                    y = Convert.ToInt32(Canvas.GetTop(spieler) - Canvas.GetTop(ghost1));
                }
                else
                {
                    y = Convert.ToInt32(Canvas.GetTop(ghost1) - Canvas.GetTop(spieler));
                }
            }

            distanztoplayerred = (x ^ 2 + y ^ 2);

            #endregion

            #region Distanzberechnung

            //Prüfung ob der Spieler einen Geist während des Spieles Berührt

            #region Geist1
            if (xspieler - xgeist1 < 33 && xgeist1 - xspieler < 33)
            {
                if (yspieler - ygeist1 < 33 && ygeist1 - yspieler < 33)
                {
                    gameend();
                }
            }

            #endregion

            #region Geist 2
            if (xspieler - xgeist2 < 33 && xgeist2 - xspieler < 33)
            {
                if (yspieler - ygeist2 < 33 && ygeist2 - yspieler < 33)
                {
                    gameend();
                }
                else
                {

                }
            }
            #endregion

            #region Geist 3
            if (xspieler - xgeist3 < 33 && xgeist3 - xspieler < 33)
            {
                if (yspieler - ygeist3 < 33 && ygeist3 - yspieler < 33)
                {
                    gameend();
                }
                else
                {

                }
            }
            #endregion

            #region Geist 4
            if (xspieler - xgeist4 < 33 && xgeist4 - xspieler < 33)
            {
                if (yspieler - ygeist4 < 33 && ygeist4 - yspieler < 33)
                {
                    gameend();
                }
                else
                {

                }
            }
            #endregion

            #endregion

        }

        //Methode für das Ende des Spiels
        public void gameend()
        {
            //Eine MessageBox wird geöffnet 
            MessageBox.Show("Du hast Leider Verloren. Das Programm wird beendet!", "Verloren");
            //Das Fenster wird geschlossen
            this.Close();
        }
    }
}