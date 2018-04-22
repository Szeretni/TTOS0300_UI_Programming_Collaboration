using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows.Media.Animation;
using ExtensionMethods;

namespace TTOS0300_UI_Programming_Collaboration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Grid> grids = new List<Grid>();
        List<Player> players = new List<Player>();
        List<Cell> cells = new List<Cell>();
        List<Border> borders = new List<Border>();
        List<Image> tokens = new List<Image>();
        List<Point> points = new List<Point>();
        int bordernumber = 0;
        int currentPlayer; //20180422

        public static double windowWidth = 0;
        public static double windowHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
            //20180422
            LoadPlayers();
            for (int i = 0; i < players.Count(); i++)
            {
                players[i].Position = BLLayer.GetPlayerPositionFromMySQL(players[i].Id);
            }
            //players[0].Position = 0; //20180422
            //20180422
            if (players.Count() != 0)
            {
                lblCurrentPlayer.Content = "Player " + players[0].Name;
                currentPlayer = 0;
            }
        }

        private void LoadPlayers()
        {
            try
            {
                players = BLLayer.GetAllPlayersFromDt();
                cells = BLLayer.GetAllCellsFromDt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get our game window area
            //FrameworkElement client = this.Content as FrameworkElement;
            //windowWidth = (double)client.ActualWidth;
            //windowHeight = (double)client.ActualHeight;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // get our game window area
            FrameworkElement client = this.Content as FrameworkElement;
            windowWidth = (double)client.ActualWidth;
            windowHeight = (double)client.ActualHeight;
            try
            {
                canvasObj.Children.Clear();
                bordernumber = 0;

                for (int i = 0; i < tokens.Count; i++)
                {
                    canvasObj.UnregisterName(tokens[i].Name);
                }

                tokens.Clear();
                points.Clear();
                PrintGrid();
                CreatePlayerTokens();
            }

            catch (Exception ex)
            {
                MessageBox.Show("1" + ex.Message);
            }

        }

        private void CreatePlayerTokens()
        {
            try
            {
                for (int i = 0; i < players.Count; i++)
                {
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.
                    int tokennumber = i + 1;
                    string path = "/token" + tokennumber + ".png";
                    bi.BeginInit();
                    bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    string name = "asdf" + i;
                    // add player tokens into list
                    tokens.Add(new Image { Width = windowWidth / 100 * 3, Height = windowWidth / 100 * 3, Source = bi});
                    tokens[i].Name = name;
                    canvasObj.RegisterName(tokens[i].Name, tokens[i]);
                    Canvas.SetLeft(tokens[i], points[i].X);
                    Canvas.SetTop(tokens[i], points[i].Y);
                    canvasObj.Children.Add(tokens[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("asdf" + ex.Message);
            }
        }

        private void buttonMoveToken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random rnd = new Random();

                //20180422
                int DieResult = rnd.Next(2, 12);

                lblDieResult.Content = "Die Result: " + DieResult.ToString();

                int temp = players[currentPlayer].Position;
                int maxposition = 35;

                players[currentPlayer].Position += DieResult;

                if (players[currentPlayer].Position > 35)
                {
                    players[currentPlayer].Position -= maxposition;
                }
                else if (players[currentPlayer].Position == 36)
                {
                    players[currentPlayer].Position = 0;
                }

                //20180422
                lblPreviousPosition.Content = lblCell.Content = "Previous Position" + BLLayer.GetPlayerPositionFromMySQL(players[currentPlayer].Id);

                //20180422
                //sets player's new position to db
                BLLayer.SetPlayerPositionToMySQL(players[currentPlayer].Id, players[currentPlayer].Position);

                lblCurrPlrId.Content = "Current Player's Id: " + players[currentPlayer].Id.ToString();

                //20180422
                //gets player position from db
                lblCell.Content = "Current Position" + BLLayer.GetPlayerPositionFromMySQL(players[currentPlayer].Id);

                //20180422
                players[currentPlayer].Cash = BLLayer.GetPlayerCashFromMySQL(players[currentPlayer].Id);
                lblCash.Content = "Player's Cash: " + players[currentPlayer].Cash;

                Storyboard story = new Storyboard();

                DoubleAnimation dbCanvasX = new DoubleAnimation();
                if (players[currentPlayer].Position == 27)
                {
                    dbCanvasX.From = points[temp * 4].X;
                    dbCanvasX.To = points[36].X;
                    players[currentPlayer].Position = 9;
                }
                else
                {
                    dbCanvasX.From = points[temp * 4].X;
                    dbCanvasX.To = points[players[currentPlayer].Position * 4].X;
                }
                dbCanvasX.Duration = new Duration(TimeSpan.FromSeconds(2));

                DoubleAnimation dbCanvasY = new DoubleAnimation();
                if (players[currentPlayer].Position == 27)
                {
                    dbCanvasY.From = points[temp * 4].Y;
                    dbCanvasY.To = points[36].Y;
                }
                else
                {
                    dbCanvasY.From = points[temp * 4].Y;
                    dbCanvasY.To = points[players[currentPlayer].Position * 4].Y;
                }

                story.Children.Add(dbCanvasX);
                Storyboard.SetTargetName(dbCanvasX, tokens[currentPlayer].Name);
                Storyboard.SetTargetProperty(dbCanvasX, new PropertyPath(Canvas.LeftProperty));

                story.Children.Add(dbCanvasY);
                Storyboard.SetTargetName(dbCanvasX, tokens[currentPlayer].Name);
                Storyboard.SetTargetProperty(dbCanvasY, new PropertyPath(Canvas.TopProperty));

                story.Begin(tokens[currentPlayer]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("storyboard " + ex.Message);
                //throw;
            }
        }

        private void PlayerTest(Color c, int pos)
        {
            Rectangle r = new Rectangle();

            r.Fill = new SolidColorBrush(c);

            r.Width = 15;
            r.Height = 15;
            Grid.SetRow(r, 3);
            Grid.SetColumn(r, 0);
        }

        private void AddGrid(double x, double y, string side)
        {
            try
            {
                Grid g = new Grid();
                g.Background = Brushes.AliceBlue;
                borders.Add(new Border());
                borders[bordernumber].BorderBrush = Brushes.Black;
                if (bordernumber != 0 && bordernumber < 9)
                {
                    borders[bordernumber].BorderThickness = new Thickness(0,2,2,2);
                }
                else if (bordernumber != 9 && bordernumber > 9 && bordernumber < 19)
                {
                    borders[bordernumber].BorderThickness = new Thickness(2, 0, 2, 2);
                }
                else if (bordernumber != 18 && bordernumber > 18 && bordernumber < 28)
                {
                    borders[bordernumber].BorderThickness = new Thickness(2, 2, 0, 2);
                }
                else if (bordernumber != 27 && bordernumber > 27 && bordernumber < 36)
                {
                    borders[bordernumber].BorderThickness = new Thickness(2, 2, 2, 0);
                }

                //grid dimension
                switch (side)
                {
                    default:
                        g.Height = windowHeight / 100 * 16;
                        g.Width = windowWidth / 100 * 8.5;
                        break;
                    case "sides":
                        g.Height = windowHeight / 100 * 8.5;
                        g.Width = windowWidth / 100 * 16;
                        break;
                    case "top":
                        g.Height = windowHeight / 100 * 16;
                        g.Width = windowWidth / 100 * 8.5;
                        break;
                    case "corner":
                        g.Height = windowHeight / 100 * 16;
                        g.Width = windowWidth / 100 * 16;
                        break;
                }

                //g.ShowGridLines = true;

                // Define the Columns
                ColumnDefinition colDef1 = new ColumnDefinition();
                g.ColumnDefinitions.Add(colDef1);

                // Define the Rows
                RowDefinition rowDef1 = new RowDefinition();
                RowDefinition rowDef2 = new RowDefinition();
                RowDefinition rowDef3 = new RowDefinition();
                RowDefinition rowDef4 = new RowDefinition();
                g.RowDefinitions.Add(rowDef1);
                g.RowDefinitions.Add(rowDef2);
                g.RowDefinitions.Add(rowDef3);
                g.RowDefinitions.Add(rowDef4);

                Rectangle r = new Rectangle();
                
                switch (cells[bordernumber].SerieId)
                {
                    case 1:
                        {
                            r.Fill = Brushes.Brown;
                            break;
                        }
                    case 3:
                        {
                            r.Fill = Brushes.SkyBlue;
                            break;
                        }
                    case 4:
                        {
                            r.Fill = Brushes.Pink;
                            break;
                        }
                    case 5:
                        {
                            r.Fill = Brushes.Orange;
                            break;
                        }
                    case 6:
                        {
                            r.Fill = Brushes.Red;
                            break;
                        }
                    case 7:
                        {
                            r.Fill = Brushes.Yellow;
                            break;
                        }
                    case 8:
                        {
                            r.Fill = Brushes.DarkGreen;
                            break;
                        }
                    case 9:
                        {
                            r.Fill = Brushes.DarkBlue;
                            break;
                        }
                }
                
                Grid.SetRow(r, 0);
                Grid.SetColumn(r, 0);

                TextBlock txt1 = new TextBlock();
                txt1.Text = cells[bordernumber].Name;
                txt1.FontSize = 12;
                txt1.TextAlignment = TextAlignment.Center;
                txt1.TextWrapping = TextWrapping.WrapWithOverflow;
                Grid.SetRow(txt1, 1);
                Grid.SetColumn(txt1, 0);

                StackPanel stack = new StackPanel();
                Grid.SetRow(stack, 2);
                Grid.SetColumn(stack, 0);
                stack.Orientation = Orientation.Horizontal;
                
                TextBlock txt2 = new TextBlock();
                if (cells[bordernumber].Price > 0)
                {
                    txt2.Text = cells[bordernumber].Price.ToString() + "$";
                }

                txt2.FontSize = 12;
                txt2.TextAlignment = TextAlignment.Center;
                Grid.SetRow(txt2, 3);
                Grid.SetColumn(txt2, 0);

                g.Children.Add(stack);
                g.Children.Add(r);
                g.Children.Add(txt1);
                g.Children.Add(txt2);

                borders[bordernumber].Child = g;

                //set coordinates for player token spots, needs some adjustments
                if (bordernumber == 0 || bordernumber == 9 || bordernumber == 18 || bordernumber == 27)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.16 * 0.7) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.25), Y = y + (windowHeight * 0.16 * 0.7) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.45), Y = y + (windowHeight * 0.16 * 0.7) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.16 * 0.7) });

                }
                else if (bordernumber >= 1 && bordernumber <= 8)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.65) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.65) });
                }
                else if (bordernumber >= 10 && bordernumber <= 17)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.05), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.25), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.45), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.65), Y = y + (windowHeight * 0.16 * 0.55) });
                }
                else if (bordernumber >= 19 && bordernumber <= 26)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.65) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.65) });
                }
                else if (bordernumber >= 28 && bordernumber <= 35)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.05), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.25), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.45), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.65), Y = y + (windowHeight * 0.16 * 0.55) });
                }

                Canvas.SetLeft(borders[bordernumber], x);
                Canvas.SetTop(borders[bordernumber], y);

                canvasObj.Children.Add(borders[bordernumber]);

                bordernumber++;
            }

            catch (Exception ex)
            {
                //throw;
                MessageBox.Show("2 " + ex.Message);
            }
        }

        private void PrintGrid()
        {
            int j = 1; //used to determine coordinates for printing
            double htop = windowHeight / 100 * 16;
            double wtop = windowWidth / 100 * 8.5;
            double hsides = windowHeight / 100 * 8.5;
            double wsides = windowWidth / 100 * 16;
            for (int i = 0; i < 37; i++)
            {
                if (i == 0) // bottom left corner, left x - top y
                {
                    AddGrid(0, windowHeight - htop, "corner");
                }

                else if (i < 9) // left cells width 15% height 8,75% starting from 15% from bottom, left x - top y
                {
                    AddGrid(0, windowHeight - htop - (hsides*j), "sides");
                    j++;
                }
                
                else if (i == 9) // top left corner, left x - top y
                {
                    AddGrid(0, 0, "corner");
                    j = 8;
                }
                
                else if (i < 18) // top cells, left x - top y
                {
                    AddGrid(windowWidth - wsides - (wtop * j), 0, "top");
                    j--;
                }
                
                else if (i == 18) // top right corner, left x - top y
                {
                    AddGrid(windowWidth - wsides, 0, "corner");
                    j = 8;
                }
                
                else if (i < 27) // right cells, left x - top y
                {
                    AddGrid(windowWidth - wsides, windowHeight - htop - (hsides * j), "sides");
                    j--;
                }
                
                else if (i == 27) // bottom right corner, left x - top y
                {
                    AddGrid(windowWidth - wsides, windowHeight - htop, "corner");
                    j = 1;
                }

                else if (i < 36)// bottom cells, left x - top y
                {
                    AddGrid(windowWidth - wsides - (wtop*j), windowHeight - htop, "top");
                    j++;
                }
                else if (i == 36)
                {
                    buttonMoveToken.Height = windowWidth * 0.1;
                    buttonMoveToken.Width = windowHeight * 0.1;
                    buttonMoveToken.Content = "Roll Dice";
                    buttonMoveToken.Background = Brushes.BlanchedAlmond;
                    /*
                    Canvas.SetLeft(buttonMoveToken, windowWidth - windowWidth * 0.5);
                    Canvas.SetTop(buttonMoveToken, windowHeight - windowHeight * 0.5);
                    canvasObj.Children.Add(buttonMoveToken);
                    */
                }
            }
        }

        //20180422
        private void btnNextPlayer_Click(object sender, RoutedEventArgs e)
        {
            //in case of 0 players
            try
            {
                currentPlayer++;
                if (currentPlayer == players.Count())
                {
                    currentPlayer = 0;
                }
                lblCurrentPlayer.Content = "Player " + players[currentPlayer].Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}