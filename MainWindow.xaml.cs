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
        List<Cell> cellserie = new List<Cell>();
        int[] rents =
        {
            10  ,30     ,90     ,160    ,250,
            20  ,60     ,180    ,320    ,450,
            30  ,90     ,270    ,400    ,550,
            30  ,90     ,270    ,400    ,550,
            40  ,100    ,300    ,450    ,600,
            50  ,150    ,450    ,625    ,750,
            50  ,150    ,450    ,625    ,750,
            60  ,180    ,500    ,700    ,900,
            70  ,200    ,550    ,750    ,950,
            70  ,200    ,550    ,750    ,950,
            80  ,220    ,600    ,800    ,1000,
            90  ,250    ,700    ,875    ,1050,
            90  ,250    ,700    ,875    ,1050,
            100 ,300    ,750    ,925    ,1100,
            110 ,330    ,800    ,975    ,1150,
            110 ,330    ,800    ,975    ,1150,
            120 ,360    ,850    ,1025   ,1200,
            130 ,390    ,900    ,1100   ,1275,
            130 ,390    ,900    ,1100   ,1275,
            150 ,450    ,1000   ,1200   ,1400,
            175 ,500    ,1100   ,1300   ,1500,
            200 ,600    ,1400   ,1700   ,2000,
        };

        int[] buildingcosts = { 50, 50, 100, 100, 150, 150, 200, 200 };
        
        //List<Cell> player0Cells = new List<Cell>();
        int bordernumber = 0;
        int currentPlayer; //20180422;
        int DieResult = 0;
        int propertyId = 0;
        int serieId = 0;
        bool ownsAll = false;


        public static double windowWidth = 0;
        public static double windowHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
            //20180422
            LoadPlayers();

            //20180422
            //first player has first turn
            if (players.Count() != 0)
            {
                currentPlayer = BLLayer.GetCurrentPlayerIdFromMySQL();
                lblCurrentPlayer.Content = "Player " + players[currentPlayer].Name;
            }

            Player p = new Player { Name = "Kalle" };

            Binding b = new Binding
            {
                Source = p,

                Path = new PropertyPath("Name")
            };
            txtTest.SetBinding(TextBlock.TextProperty, b);

            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = players;

            //BindingExpression be = dataGrid1.GetBindingExpression(DataGrid.DataContextProperty);
            //be.UpdateSource();
        }

        private void LoadPlayers()
        {
            try
            {
                players = BLLayer.GetAllPlayersFromDt();
                cells = BLLayer.GetAllCellsFromDt();
                //init player's die rolled status
                for (int i = 0; i < players.Count(); i++)
                {
                    players[i].DieRolled = BLLayer.GetDieRolledFlagFromMySQL(players[i].Id);
                    players[i].Position = BLLayer.GetPlayerPositionFromMySQL(players[i].Id);
                }



                cells[1].Owner = 5;
                cells[3].Owner = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadPlayers " + ex.Message);
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

        private void CreateHandlersForGrids()
        {
            //20180424
            for (int i = 0; i < bordernumber; i++)
            {
                borders[i].Child.MouseEnter += Child_MouseEnter;
            }
        }

        //20180424
        private void Child_MouseEnter(object sender, MouseEventArgs e)
        {
            var gr = sender as Grid;
            var grch = gr.Children;
            var tb = grch[2] as TextBlock; // this child contains cell's name
            var tbValueName = tb.Text;
            Cell cellCopy = cells.Find(x => x.Name.Contains(tbValueName)); // gain access to Cell properties at hovered cell
            lblNotification.Content = cellCopy.Id;
            List<Cell> tempCellList = new List<Cell>();
            tempCellList.Add(cellCopy);
            dgCellTest.ItemsSource = tempCellList; // temp, just to verify
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
                CreateHandlersForGrids();
                CreatePlayerTokens();
                CreateBuildings();
            }

            catch (Exception ex)
            {
                MessageBox.Show("RecreateCanvas: " + ex.Message);
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
                    tokens.Add(new Image { Width = windowWidth / 100 * 3, Height = windowWidth / 100 * 3, Source = bi });
                    tokens[i].Name = name;
                    canvasObj.RegisterName(tokens[i].Name, tokens[i]);
                    Canvas.SetLeft(tokens[i], points[players[i].Position * 4 + i].X);
                    Canvas.SetTop(tokens[i], points[players[i].Position * 4 + i].Y);
                    canvasObj.Children.Add(tokens[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CreatePlayerTokens: " + ex.Message);
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
                MessageBox.Show("CreateBuildings" + ex.Message);
            }
        }

        private void buttonMoveToken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int temp = currentPlayer;
                //if die rolled, cannot roll again
                if (players[currentPlayer].DieRolled != true)
                {
                    Random rnd = new Random();

                    //20180422 players[0] -> players[currentPlayer]
                    //also token[0] -> token[currentPlayer]
                    DieResult = 1;

                    lblDieResult.Content = "Die Result: " + DieResult.ToString();

                    temp = players[currentPlayer].Position;

                    int maxposition = 35;

                    players[currentPlayer].Position += DieResult;

                    //TokenAnimation();

                    //players moves through start position
                    if (players[currentPlayer].Position > 35)
                    {
                        players[currentPlayer].Cash += 200;
                        players[currentPlayer].Position -= maxposition;
                        BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash += 300);
                        lblNotification.Content = "You collected 200$ in income by passing start";
                    }
                    else if (players[currentPlayer].Position == 36)
                    {
                        players[currentPlayer].Position = 0;
                    }

                    //20180422
                    //shows previous position in ui
                    lblPreviousPosition.Content = "Previous Position: " + BLLayer.GetPlayerPositionFromMySQL(players[currentPlayer].Id);

                    //20180422
                    //sets player's new position to db
                    BLLayer.SetPlayerPositionToMySQL(players[currentPlayer].Id, players[currentPlayer].Position);

                    //20180422
                    //shows current player's id in ui
                    lblCurrPlrId.Content = "Current Player's Id: " + players[currentPlayer].Id.ToString();

                    //20180422
                    //gets player position from db
                    lblCell.Content = "Current Position: " + BLLayer.GetPlayerPositionFromMySQL(players[currentPlayer].Id);

                    //20180422
                    //shows current player's cash in ui
                    lblCash.Content = "Cash: " + players[currentPlayer].Cash;

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
                    //20180423 HO
                    //updates die rolled to db
                    BLLayer.SetDieRolledFlagToMySQL(players[currentPlayer].Id, players[currentPlayer].DieRolled);
                }

                //20180422
                else
                {
                //btnDice.IsHitTestVisible = false;
                lblNotification.Content = "You have already rolled the die.";
                }

                ActionAfterMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show("buttonMoveToken_Click: " + ex.Message);
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

        private void ActionAfterMove()
        {
            if (cells[players[currentPlayer].Position].Owner != 0 && cells[players[currentPlayer].Position].Owner != players[currentPlayer].Id && cells[players[currentPlayer].Position].Price != 0)
            {
                players[currentPlayer].Cash -= cells[players[currentPlayer].Position].Rent;
                //db update
                BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash);
                BLLayer.SetCellOwnerToMySQL(players[currentPlayer].Id, cells[players[currentPlayer].Position].Id);

                foreach (Player p in players)
                {
                    if (cells[players[currentPlayer].Position].Owner == p.Id)
                    {
                        p.Cash += cells[players[currentPlayer].Position].Rent;
                        //db update
                        BLLayer.SetPlayerCashToMySQL(p.Id, p.Cash);
                        lblNotification.Content = "You paid " + cells[players[currentPlayer].Position].Rent + "$ to " + p.Name;
                    }
                }
            }

            else if (cells[players[currentPlayer].Position].Owner == 0 && cells[players[currentPlayer].Position].Price != 0 )
            {
                BuyProperty();
            }
        }

        private void BuyProperty()
        {
            StackPanel buyStack = new StackPanel();
            buyStack.Orientation = Orientation.Vertical;

            Button btnBuyProperty = new Button();
            btnBuyProperty.Background = Brushes.White;
            btnBuyProperty.Height = 20;
            btnBuyProperty.Width = 200;
            btnBuyProperty.Content = "Buy property for " + cells[players[currentPlayer].Position].Price + "$";
            btnBuyProperty.Click += new RoutedEventHandler(btnBuyProperty_Click);

            Button btnPassProperty = new Button();
            btnPassProperty.Background = Brushes.White;
            btnPassProperty.Height = 20;
            btnPassProperty.Width = 200;
            btnPassProperty.Content = "Pass";
            btnPassProperty.Click += new RoutedEventHandler(btnPassProperty_Click);

            buyStack.Children.Add(btnPassProperty);
            buyStack.Children.Add(btnBuyProperty);

            Canvas.SetLeft(buyStack, 250);
            Canvas.SetTop(buyStack, 250);
            canvasObj.Children.Add(buyStack);
        }

        private void btnBuyProperty_Click(object sender, RoutedEventArgs e)
        {
            cells[players[currentPlayer].Position].Owner = players[currentPlayer].Id;
            lblNotification.Content = "You bought " + cells[players[currentPlayer].Position].Name;
            BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash- cells[players[currentPlayer].Position].Price);
            RecreateCanvas();
        }

        private void btnPassProperty_Click(object sender, RoutedEventArgs e)
        {
            RecreateCanvas();
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
                    case 2:
                        {
                            r.Fill = Brushes.DarkBlue;
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

            catch (Exception)
            {
                throw;
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
                    btnEndturn.MouseEnter += new MouseEventHandler(btnEndTurn_MouseEnter); //MouseEnter testing


                    stack.Children.Add(btnDice);
                    stack.Children.Add(btnBuyBuildings);
                    stack.Children.Add(btnEndturn);

                    Canvas.SetLeft(stack, windowWidth * 0.5);
                    Canvas.SetTop(stack, windowHeight * 0.2);
                    canvasObj.Children.Add(stack);
                }
            }
        }

        //MouseEnter testing
        private void btnEndTurn_MouseEnter(object sender, RoutedEventArgs e)
        {
            lblNotification.Content = "You are about to end your turn";
        }

        //20180422
        private void btnNextPlayer_Click(object sender, RoutedEventArgs e)
        {
            //20180423 HO
            //warns the player if she hasn't moved
            if (players[currentPlayer].DieRolled == false)
            {
                lblNotification.Content = "You haven't rolled the die yet!";
            }

            else
            {
                RecreateCanvas();

                try
                {
                    BLMethod.NextTurn(ref currentPlayer, ref players);
                    //shows player's name in ui
                    lblCurrentPlayer.Content = "Player " + players[currentPlayer].Name;
                    //reset notifications
                    lblNotification.Content = "Notifications";
                    //btnDice.IsHitTestVisible = true;

                    //20180423 HO
                    //updates cash field to match current player
                    lblCash.Content = "Cash: " + BLLayer.GetPlayerCashFromMySQL(players[currentPlayer].Id);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("btnNextPlayer_Click: " + ex.Message);
                } 
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
                    if (players[currentPlayer].Id == c.Owner)
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
                    lblNotification.Content = "You do not own any properties";
                }
                else
                {
                    lblNotification.Content = "Select property to build on";
                }

                g.Children.Add(stack);
                Canvas.SetLeft(g, 250);
                Canvas.SetTop(g, 250);
                canvasObj.Children.Add(g);

            }
            catch (Exception ex)
            {
                MessageBox.Show("btnBuyBuildings_Click: " + ex.Message);
            }
        }

        private void btnBuyForCell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RecreateCanvas();

                List<Button> buttons = new List<Button>();
                Grid g = new Grid();
                StackPanel stack = new StackPanel();

                Button btn = (Button)sender;

                propertyId = 0;

                foreach (Cell c in cells)
                {
                    if (c.Name == btn.Content.ToString())
                    {
                        propertyId = c.Id - 1;
                    }
                }

                CheckIfPlayerOwnsAllOfSameColorProperties(cells[propertyId].SerieId);

                if (ownsAll == true)
                {

                    if (cells[propertyId].HouseCount == 4)
                    {
                        lblNotification.Content = "Property has 4 houses";
                        buttons.Add(new Button());
                        buttons[0].Height = 20;
                        buttons[0].Width = 200;
                        buttons[0].Content = "Buy a Hotel";
                        buttons[0].Background = Brushes.White;
                        buttons[0].Click += new RoutedEventHandler(btnBuyHotel_Click);
                        stack.Children.Add(buttons[0]);
                    }
                    else if (cells[propertyId].HotelCount == 1)
                    {
                        lblNotification.Content = "Property has maximum amount of hotels";
                    }
                    else
                    {
                        lblNotification.Content = "Property has " + cells[propertyId].HouseCount + " houses";
                        buttons.Add(new Button());
                        buttons[0].Height = 20;
                        buttons[0].Width = 200;
                        buttons[0].Content = "Buy a House";
                        buttons[0].Background = Brushes.White;
                        buttons[0].Click += new RoutedEventHandler(btnBuyHouse_Click);
                        stack.Children.Add(buttons[0]);
                    }
                    g.Children.Add(stack);
                    Canvas.SetZIndex(g, 1000);
                    Canvas.SetLeft(g, 250);
                    Canvas.SetTop(g, 250);
                    canvasObj.Children.Add(g);
                }

                else
                {
                    lblNotification.Content = "You need to own every property with the same color to build";
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("btnBuyForCell_Click: " + ex.Message);
            }
        }

        private void CheckIfPlayerOwnsAllOfSameColorProperties(int serie)
        {
            cellserie = cells.FindAll(x => x.SerieId.Equals(serie));
            int i = 0;

            foreach (Cell c in cellserie)
            {
                if (c.Owner == players[currentPlayer].Id)
                {
                    i++;
                    serieId = c.SerieId;
                }
            }

            if (i == cellserie.Count)
            {
                ownsAll = true;
            }
            else
            {
                ownsAll = false;
            }
        }

        private void SetPlayerCashAndCellRent()
        {
            //set cell rent to what it should be after buying buildings
            try
            {
                if (cells[propertyId].HouseCount == 1)
                {
                    players[currentPlayer].Cash -= buildingcosts[cells[propertyId].SerieId - 1];
                    cells[propertyId].Rent = rents[propertyId * 5];
                }
                else if (cells[propertyId].HouseCount == 2)
                {
                    players[currentPlayer].Cash -= buildingcosts[cells[propertyId].SerieId - 1];
                    cells[propertyId].Rent = rents[propertyId * 5 + 1];
                }
                else if (cells[propertyId].HouseCount == 3)
                {
                    players[currentPlayer].Cash -= buildingcosts[cells[propertyId].SerieId - 1];
                    cells[propertyId].Rent = rents[propertyId * 5 + 2];
                }
                else if (cells[propertyId].HouseCount == 4)
                {
                    players[currentPlayer].Cash -= buildingcosts[cells[propertyId].SerieId - 1];
                    cells[propertyId].Rent = rents[propertyId * 5 + 3];
                }
                else if (cells[propertyId].HotelCount == 1)
                {
                    players[currentPlayer].Cash -= buildingcosts[cells[propertyId].SerieId - 1];
                    cells[propertyId].Rent = rents[propertyId * 5 + 4];
                }

                BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash);

                lblCash.Content = "Cash: " + BLLayer.GetPlayerCashFromMySQL(players[currentPlayer].Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetPlayerCashAndCellRent: " + ex.Message);
            }
        }

        private void AddImageToCanvas(string path, double width, double height, double left, double top)
        {
            BitmapImage bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            bi.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            // add player tokens into list
            Image image = new Image { Width = width, Height = height, Source = bi };
            Canvas.SetLeft(image, left);
            Canvas.SetTop(image, top);
            canvasObj.Children.Add(image);
        }

        private void btnBuyHotel_Click(object sender, RoutedEventArgs e)
        {
            cells[propertyId].HouseCount = 0;

            RecreateCanvas();

            AddImageToCanvas("/hotel.png", windowWidth / 100 * 3, windowWidth / 100 * 3, buildingPoints[propertyId * 4].X, buildingPoints[propertyId * 4].Y);

            cells[propertyId].HotelCount++;

            BLLayer.SetCellBuildingCountsToMySQL(cells[propertyId].Id, cells[propertyId].HotelCount, cells[propertyId].HouseCount);

            SetPlayerCashAndCellRent();
        }

        private void btnBuyHouse_Click(object sender, RoutedEventArgs e)
        {
            CheckIfPlayerOwnsAllOfSameColorProperties(cells[propertyId].SerieId);

            try
            {
                if (cells[propertyId].HouseCount < 4)
                {
                    AddImageToCanvas("/house.png", windowWidth / 100 * 3, windowWidth / 100 * 3, buildingPoints[propertyId * 4 + cells[propertyId].HouseCount].X, buildingPoints[propertyId * 4 + cells[propertyId].HouseCount].Y);

                    cells[propertyId].HouseCount++;

                    lblNotification.Content = "Property has " + cells[propertyId].HouseCount + " houses";

                    BLLayer.SetCellBuildingCountsToMySQL(cells[propertyId].Id, cells[propertyId].HotelCount, cells[propertyId].HouseCount);

                    SetPlayerCashAndCellRent();
                }
                else
                {
                    lblNotification.Content = "Maximum number of houses on property.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnBuyHouse_Click: " + ex.Message);
            }
        }

        //20180425 HO
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            //  IMPROVEMENT IDEAS
            //  New window?
            //  Should create new dg or other element to show players
            //  Should create Done-button
            //  Clicking dg selects player id and removes that row
            //  Destroy elemenents after players selected
            //  All info to one class and one db insert using that class

            //generate new game id
            int newGameId = BLMethod.NewGameId();
            BLLayer.SetNewGameIdToMySQL(newGameId);

            //get players from db
            dgCellTest.ItemsSource = BLMethod.ShowPlayers();
        }

        //new players
        //IMPROVEMENT IDEAS
        //if (alreadySelected) change bgcolor or something
        List<Player> newPlayers = new List<Player>();
        private void dgCellTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Player selected = dgCellTest.SelectedItem as Player;
            if (selected != null)
            {
                int selId = selected.Id;
                bool alreadySelected = newPlayers.Exists(x => x.Id.ToString().Contains(selId.ToString()));
                if (alreadySelected)
                {
                    MessageBox.Show("Already selected");
                }
                else
                {
                    lblNotification.Content = "Player " + selected.Name + " added to new game";
                    newPlayers.Add(selected);
                }
            }
            
        }

        //var gr = sender as Grid;
        //var grch = gr.Children;
        //var tb = grch[2] as TextBlock; // this child contains cell's name
        //var tbValueName = tb.Text;
        //Cell cellCopy = cells.Find(x => x.Name.Contains(tbValueName)); // gain access to Cell properties at hovered cell
        //lblNotification.Content = cellCopy.Id;
        //List<Cell> tempCellList = new List<Cell>();
        //tempCellList.Add(cellCopy);
        //dgCellTest.ItemsSource = tempCellList; // temp, just to verify

    }
}