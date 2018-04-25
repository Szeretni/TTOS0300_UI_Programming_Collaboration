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
        List<Point> hoverPoints = new List<Point>();
        List<DoubleAnimation> da = new List<DoubleAnimation>();
        List<Image> buildings = new List<Image>();
        List<Cell> cellserie = new List<Cell>();
        List<Player> newplayers = new List<Player>(); //20180425 HO used to manage new game's players
        NewGame newgame = new NewGame(); //20180425 HO used to manage new game
        
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
        int temp = 0;
        string pname = "";

        public static double windowWidth = 0;
        public static double windowHeight = 0;

        public MainWindow()
        {
            MessageBox.Show(Properties.Settings.Default.settingsCurrentGameId.ToString());
            Properties.Settings.Default.settingsCurrentGameId = 1;
            InitializeComponent();
            //20180422
            LoadPlayers();

            //20180422
            if (players.Count() != 0)
            {
                int currentPlayerId = BLLayer.GetCurrentPlayerIdFromMySQL();
                int i = 0;
                foreach (Player pl in players)
                {
                    if (currentPlayerId == pl.Id)
                    {
                        currentPlayer = i;
                        break;
                    }
                    i++;
                }
                
                lblCurrentPlayer.Content = "Player " + players[currentPlayer].Name;
            }

            Binding b = new Binding
            {
                Source = players[0]
            };

            txtTest.SetBinding(TextBlock.DataContextProperty, b);

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
                borders[i].Child.MouseLeave += Child_MouseLeave;
            }
        }

        private void Child_MouseLeave (object sender, MouseEventArgs e)
        {
            RecreateCanvas();
        }

        //20180424
        private void Child_MouseEnter(object sender, MouseEventArgs e)
        {
            RecreateCanvas();
            try
            {
                List<TextBlock> txtBlocks = new List<TextBlock>();
                Border b = new Border();
                b.BorderBrush = Brushes.Black;
                b.BorderThickness = new Thickness(1, 1, 1, 1);
                StackPanel stack = new StackPanel();
                var gr = sender as Grid;
                var grch = gr.Children;
                var tb = grch[2] as TextBlock; // this child contains cell's name
                var tbValueName = tb.Text;
                Cell cellCopy = cells.Find(x => x.Name.Contains(tbValueName)); // gain access to Cell properties at hovered cell
                List<Cell> tempCellList = new List<Cell>
                {
                    cellCopy
                };

                foreach (Player p in players)
                {
                    if (tempCellList[0].Owner == p.Id)
                    {
                        pname = p.Name;
                    }
                    else
                    {
                        pname = "No owner";
                    }
                }
                txtBlocks.Add(new TextBlock { Name = "cellOwner", Text = "Owner:", Background = Brushes.White, Padding = new Thickness(2, 1, 2, 0) });
                txtBlocks.Add(new TextBlock { Name = "cellOwner", Text =  pname, Background = Brushes.White, Padding = new Thickness(2, 0, 2, 0) });
                txtBlocks.Add(new TextBlock { Name = "cellRent", Text = "Rent:", Background = Brushes.White, Padding = new Thickness(2, 0, 2, 0) });
                txtBlocks.Add(new TextBlock { Name = "cellRent", Text = tempCellList[0].Rent.ToString(), Background = Brushes.White, Padding = new Thickness(2, 0, 2, 1) });

                stack.Children.Add(txtBlocks[0]);
                stack.Children.Add(txtBlocks[1]);
                stack.Children.Add(txtBlocks[2]);
                stack.Children.Add(txtBlocks[3]);

                b.Child = stack;

                int cellPosition = tempCellList[0].Id - 1;

                if (cellPosition == 2 ||cellPosition == 6)
                {
                }
                else if (cellPosition == 0 || cellPosition == 9 || cellPosition == 18 || cellPosition == 27)
                {
                    Canvas.SetLeft(b, hoverPoints[cellPosition].X);
                    Canvas.SetTop(b, hoverPoints[cellPosition].Y);
                    canvasObj.Children.Add(b);
                }
                else if (cellPosition != 0 && cellPosition < 9)
                {
                    Canvas.SetLeft(b, hoverPoints[cellPosition].X);
                    Canvas.SetTop(b, hoverPoints[cellPosition].Y);
                    canvasObj.Children.Add(b);
                }
                else if (cellPosition != 9 && cellPosition > 9 && cellPosition < 19)
                {
                    Canvas.SetLeft(b, hoverPoints[cellPosition].X);
                    Canvas.SetTop(b, hoverPoints[cellPosition].Y);
                    canvasObj.Children.Add(b);
                }
                else if (cellPosition != 18 && cellPosition > 18 && cellPosition < 28)
                {
                    Canvas.SetLeft(b, hoverPoints[cellPosition].X);
                    Canvas.SetTop(b, hoverPoints[cellPosition].Y);
                    canvasObj.Children.Add(b);
                }
                else if (cellPosition != 27 && cellPosition > 27 && cellPosition < 36)
                {
                    Canvas.SetLeft(b, hoverPoints[cellPosition].X);
                    Canvas.SetTop(b, hoverPoints[cellPosition].Y);
                    canvasObj.Children.Add(b);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hover: " + ex.Message);
            }
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
                hoverPoints.Clear();
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

        private void OnbuttonMoveToken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                temp = currentPlayer;
                //if die rolled, cannot roll again
                if (players[currentPlayer].DieRolled != true)
                {
                    Random rnd = new Random();

                    //20180422 players[0] -> players[currentPlayer]
                    //also token[0] -> token[currentPlayer]
                    DieResult = rnd.Next(2,12);

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
        //            da[j + 2].To = points[(players[currentPlayer].Position + i + 1) * 4].X;
        //            da[j + 2].BeginTime = TimeSpan.FromSeconds(i);
        //        }
        //        da[j + 2].Duration = new Duration(TimeSpan.FromMilliseconds(500));

        //        da.Add(new DoubleAnimation());
        //        if (players[currentPlayer].Position == 27)
        //        {
        //            da[k + 2].From = points[(temp + i) * 4].Y;
        //            da[k + 2].To = points[36].Y;
        //        }
        //        else
        //        {
        //            da[k + 2].From = points[(temp + i) * 4].Y;
        //            da[k + 2].To = points[(players[currentPlayer].Position + i + 1) * 4].Y;
        //            da[k + 2].BeginTime = TimeSpan.FromSeconds(i);
        //        }
        //        da[k + 2].Duration = new Duration(TimeSpan.FromMilliseconds(500));

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
            StackPanel buyStack = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            Button btnBuyProperty = new Button
            {
                Background = Brushes.White,
                Height = 20,
                Width = 200,
                Content = "Buy property for " + cells[players[currentPlayer].Position].Price + "$"
            };
            btnBuyProperty.Click += new RoutedEventHandler(OnbtnBuyProperty_Click);

            Button btnPassProperty = new Button
            {
                Background = Brushes.White,
                Height = 20,
                Width = 200,
                Content = "Pass"
            };
            btnPassProperty.Click += new RoutedEventHandler(OnbtnPassProperty_Click);

            buyStack.Children.Add(btnPassProperty);
            buyStack.Children.Add(btnBuyProperty);

            Canvas.SetLeft(buyStack, 250);
            Canvas.SetTop(buyStack, 250);
            canvasObj.Children.Add(buyStack);
        }

        private void OnbtnBuyProperty_Click(object sender, RoutedEventArgs e)
        {
            cells[players[currentPlayer].Position].Owner = players[currentPlayer].Id;
            lblNotification.Content = "You bought " + cells[players[currentPlayer].Position].Name;
            BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash- cells[players[currentPlayer].Position].Price);
            RecreateCanvas();
        }

        private void OnbtnPassProperty_Click(object sender, RoutedEventArgs e)
        {
            RecreateCanvas();
        }

        private void AddGrid(double x, double y, string side)
        {
            try
            {
                Grid g = new Grid
                {
                    Background = Brushes.AliceBlue
                };
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

                TextBlock txt1 = new TextBlock
                {
                    Text = cells[bordernumber].Name,
                    FontSize = 12,
                    TextAlignment = TextAlignment.Center,
                    TextWrapping = TextWrapping.WrapWithOverflow
                };
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

                //set coordinates for player token spots, building spots and grid hoverinfo points
                if (bordernumber == 0 || bordernumber == 9 || bordernumber == 18 || bordernumber == 27)
                {
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.05), Y = y + (windowHeight * 0.16 * 0.7) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.25), Y = y + (windowHeight * 0.16 * 0.7) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.45), Y = y + (windowHeight * 0.16 * 0.7) });
                    points.Add(new Point { X = x + (windowWidth * 0.16 * 0.65), Y = y + (windowHeight * 0.16 * 0.7) });

                    if (bordernumber == 0)
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.17), Y = windowHeight - (windowHeight * 0.26 )});
                    }
                    else if (bordernumber == 9)
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.17), Y = y + (windowHeight * 0.17) });
                    }
                    else if (bordernumber == 18)
                    {
                        hoverPoints.Add(new Point { X = windowWidth - (windowWidth * 0.24), Y = y + (windowHeight * 0.17) });
                    }
                    else if (bordernumber == 27)
                    {
                        hoverPoints.Add(new Point { X = windowWidth - (windowWidth * 0.24), Y = windowHeight - (windowHeight * 0.26) });
                    }

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

                    if (bordernumber == 1)
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.17), Y = windowHeight - (windowHeight * 0.26) });
                    }

                    else if (bordernumber == 8)
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.17), Y = y + (windowHeight * 0.01) });
                    }

                    else
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.17), Y = y });
                    }

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

                    if (bordernumber == 17)
                    {
                        hoverPoints.Add(new Point { X = windowWidth - (windowWidth * 0.24), Y = y + (windowHeight * 0.17) });
                    }

                    else
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.01), Y = y + (windowHeight * 0.17) });
                    }

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

                    if (bordernumber == 19)
                    {
                        hoverPoints.Add(new Point { X = windowWidth - (windowWidth * 0.24), Y = y + (windowHeight * 0.01) });
                    }

                    else if (bordernumber == 26)
                    {
                        hoverPoints.Add(new Point { X = windowWidth - (windowWidth * 0.24), Y = windowHeight - (windowHeight * 0.26) });
                    }

                    else
                    {
                        hoverPoints.Add(new Point { X = windowWidth - (windowWidth * 0.24), Y = y });
                    }

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

                    if (bordernumber == 28)
                    {
                        hoverPoints.Add(new Point { X = windowWidth - (windowWidth * 0.24), Y = windowHeight - (windowHeight * 0.26) });
                    }

                    else if (bordernumber == 35)
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.01), Y = windowHeight - (windowHeight * 0.26) });
                    }

                    else
                    {
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.01), Y = windowHeight - (windowHeight * 0.26) });
                    }

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
                    StackPanel stack = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    Button btnDice = new Button
                    {
                        Height = windowHeight * 0.05,
                        Width = windowWidth * 0.1,
                        Content = "Roll Dice",
                        Background = Brushes.Coral
                    };
                    btnDice.Click += new RoutedEventHandler(OnbuttonMoveToken_Click);

                    Button btnBuyBuildings = new Button
                    {
                        Height = windowHeight * 0.05,
                        Width = windowWidth * 0.1,
                        Content = "Construct Buildings",
                        Background = Brushes.Coral
                    };
                    btnBuyBuildings.Click += new RoutedEventHandler(OnbtnBuyBuildings_Click);

                    Button btnEndturn = new Button
                    {
                        Height = windowHeight * 0.05,
                        Width = windowWidth * 0.1,
                        Content = "End turn",
                        Background = Brushes.Coral
                    };
                    btnEndturn.Click += new RoutedEventHandler(OnbtnNextPlayer_Click);

                    Button btnMenu = new Button
                    {
                        Height = windowHeight * 0.05,
                        Width = windowWidth * 0.1,
                        Content = "Menu",
                        Background = Brushes.Coral
                    };
                    btnMenu.Click += new RoutedEventHandler(OnbtnMenu_Click);


                    stack.Children.Add(btnDice);
                    stack.Children.Add(btnBuyBuildings);
                    stack.Children.Add(btnEndturn);
                    stack.Children.Add(btnMenu);

                    Canvas.SetLeft(stack, windowWidth * 0.4);
                    Canvas.SetTop(stack, windowHeight * 0.23);
                    canvasObj.Children.Add(stack);

                    Label tbNotifications = new Label
                    {
                        Name = "tbNotifications",

                        FontSize = 12,

                        Foreground = Brushes.White,

                        Padding = new Thickness(2, 2, 2, 2),

                        Content = "Notifications"
                    };

                    Label lbPlayerName = new Label
                    {
                        Name = "lbPlayerName",

                        FontSize = 12,

                        Foreground = Brushes.White,

                        Padding = new Thickness(2, 2, 2, 2),

                        Content = "Current player: "
                    };

                    Label lbPlayerCash = new Label
                    {
                        Name = "lbPlayerCash",

                        FontSize = 12,

                        Foreground = Brushes.White,

                        Padding = new Thickness(2,2,2,2),

                        Content = "Player cash: "
                    };

                    Binding b = new Binding
                    {
                        Source = players[currentPlayer]
                    };

                    Binding bCash = new Binding
                    {
                        Path = new PropertyPath("Cash")
                    };

                    Binding bName = new Binding
                    {
                        Path = new PropertyPath("Name")
                    };

                    lbPlayerName.SetBinding(Label.DataContextProperty, b);
                    lbPlayerCash.SetBinding(Label.DataContextProperty, b);

                    lbPlayerName.SetBinding(Label.ContentProperty, bName);
                    lbPlayerCash.SetBinding(Label.ContentProperty, bCash);

                    Border br = new Border
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(2, 2, 2, 2)
                    };

                    StackPanel infoStack = new StackPanel();

                    infoStack.Children.Add(tbNotifications);
                    infoStack.Children.Add(lbPlayerName);
                    infoStack.Children.Add(lbPlayerCash);

                    br.Child = infoStack;

                    Canvas.SetLeft(br, windowWidth * 0.2);
                    Canvas.SetTop(br, windowHeight * 0.17);
                    canvasObj.Children.Add(br);
                }
            }
        }

        private void OnbtnMenu_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stack = new StackPanel();

            Button btnNewGame = new Button
            {
                Height = windowHeight * 0.05,
                Width = windowWidth * 0.1,
                Content = "New Game",
                Background = Brushes.Coral
            };
            btnNewGame.Click += new RoutedEventHandler(OnbtnNewGame_Click);

            Button btnConfirm = new Button
            {
                Height = windowHeight * 0.05,
                Width = windowWidth * 0.1,
                Content = "Confirm",
                Background = Brushes.Coral
            };
            btnConfirm.Click += new RoutedEventHandler(OnbtnConfirm_Click);

            Button btnLoadGame = new Button
            {
                Height = windowHeight * 0.05,
                Width = windowWidth * 0.1,
                Content = "Load Game",
                Background = Brushes.Coral
            };
            btnLoadGame.Click += new RoutedEventHandler(OnbtnLoadGame_Click);

            stack.Children.Add(btnNewGame);
            stack.Children.Add(btnConfirm);
            stack.Children.Add(btnLoadGame);

            Canvas.SetLeft(stack, windowWidth * 0.4 + (btnConfirm.Width * 3));
            Canvas.SetTop(stack, windowHeight * 0.23 + btnConfirm.Height);
            canvasObj.Children.Add(stack);
        }

        //20180422
        private void OnbtnNextPlayer_Click(object sender, RoutedEventArgs e)
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
                    lblCash.Content = "Cash: " + BLLayer.GetPlayerCashFromMySQL(players[currentPlayer].Id); //20180425T2000
                    //lblCash.Content = "Cash: " + BLLayer.DynamicGetPlayerCashFromMySQL(players[currentPlayer].Id,Properties.Settings.Default.settingsCurrentGameId);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("btnNextPlayer_Click: " + ex.Message);
                } 
            }
        }

        private void OnbtnBuyBuildings_Click(object sender, RoutedEventArgs e)
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
                        Button btn = new Button
                        {
                            Height = 20,
                            Width = 200,
                            Content = c.Name,
                            Name = c.Name.ToString(),
                            Background = Brushes.White
                        };
                        btn.Click += new RoutedEventHandler(OnbtnBuyForCell_Click);
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

        private void OnbtnBuyForCell_Click(object sender, RoutedEventArgs e)
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
                        buttons[0].Click += new RoutedEventHandler(OnbtnBuyHotel_Click);
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
                        buttons[0].Click += new RoutedEventHandler(OnbtnBuyHouse_Click);
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

                lblCash.Content = "Cash: " + BLLayer.GetPlayerCashFromMySQL(players[currentPlayer].Id); //20180425T2000
                //lblCash.Content = "Cash: " + BLLayer.DynamicGetPlayerCashFromMySQL(players[currentPlayer].Id, Properties.Settings.Default.settingsCurrentGameId);
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

        private void OnbtnBuyHotel_Click(object sender, RoutedEventArgs e)
        {
            cells[propertyId].HouseCount = 0;

            RecreateCanvas();

            AddImageToCanvas("/hotel.png", windowWidth / 100 * 3, windowWidth / 100 * 3, buildingPoints[propertyId * 4].X, buildingPoints[propertyId * 4].Y);

            cells[propertyId].HotelCount++;

            BLLayer.SetCellBuildingCountsToMySQL(cells[propertyId].Id, cells[propertyId].HotelCount, cells[propertyId].HouseCount);

            SetPlayerCashAndCellRent();
        }

        private void OnbtnBuyHouse_Click(object sender, RoutedEventArgs e)
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

        private void OnbtnLoadGame_Click(object sender, RoutedEventArgs e)
        {

        }

        //20180425 HO
        private void OnbtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            //  IMPROVEMENT IDEAS
            //  New window?
            //  Should create new dg or other element to show players
            //  Should create Done-button
            //  Clicking dg selects player id and removes that row
            //  Destroy elemenents after players selected
            //  All info to one class and one db insert using that class

            //generate new game id
            newgame.GameId = BLMethod.NewGameId();

            //get players from db
            dgCellTest.ItemsSource = BLMethod.ShowPlayers();
        }

        //new players
        //IMPROVEMENT IDEAS
        //if (alreadySelected) change bgcolor or something
        private void OndgCellTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected player
            if (dgCellTest.SelectedItem is Player selected)
            {
                //is player already selected? -check
                int selId = selected.Id;
                bool alreadySelected = newplayers.Exists(x => x.Id.ToString().Contains(selId.ToString()));
                //player already selected for new game
                if (alreadySelected)
                {
                    string allselected = null;
                    foreach (Player p in newplayers)
                    {
                        allselected += p.Name + " ";
                    }
                    MessageBox.Show("Already selected players: " + allselected);
                }
                //add player to new game
                else
                {
                    lblNotification.Content = "Player " + selected.Name + " added to new game";
                    newplayers.Add(selected);
                }
            }
        }

        private void OnbtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newgame.NewPlayers = newplayers;
                //newgame to db
                BLMethod.NewGame(newgame);
                //clear newplayers for new new game
                newplayers = new List<Player>();
                MessageBox.Show("New Game Id: " + newgame.GameId.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnConfirm_Click: " + ex.Message);
            }
        }

        private void OnbtnNameChange_Click(object sender, RoutedEventArgs e)
        {
            players[0].Name = "Veijo";
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