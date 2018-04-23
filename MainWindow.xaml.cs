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
        List<Point> buildingPoints = new List<Point>();
        List<DoubleAnimation> da = new List<DoubleAnimation>();
        List<Image> buildings = new List<Image>();
        //List<Cell> player0Cells = new List<Cell>();
        int bordernumber = 0;
        int currentPlayer; //20180422
        int temp = 0;
        int DieResult = 0;
        int propertyId = 0;

        public static double windowWidth = 0;
        public static double windowHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
            //20180422
            LoadPlayers();
            //init player's position from db
            for (int i = 0; i < players.Count(); i++)
            {
                players[i].Position = BLLayer.GetPlayerPositionFromMySQL(players[i].Id);
            }
            //players[0].Position = 0; //20180422
            //20180422
            //first player has first turn
            if (players.Count() != 0)
            {
                lblCurrentPlayer.Content = "Player " + players[0].Name;
                currentPlayer = 0;
            }
            cells[5].HouseCount = 3;
            cells[6].HouseCount = 3;
            cells[12].HouseCount = 3;
            cells[7].HotelCount = 3;
            cells[20].HotelCount = 3;
            cells[34].HotelCount = 3;

            cells[5].Owner = "Antti";
            cells[6].Owner = "Antti";
            cells[12].Owner = "Antti";
        }

        private void LoadPlayers()
        {
            try
            {
                players = BLLayer.GetAllPlayersFromDt();
                cells = BLLayer.GetAllCellsFromDt();
                //init player's hasn't rolled die yet
                for (int i = 0; i < players.Count(); i++)
                {
                    players[i].DieRolled = false;
                }
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
            RecreateCanvas();
        }

        private void RecreateCanvas()
        {
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
                buildingPoints.Clear();
                buildings.Clear();
                PrintGrid();
                CreatePlayerTokens();
                CreateBuildings();
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
                    Canvas.SetLeft(tokens[i], points[players[i].Position * 4 + i].X);
                    Canvas.SetTop(tokens[i], points[players[i].Position * 4 + i].Y);
                    canvasObj.Children.Add(tokens[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("asdf" + ex.Message);
            }
        }

        private void CreateBuildings()
        {
            try
            {
                for (int i = 0, k=0; i < cells.Count; i++)
                {
                    if (cells[i].HouseCount > 0)
                    {
                        for (int j = 0; j < cells[i].HouseCount; j++)
                        {
                            BitmapImage bi = new BitmapImage();
                            string path = "/house.png";
                            bi.BeginInit();
                            bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                            bi.EndInit();

                            buildings.Add( new Image { Width = windowWidth / 100 * 3, Height = windowWidth / 100 * 3, Source = bi });

                            Canvas.SetLeft(buildings[k], buildingPoints[i * 4 + j].X);
                            Canvas.SetTop(buildings[k], buildingPoints[i * 4 + j].Y);

                            canvasObj.Children.Add(buildings[k]);
                            k++;
                        }
                    }

                    else if (cells[i].HotelCount > 0)
                    {
                        for (int j = 0; j < cells[i].HotelCount; j++)
                        {
                            BitmapImage bi = new BitmapImage();
                            string path = "/hotel.png";
                            bi.BeginInit();
                            bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                            bi.EndInit();

                            buildings.Add(new Image { Width = windowWidth / 100 * 3, Height = windowWidth / 100 * 3, Source = bi });

                            Canvas.SetLeft(buildings[k], buildingPoints[i * 4 + j].X);
                            Canvas.SetTop(buildings[k], buildingPoints[i * 4 + j].Y);

                            canvasObj.Children.Add(buildings[k]);
                            k++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Buildings" + ex.Message);
            }
        }

        private void buttonMoveToken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if die rolled, cannot roll again
                if (players[currentPlayer].DieRolled != true)
                {
                    Random rnd = new Random();

                    //20180422 players[0] -> players[currentPlayer]
                    //also token[0] -> token[currentPlayer]
                    DieResult = rnd.Next(2, 12);

                    lblDieResult.Content = "Die Result: " + DieResult.ToString();

                    temp = players[currentPlayer].Position;

                    int maxposition = 35;

                    players[currentPlayer].Position += DieResult;

                    //TokenAnimation();

                    if (players[currentPlayer].Position > 35)
                    {
                        players[currentPlayer].Position -= maxposition;
                    }
                    else if (players[currentPlayer].Position == 36)
                    {
                        players[currentPlayer].Position = 0;
                    }

                    //20180422
                    //shows previous position in ui
                    lblPreviousPosition.Content = lblCell.Content = "Previous Position" + BLLayer.GetPlayerPositionFromMySQL(players[currentPlayer].Id);

                    //20180422
                    //sets player's new position to db
                    BLLayer.SetPlayerPositionToMySQL(players[currentPlayer].Id, players[currentPlayer].Position);

                    //20180422
                    //shows current player's id in ui
                    lblCurrPlrId.Content = "Current Player's Id: " + players[currentPlayer].Id.ToString();

                    //20180422
                    //gets player position from db
                    lblCell.Content = "Current Position" + BLLayer.GetPlayerPositionFromMySQL(players[currentPlayer].Id);


                    //20180422
                    //    shows current player's cash in ui
                    //players[currentPlayer].Cash = BLLayer.GetPlayerCashFromMySQL(players[currentPlayer].Id);
                    //lblCash.Content = "Player's Cash: " + players[currentPlayer].Cash;

                    Storyboard story = new Storyboard();

                    //send to prison animation
                    DoubleAnimation dbCanvasX = new DoubleAnimation();
                    if (players[currentPlayer].Position == 27)
                    {
                        dbCanvasX.From = points[temp * 4].X;
                        dbCanvasX.To = points[36].X;
                        players[currentPlayer].Position = 9;
                        lblNotification.Content = "You were sent to the prison!";
                        //update position to db instead of only to canvas
                        //otherwise when game loaded player would be at "go to jail"-cell
                        BLLayer.SetPlayerPositionToMySQL(players[currentPlayer].Id, players[currentPlayer].Position);
                    }
                    else
                    {
                        dbCanvasX.From = points[temp * 4 + currentPlayer].X;
                        dbCanvasX.To = points[players[currentPlayer].Position * 4 + currentPlayer].X;
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
                        dbCanvasY.From = points[temp * 4 + currentPlayer].Y;
                        dbCanvasY.To = points[players[currentPlayer].Position * 4 + currentPlayer].Y;
                    }

                    story.Children.Add(dbCanvasX);
                    Storyboard.SetTargetName(dbCanvasX, tokens[currentPlayer].Name);
                    Storyboard.SetTargetProperty(dbCanvasX, new PropertyPath(Canvas.LeftProperty));

                    story.Children.Add(dbCanvasY);
                    Storyboard.SetTargetName(dbCanvasX, tokens[currentPlayer].Name);
                    Storyboard.SetTargetProperty(dbCanvasY, new PropertyPath(Canvas.TopProperty));

                    story.Begin(tokens[currentPlayer]);

                    //20180422
                    players[currentPlayer].DieRolled = true;
                }

                //20180422
                else
                {
                //btnDice.IsHitTestVisible = false;
                lblNotification.Content = "You have already rolled the die.";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("storyboard " + ex.Message);
                //throw;
            }
        }

        //private void TokenAnimation()
        //{
        //    Storyboard story = new Storyboard();
        //    //send to prison animation
        //    for (int i = 0, j = -2, k = -1; i < DieResult; i++)
        //    {
        //        da.Add(new DoubleAnimation());
        //        if (players[currentPlayer].Position == 27)
        //        {
        //            da[j + 2].From = points[temp * 4].X;
        //            da[j + 2].To = points[36].X;
        //            players[currentPlayer].Position = 9;
        //            lblNotification.Content = "You were sent to the prison!";
        //            //update position to db instead of only to canvas
        //            //otherwise when game loaded player would be at "go to jail"-cell
        //            BLLayer.SetPlayerPositionToMySQL(players[currentPlayer].Id, players[currentPlayer].Position);
        //        }
        //        else
        //        {
        //            da[j + 2].From = points[(temp + i) * 4].X;
        //            da[j + 2].To = points[(players[currentPlayer].Position + i) * 4].X;
        //            da[j + 2].BeginTime = TimeSpan.FromSeconds(i);
        //        }
        //        da[j + 2].Duration = new Duration(TimeSpan.FromSeconds(1));

        //        da.Add(new DoubleAnimation());
        //        if (players[currentPlayer].Position == 27)
        //        {
        //            da[k + 2].From = points[(temp + i) * 4].Y;
        //            da[k + 2].To = points[36].Y;
        //        }
        //        else
        //        {
        //            da[k + 2].From = points[(temp + i) * 4].Y;
        //            da[k + 2].To = points[(players[currentPlayer].Position + i) * 4].Y;
        //            da[k + 2].BeginTime = TimeSpan.FromSeconds(i);
        //        }

        //        story.Children.Add(da[j + 2]);
        //        Storyboard.SetTargetName(da[j + 2], tokens[currentPlayer].Name);
        //        Storyboard.SetTargetProperty(da[j + 2], new PropertyPath(Canvas.LeftProperty));

        //        story.Children.Add(da[k + 2]);
        //        Storyboard.SetTargetName(da[k + 2], tokens[currentPlayer].Name);
        //        Storyboard.SetTargetProperty(da[k + 2], new PropertyPath(Canvas.TopProperty));

        //        story.Begin(tokens[currentPlayer]);

        //        j += 2;
        //        k += 2;
        //    }
        //}

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

                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.10), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.30), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.50), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.70), Y = y + (windowHeight * 0.16 * 0.05) });
                }
                else if (bordernumber >= 1 && bordernumber <= 8)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.65) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.65) });

                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.10), Y = y + (windowHeight * 0.16 * 0.01) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.30), Y = y + (windowHeight * 0.16 * 0.01) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.50), Y = y + (windowHeight * 0.16 * 0.01) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.70), Y = y + (windowHeight * 0.16 * 0.01) });
                }
                else if (bordernumber >= 10 && bordernumber <= 17)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.05), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.25), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.45), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.65), Y = y + (windowHeight * 0.16 * 0.55) });

                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.05), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.25), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.45), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.65), Y = y + (windowHeight * 0.16 * 0.05) });
                }
                else if (bordernumber >= 19 && bordernumber <= 26)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.30) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.085 * 0.65) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.085 * 0.65) });

                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.10), Y = y + (windowHeight * 0.16 * 0.01) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.30), Y = y + (windowHeight * 0.16 * 0.01) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.50), Y = y + (windowHeight * 0.16 * 0.01) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.16 * 0.70), Y = y + (windowHeight * 0.16 * 0.01) });
                }
                else if (bordernumber >= 28 && bordernumber <= 35)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.05), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.25), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.45), Y = y + (windowHeight * 0.16 * 0.55) });
                    points.Add(new Point { X = x + (windowWidth * 0.085 * 0.65), Y = y + (windowHeight * 0.16 * 0.55) });

                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.05), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.25), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.45), Y = y + (windowHeight * 0.16 * 0.05) });
                    buildingPoints.Add(new Point { X = x + (windowWidth * 0.085 * 0.65), Y = y + (windowHeight * 0.16 * 0.05) });
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
                    StackPanel stack = new StackPanel();
                    stack.Orientation = Orientation.Horizontal;

                    Button btnDice = new Button();
                    btnDice.Height = windowHeight * 0.05;
                    btnDice.Width = windowWidth * 0.1;
                    btnDice.Content = "Roll Dice";
                    btnDice.Background = Brushes.Coral;
                    btnDice.Click += new RoutedEventHandler(buttonMoveToken_Click);

                    Button btnBuyBuildings = new Button();
                    btnBuyBuildings.Height = windowHeight * 0.05;
                    btnBuyBuildings.Width = windowWidth * 0.1;
                    btnBuyBuildings.Content = "Construct Buildings";
                    btnBuyBuildings.Background = Brushes.Coral;
                    btnBuyBuildings.Click += new RoutedEventHandler(btnBuyBuildings_Click);

                    Button btnEndturn = new Button();
                    btnEndturn.Height = windowHeight * 0.05;
                    btnEndturn.Width = windowWidth * 0.1;
                    btnEndturn.Content = "End turn";
                    btnEndturn.Background = Brushes.Coral;
                    btnEndturn.Click += new RoutedEventHandler(btnNextPlayer_Click);


                    stack.Children.Add(btnDice);
                    stack.Children.Add(btnBuyBuildings);
                    stack.Children.Add(btnEndturn);

                    Canvas.SetLeft(stack, windowWidth * 0.5);
                    Canvas.SetTop(stack, windowHeight * 0.2);
                    canvasObj.Children.Add(stack);
                }
            }
        }

        //20180422
        private void btnNextPlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLMethod.NextTurn(ref currentPlayer, ref players);
                //shows player's name in ui
                lblCurrentPlayer.Content = "Player " + players[currentPlayer].Name;
                //reset notifications
                lblNotification.Content = "Notifications";
                //btnDice.IsHitTestVisible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuyBuildings_Click(object sender, RoutedEventArgs e)
        {
            RecreateCanvas();

            try
            {
                Grid g = new Grid();
                StackPanel stack = new StackPanel();
                int i = 0;

                foreach (Cell c in cells)
                {
                    if (players[currentPlayer].Name == c.Owner)
                    {
                        Button btn = new Button();
                        btn.Height = 20;
                        btn.Width = 200;
                        btn.Content = c.Name;
                        btn.Name = c.Name.ToString();
                        btn.Background = Brushes.White;
                        btn.Click += new RoutedEventHandler(btnBuyForCell_Click);
                        stack.Children.Add(btn);

                        i++;
                    }
                }

                TextBlock t = new TextBlock();
                if (i == 0)
                {
                    t.Text = "You do not own any properties";
                }
                else
                {
                    t.Text = "Select property to build on";
                }

                t.Height = 20;
                t.Width = 200;
                stack.Children.Add(t);

                g.Children.Add(stack);
                Canvas.SetLeft(g, 250);
                Canvas.SetTop(g, 250);
                canvasObj.Children.Add(g);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuyForCell_Click(object sender, RoutedEventArgs e)
        {
            RecreateCanvas();

            try
            {
                List<Button> buttons = new List<Button>();
                Grid g = new Grid();
                StackPanel stack = new StackPanel();

                Button btn = (Button)sender;

                propertyId = 0;

                TextBlock t = new TextBlock();

                foreach(Cell c in cells)
                {
                    if (c.Name == btn.Content.ToString())
                    {
                        propertyId = c.Id - 1;
                    }
                }

                if (cells[propertyId].HouseCount == 4)
                {
                    t.Text = "Property has 4 houses";
                    stack.Children.Add(t);
                    buttons.Add(new Button());
                    buttons[0].Height = 20;
                    buttons[0].Width = 200;
                    buttons[0].Content = "Buy a Hotel for 200$";
                    buttons[0].Background = Brushes.White;
                    buttons[0].Click += new RoutedEventHandler(btnBuyHotel_Click);
                    stack.Children.Add(buttons[0]);
                }
                else if (cells[propertyId].HotelCount > 0)
                {
                    t.Text = "Property has " + cells[propertyId].HotelCount + " hotels.";
                    stack.Children.Add(t);
                    buttons.Add(new Button());
                    buttons[0].Height = 20;
                    buttons[0].Width = 200;
                    buttons[0].Content = "Buy a Hotel for 200$";
                    buttons[0].Background = Brushes.White;
                    buttons[0].Click += new RoutedEventHandler(btnBuyHotel_Click);

                    stack.Children.Add(buttons[0]);
                }
                else if (cells[propertyId].HotelCount == 4)
                {
                    t.Text = "Property has maximum amount of hotels";
                    stack.Children.Add(t);
                }
                else
                {
                    t.Text = "Property has " + cells[propertyId].HouseCount + " houses";
                    stack.Children.Add(t);
                    buttons.Add(new Button());
                    buttons[0].Height = 20;
                    buttons[0].Width = 200;
                    buttons[0].Content = "Buy a House for 100$";
                    buttons[0].Background = Brushes.White;
                    buttons[0].Click += new RoutedEventHandler(btnBuyHouse_Click);
                    stack.Children.Add(buttons[0]);
                }

                t.Height = 20;
                t.Width = 200;

                g.Children.Add(stack);
                Canvas.SetZIndex(g, 1000);
                Canvas.SetLeft(g, 250);
                Canvas.SetTop(g, 250);
                canvasObj.Children.Add(g);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuyHotel_Click(object sender, RoutedEventArgs e)
        {
            cells[propertyId].HouseCount = 0;
            if (cells[propertyId].HotelCount < 4)
            {
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.
                string path = "/hotel.png";
                bi.BeginInit();
                bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bi.EndInit();
                // add player tokens into list
                Image image = new Image { Width = windowWidth / 100 * 3, Height = windowWidth / 100 * 3, Source = bi };
                Canvas.SetLeft(image, buildingPoints[propertyId * 4 + cells[propertyId].HotelCount].X);
                Canvas.SetTop(image, buildingPoints[propertyId * 4 + cells[propertyId].HotelCount].Y);
                canvasObj.Children.Add(image);

                cells[propertyId].HotelCount++;
            }
            else
            {
                lblNotification.Content = "Maximum number of houses on property.";
            }
        }

        private void btnBuyHouse_Click(object sender, RoutedEventArgs e)
        {
            if (cells[propertyId].HouseCount < 4)
            {
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.
                string path = "/house.png";
                bi.BeginInit();
                bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                bi.EndInit();
                // add player tokens into list
                Image image = new Image { Width = windowWidth / 100 * 3, Height = windowWidth / 100 * 3, Source = bi };
                Canvas.SetLeft(image, buildingPoints[propertyId * 4 + cells[propertyId].HouseCount].X);
                Canvas.SetTop(image, buildingPoints[propertyId * 4 + cells[propertyId].HouseCount].Y);
                canvasObj.Children.Add(image);

                cells[propertyId].HouseCount++;
            }
            else
            {
                lblNotification.Content = "Maximum number of houses on property.";
            }
        }
    }
}