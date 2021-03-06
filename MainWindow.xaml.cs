﻿using System;
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
        List<Image> buildings = new List<Image>();
        List<Player> newplayers = new List<Player>();
        List<int> games = new List<int>();
        List<TextBlock> cards = new List<TextBlock>();
        NewGame newgame = new NewGame();

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

        string[] chance =
        {
            "Maksa koulumaksuja 50$",
        };

        string[] communitychest =
        {
           "Lääkärinpalkkio maksa 50$",
        };

        int[] buildingcosts = { 50, 50, 100, 100, 150, 150, 200, 200 };

        int bordernumber = 0;
        int currentPlayer; 
        int propertyId = 0;
        bool ownsAll = false;

        public static double windowWidth = 0;
        public static double windowHeight = 0;

        public MainWindow()
        {
            LoadPlayers();
            InitializeComponent();
            if (players.Count() != 0)
            {
                int currentPlayerId = BLLayer.GetCurrentPlayerIdFromMySQL();
                int i = 0;
                foreach (Player p in players)
                {
                    if (currentPlayerId == p.Id)
                    {
                        currentPlayer = i;
                        break;
                    }
                    i++;
                }
            }
        }

        private void LoadPlayers()
        {
            try
            {   
                //current game's players and cells&games from db
                players = BLLayer.GetGamesPlayersFromDt();
                cells = BLLayer.GetAllCellsFromDt();
                games = BLLayer.GetGameSessionsFromDt();

                //init more player data to players 
                for (int i = 0; i < players.Count(); i++)
                {
                    players[i].DieRolled = BLLayer.GetDieRolledFlagFromMySQL(players[i].Id);
                    players[i].RentPaid = BLLayer.GetPlayerRentPaidFromMySQL(players[i].Id);
                    players[i].Position = BLLayer.GetPlayerPositionFromMySQL(players[i].Id);
                    players[i].Cash = BLLayer.GetPlayerCashFromMySQL(players[i].Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadPlayers " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
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
            for (int i = 0; i < bordernumber; i++)
            {
                borders[i].Child.MouseEnter += Child_MouseEnter;
            }
        }

        private void Child_MouseEnter(object sender, MouseEventArgs e)
        {
            RecreateCanvas();
            try
            {
                List<TextBlock> txtBlocks = new List<TextBlock>();

                string pname = "No owner";

                Border b = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1, 1, 1, 1)
                };

                StackPanel stack = new StackPanel();

                //using sender to get hovered cell's information
                var gr = sender as Grid;
                var grch = gr.Children;
                var tb = grch[2] as TextBlock; // this child contains cell's name at 2
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

                int cellPosition = tempCellList[0].Id;

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

                Binding bDiceResult = new Binding
                {
                    Path = new PropertyPath("DieResult")
                };

                txtPlayerName.SetBinding(TextBlock.DataContextProperty, b);
                txtPlayerCash.SetBinding(TextBlock.DataContextProperty, b);
                txtPlayerDiceRoll.SetBinding(TextBlock.DataContextProperty, b);

                txtPlayerName.SetBinding(TextBlock.TextProperty, bName);
                txtPlayerCash.SetBinding(TextBlock.TextProperty, bCash);
                txtPlayerDiceRoll.SetBinding(TextBlock.TextProperty, bDiceResult);

                brNotifications.Width = windowWidth * 0.64;
                brNotifications.Height = windowHeight * 0.05;
                brNotifications.Margin = new Thickness(windowWidth * 0.18, windowHeight * 0.27, windowWidth * 0.18, windowHeight * 0.25);

                brInfo.Width = windowWidth * 0.23;
                brInfo.Height = windowHeight * 0.08;
                brInfo.Margin = new Thickness(windowWidth * 0.25, windowHeight * 0.65, 0,0);
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
                    tokens.Add(new Image { Width = windowWidth / 100 * 3, Height = windowHeight / 100 * 3, Source = bi });
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

        //player can try to roll doubles 3 times to get out of jail
        private void OnbtnJailDice_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();

            int firstDice = rnd.Next(1, 6);
            int scndDice = rnd.Next(1, 6);

            if (firstDice == scndDice)
            {
                lblNotification.Content = "You were released from prison with diceroll" + firstDice.ToString() + ", " + scndDice.ToString();

                players[currentPlayer].JailTime = 0;

                players[currentPlayer].InJail = false;

                MoveToken(firstDice + scndDice);
            }

            else
            {
                players[currentPlayer].JailTime++;
                EndTurn("Your dice result: " + firstDice.ToString() + ", " + scndDice.ToString() + " ");
            }
        }

        //player can pay 50$ to get out of jail
        private void OnbtnPayBail_Click(object sender, RoutedEventArgs e)
        {
            players[currentPlayer].Cash -= 50;

            BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash);

            lblNotification.Content = "You paid 50$ in bail, you're free to roll the dice";

            players[currentPlayer].JailTime = 0;

            players[currentPlayer].InJail = false;
        }

        private void OnbuttonMoveToken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if player has been in jail for 3 turns he needs to pay 50$ to get out
                if (players[currentPlayer].InJail == true)
                {
                    if (players[currentPlayer].JailTime == 3)
                    {
                        players[currentPlayer].Cash -= 50;

                        BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash);

                        lblNotification.Content = "You paid 50$ in bail, you're free to roll the dice";

                        players[currentPlayer].JailTime = 0;

                        players[currentPlayer].InJail = false;
                    }

                    else
                    {
                        StackPanel stack = new StackPanel
                        {
                            Orientation = Orientation.Vertical
                        };

                        Button btnJailDice = new Button
                        {
                            Height = windowHeight * 0.05,
                            Width = windowWidth * 0.25,
                            FontSize = 14,
                            Content = "Roll Dice",
                        };
                        btnJailDice.Click += new RoutedEventHandler(OnbtnJailDice_Click);

                        Button btnPayBail = new Button
                        {
                            Height = windowHeight * 0.05,
                            Width = windowWidth * 0.25,
                            FontSize = 14,
                            Content = "Pay 50$ bail",
                        };
                        btnPayBail.Click += new RoutedEventHandler(OnbtnPayBail_Click);

                        stack.Children.Add(btnJailDice);
                        stack.Children.Add(btnPayBail);

                        Canvas.SetLeft(stack, windowWidth * 0.37);
                        Canvas.SetTop(stack, windowHeight * 0.35);
                        canvasObj.Children.Add(stack);
                    }
                }
                else
                {
                    Random rnd = new Random();            
                    MoveToken(rnd.Next(2, 12));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("buttonMoveToken_Click: " + ex.Message);
            }
        }

        private void MoveToken(int dieroll)
        {
            //if die rolled, cannot roll again
            if (players[currentPlayer].DieRolled != true)
            {
                players[currentPlayer].DieResult = dieroll;

                TokenAnimation();

                if (players[currentPlayer].Position + players[currentPlayer].DieResult == 27)
                {
                    players[currentPlayer].Position = 9;
                    EndTurn("You were sent to the prison! ");
                }

                else
                {
                    players[currentPlayer].Position += players[currentPlayer].DieResult;
                }

                //sets player's new position to db
                BLLayer.SetPlayerPositionToMySQL(players[currentPlayer].Id, players[currentPlayer].Position);

                //die rolled
                players[currentPlayer].DieRolled = true;
                
                //updates die rolled to db
                BLLayer.SetDieRolledFlagToMySQL(players[currentPlayer].Id, players[currentPlayer].DieRolled);
            }
            else
            {
                lblNotification.Content = "You have already rolled the die.";
            }
        }

        private void TokenAnimation()
        {
            try
            {
                Storyboard story = new Storyboard();
                story.Completed += new EventHandler(Story_Completed);

                //animate token one cell at a time for as many times as the diceroll result is
                for (int i = 0, j = 1, k = 1; i < players[currentPlayer].dieResult; i++, j++, k++)
                {
                    //if player would go past start, anim normally from player position to cell 35, from cell 35 to cell 0 and then normally again
                    if (players[currentPlayer].Position + players[currentPlayer].DieResult > 35)
                    {
                        if (players[currentPlayer].Position + j == 36)
                        {
                            NewDoubleAnimation(points[35 * 4 + currentPlayer].X, points[0 + currentPlayer].X, points[35 * 4 + currentPlayer].Y, points[0 + currentPlayer].Y,  k, story);

                            players[currentPlayer].dieResult -= i;

                            players[currentPlayer].Position = 0;

                            players[currentPlayer].Cash += 200;
                            BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash);
                            lblNotification.Content = "You collected 200$ in income by passing start";
                            i = 0;
                        }
                        else
                        {
                            NewDoubleAnimation(points[(players[currentPlayer].Position + i) * 4 + currentPlayer].X, points[(players[currentPlayer].Position + i + 1) * 4 + currentPlayer].X, points[(players[currentPlayer].Position + i) * 4 + currentPlayer].Y,
                            points[(players[currentPlayer].Position + i + 1) * 4 + currentPlayer].Y, k, story);
                        }
                    }

                    //if player lands on "go to prison", he'll be sent to prison from cell 27 to cell 9
                    else if (players[currentPlayer].Position + players[currentPlayer].DieResult == 27)
                    {
                        if (players[currentPlayer].Position + j == 27)
                        {
                            NewDoubleAnimation(points[108 + currentPlayer].X, points[36 + currentPlayer].X, points[108 + currentPlayer].Y, points[36 + currentPlayer].Y, i, story);

                            players[currentPlayer].InJail = true;
                        }

                        else
                        {
                            NewDoubleAnimation(points[(players[currentPlayer].Position + i) * 4 + currentPlayer].X, points[(players[currentPlayer].Position + i + 1) * 4 + currentPlayer].X, points[(players[currentPlayer].Position + i) * 4 + currentPlayer].Y,
                                points[(players[currentPlayer].Position + i + 1) * 4 + currentPlayer].Y, k, story);
                        }
                    }

                    else
                    {
                        NewDoubleAnimation(points[(players[currentPlayer].Position + i) * 4 + currentPlayer].X, points[(players[currentPlayer].Position + i + 1) * 4 + currentPlayer].X, points[(players[currentPlayer].Position + i) * 4 + currentPlayer].Y,
                            points[(players[currentPlayer].Position + i + 1) * 4 + currentPlayer].Y, k, story);
                    }
                }

                story.Begin(tokens[currentPlayer]);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TokenAnimation");
            }
        }

        private void Story_Completed(object sender, EventArgs e)
        {
            ActionAfterMove();
        }

        //animation for player token, 4 points per cell for tokens
        private void NewDoubleAnimation(double fromX, double toX, double fromY, double toY, int i, Storyboard story)
        {
            try
            {
                DoubleAnimation animX = new DoubleAnimation();

                animX.From = fromX;
                animX.To = toX;

                animX.BeginTime = TimeSpan.FromMilliseconds(500 * i);
                animX.Duration = new Duration(TimeSpan.FromMilliseconds(300));

                DoubleAnimation animY = new DoubleAnimation();

                animY.From = fromY;
                animY.To = toY;

                animY.BeginTime = TimeSpan.FromMilliseconds(500 * i);
                animY.Duration = new Duration(TimeSpan.FromMilliseconds(300));

                story.Children.Add(animX);
                Storyboard.SetTargetName(animX, tokens[currentPlayer].Name);
                Storyboard.SetTargetProperty(animX, new PropertyPath(Canvas.LeftProperty));

                story.Children.Add(animY);
                Storyboard.SetTargetName(animY, tokens[currentPlayer].Name);
                Storyboard.SetTargetProperty(animY, new PropertyPath(Canvas.TopProperty));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DoubleAnimation");
            }
        }

        //if player moves to a cell that someone owns, cells rent is automatically removed from his cash
        //if cell has no owner call for buyproperty method
        private void ActionAfterMove()
        {
            try
            {

                if (cells[players[currentPlayer].Position].Owner != 0 && cells[players[currentPlayer].Position].Owner != players[currentPlayer].Id && cells[players[currentPlayer].Position].Price != 0)
                {
                    //prevent unlimited rent issue by clickin roll
                    if (players[currentPlayer].RentPaid)
                    {
                        lblNotification.Content = "You've already moved and paid rent.";
                    }
                    else
                    {
                        //pays rent, status update
                        players[currentPlayer].Cash -= cells[players[currentPlayer].Position].Rent;
                        players[currentPlayer].RentPaid = true;
                        foreach (Player p in players)
                        {
                            if (cells[players[currentPlayer].Position].Owner == p.Id)
                            {
                                p.Cash += cells[players[currentPlayer].Position].Rent;
                                lblNotification.Content = "You paid " + cells[players[currentPlayer].Position].Rent + "$ to " + p.Name;
                            }
                        }
                    }
                }
                //if cell has no owner, player can buy it
                else if (cells[players[currentPlayer].Position].Owner == 0 && cells[players[currentPlayer].Position].Price != 0)
                {
                    BuyProperty();
                }
                //chance-cell
                else if (cells[players[currentPlayer].Position].Name == "Chance")
                {
                    cards.Add( new TextBlock
                    {
                        Background = Brushes.Coral,
                        FontSize = 1,
                        FontWeight = FontWeights.UltraBold,
                        Width = 1,
                        Text = chance[0],
                        TextAlignment = TextAlignment.Center,
                        Name = "Chance"
                    });

                    canvasObj.RegisterName(cards[0].Name, cards[0]);

                    Canvas.SetLeft(cards[0], windowWidth * 0.2);
                    Canvas.SetTop(cards[0], windowHeight * 0.4);
                    canvasObj.Children.Add(cards[0]);

                    CardAnimation(cards[0], windowHeight * 0.1, 1, windowWidth * 0.6, 1, 1, 20);
                }
                //community chest -cell
                else if (cells[players[currentPlayer].Position].Name == "Community Chest")
                {
                    cards.Add(new TextBlock
                    {
                        Background = Brushes.Coral,
                        FontSize = 1,
                        FontWeight = FontWeights.UltraBold,
                        Width = 1,
                        Text = communitychest[0],
                        TextAlignment = TextAlignment.Center,
                        Name = "Community"
                    });

                    canvasObj.RegisterName(cards[0].Name, cards[0]);

                    Canvas.SetLeft(cards[0], windowWidth * 0.2);
                    Canvas.SetTop(cards[0], windowHeight * 0.4);
                    canvasObj.Children.Add(cards[0]);

                    CardAnimation(cards[0], windowHeight * 0.1, 1, windowWidth * 0.6, 1, 1, 20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action after move");
            }
        }

        private void CardAnimation(TextBlock t, double toh, double fromh, double tow, double fromw, int fromFsize, int toFsize)
        {
            Storyboard cardstory = new Storyboard();

            cardstory.Completed += new EventHandler(CardStory_Completed);

            DoubleAnimation animHeight = new DoubleAnimation();

            animHeight.From = fromh;
            animHeight.To = toh;

            animHeight.Duration = new Duration(TimeSpan.FromMilliseconds(600));

            DoubleAnimation animWidth = new DoubleAnimation();

            animWidth.From = fromw;
            animWidth.To = tow;

            animWidth.Duration = new Duration(TimeSpan.FromMilliseconds(600));

            DoubleAnimation animFontSize = new DoubleAnimation();

            animFontSize.From = 1;
            animFontSize.To = 20;

            animFontSize.Duration = new Duration(TimeSpan.FromMilliseconds(600));

            cardstory.Children.Add(animHeight);
            Storyboard.SetTargetName(animHeight, t.Name);
            Storyboard.SetTargetProperty(animHeight, new PropertyPath(HeightProperty));

            cardstory.Children.Add(animWidth);
            Storyboard.SetTargetName(animWidth, t.Name);
            Storyboard.SetTargetProperty(animWidth, new PropertyPath((WidthProperty)));

            cardstory.Children.Add(animFontSize);
            Storyboard.SetTargetName(animFontSize, t.Name);
            Storyboard.SetTargetProperty(animFontSize, new PropertyPath(FontSizeProperty));

            cardstory.Begin(t);
        }

        private void CardStory_Completed(object sender, EventArgs e)
        {
            Button bCloseCard = new Button
            {
                Background = Brushes.Coral,
                FontSize = 14,
                FontWeight = FontWeights.UltraBold,
                Height = windowHeight * 0.05,
                Width = windowWidth * 0.25,
                Content = "Ok"
            };

            bCloseCard.Click += new RoutedEventHandler(OnbCloseCard_Click);

            Canvas.SetLeft(bCloseCard, windowWidth * 0.37);
            Canvas.SetTop(bCloseCard, (windowHeight * 0.35) + (windowHeight * 0.1));
            canvasObj.Children.Add(bCloseCard);
        }

        private void OnbCloseCard_Click(object sender, RoutedEventArgs e)
        {
            players[currentPlayer].Cash -= 50;
            BLLayer.SetPlayerCashToMySQL(players[currentPlayer].Id, players[currentPlayer].Cash);

            canvasObj.UnregisterName(cards[0].Name);

            cards.Clear();

            RecreateCanvas();

            lblNotification.Content = "You paid 50$";
        }

        private void BuyProperty()
        {
            Color c = Color.FromRgb(185, 214, 191);

            StackPanel buyStack = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            Button btnBuyProperty = new Button
            {
                Background = new SolidColorBrush(c),
                FontSize = 14,
                FontWeight = FontWeights.UltraBold,
                Height = windowHeight * 0.05,
                Width = windowWidth * 0.25,
                Content = "Buy property for " + cells[players[currentPlayer].Position].Price + "$"
            };
            btnBuyProperty.Click += new RoutedEventHandler(OnbtnBuyProperty_Click);

            Button btnPassProperty = new Button
            {
                Background = new SolidColorBrush(c),
                FontSize = 14,
                FontWeight = FontWeights.UltraBold,
                Height = windowHeight * 0.05,
                Width = windowWidth * 0.25,
                Content = "Pass"
            };
            btnPassProperty.Click += new RoutedEventHandler(OnbtnPassProperty_Click);

            buyStack.Children.Add(btnBuyProperty);
            buyStack.Children.Add(btnPassProperty);

            Canvas.SetLeft(buyStack, windowWidth * 0.37);
            Canvas.SetTop(buyStack, windowHeight * 0.35);
            canvasObj.Children.Add(buyStack);
        }

        //if player buys a property set cells owner to currentplayer in game and to database
        private void OnbtnBuyProperty_Click(object sender, RoutedEventArgs e)
        {
            cells[players[currentPlayer].Position].Owner = players[currentPlayer].Id;
            BLLayer.SetCellOwnerToMySQL(players[currentPlayer].Id, cells[players[currentPlayer].Position].Id);
            lblNotification.Content = "You bought " + cells[players[currentPlayer].Position].Name;
            players[currentPlayer].Cash -= cells[players[currentPlayer].Position].Price;
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
                Grid g = new Grid();

                borders.Add(new Border());
                borders[bordernumber].BorderBrush = Brushes.Black;

                //overlapping borders didn't quite work so this fixes them
                if (bordernumber != 0 && bordernumber < 9)
                {
                    borders[bordernumber].BorderThickness = new Thickness(0, 1, 1, 1);
                }
                else if (bordernumber != 9 && bordernumber > 9 && bordernumber < 19)
                {
                    borders[bordernumber].BorderThickness = new Thickness(1, 0, 1, 1);
                }
                else if (bordernumber != 18 && bordernumber > 18 && bordernumber < 28)
                {
                    borders[bordernumber].BorderThickness = new Thickness(1, 1, 0, 1);
                }
                else if (bordernumber != 27 && bordernumber > 27 && bordernumber < 36)
                {
                    borders[bordernumber].BorderThickness = new Thickness(1, 1, 1, 0);
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

                r.Height = windowHeight / 100 * 16;

                switch (cells[bordernumber].SerieId)
                {
                    case 1:
                        {
                            r.Fill = Brushes.Brown;
                            break;
                        }
                    case 2:
                        {
                            r.Fill = Brushes.SkyBlue;
                            break;
                        }
                    case 3:
                        {
                            r.Fill = Brushes.Purple;
                            break;
                        }
                    case 4:
                        {
                            r.Fill = Brushes.Orange;
                            break;
                        }
                    case 5:
                        {
                            r.Fill = Brushes.Red;
                            break;
                        }
                    case 6:
                        {
                            r.Fill = Brushes.Yellow;
                            break;
                        }
                    case 7:
                        {
                            r.Fill = Brushes.ForestGreen;
                            break;
                        }
                    case 8:
                        {
                            r.Fill = Brushes.DarkBlue;
                            break;
                        }
                }

                Grid.SetRow(r, 0);
                Grid.SetColumn(r, 0);

                TextBlock txt1 = new TextBlock
                {
                    Text = cells[bordernumber].Name,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
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
                        hoverPoints.Add(new Point { X = x + (windowWidth * 0.17), Y = windowHeight - (windowHeight * 0.26) });
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
            try
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
                        AddGrid(0, windowHeight - htop - (hsides * j), "sides");
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
                        AddGrid(windowWidth - wsides - (wtop * j), windowHeight - htop, "top");
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
                            Width = windowWidth * 0.16,
                            FontSize = 14,
                            Content = "Roll Dice",
                            Background = Brushes.Coral
                        };
                        btnDice.Click += new RoutedEventHandler(OnbuttonMoveToken_Click);

                        Button btnBuyBuildings = new Button
                        {
                            Height = windowHeight * 0.05,
                            Width = windowWidth * 0.16,
                            FontSize = 14,
                            Content = "Construct Buildings",
                            Background = Brushes.Coral
                        };
                        btnBuyBuildings.Click += new RoutedEventHandler(OnbtnBuyBuildings_Click);

                        Button btnEndturn = new Button
                        {
                            Height = windowHeight * 0.05,
                            Width = windowWidth * 0.16,
                            FontSize = 14,
                            Content = "End turn",
                            Background = Brushes.Coral
                        };
                        btnEndturn.Click += new RoutedEventHandler(OnbtnNextPlayer_Click);

                        Button btnMenu = new Button
                        {
                            Height = windowHeight * 0.05,
                            Width = windowWidth * 0.16,
                            FontSize = 14,
                            Content = "Menu",
                            Background = Brushes.Coral
                        };
                        btnMenu.Click += new RoutedEventHandler(OnbtnMenu_Click);

                        stack.Children.Add(btnDice);
                        stack.Children.Add(btnBuyBuildings);
                        stack.Children.Add(btnEndturn);
                        stack.Children.Add(btnMenu);

                        Canvas.SetLeft(stack, windowWidth * 0.18);
                        Canvas.SetTop(stack, windowHeight * 0.18);
                        canvasObj.Children.Add(stack);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("PrintGrid: " + ex.Message);
            }
        }

        bool menuClickFlag = true;
        private void OnbtnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (menuClickFlag)
            {
                menuClickFlag = false;

                StackPanel stack = new StackPanel();

                Button btnNewGame = new Button
                {
                    Height = windowHeight * 0.05,
                    Width = windowWidth * 0.16,
                    FontSize = 14,
                    Content = "New Game",
                    Background = Brushes.Coral
                };
                btnNewGame.Click += new RoutedEventHandler(OnbtnNewGame_Click);

                Button btnLoadGame = new Button
                {
                    Height = windowHeight * 0.05,
                    Width = windowWidth * 0.16,
                    FontSize = 14,
                    Content = "Load Game",
                    Background = Brushes.Coral
                };
                btnLoadGame.Click += new RoutedEventHandler(OnbtnLoadGame_Click);

                Button btnQuit = new Button
                {
                    Height = windowHeight * 0.05,
                    Width = windowWidth * 0.16,
                    FontSize = 14,
                    Content = "Quit",
                    Background = Brushes.Coral
                };
                btnQuit.Click += new RoutedEventHandler(OnbtnQuit_Click);

                stack.Children.Add(btnNewGame);
                stack.Children.Add(btnLoadGame);
                stack.Children.Add(btnQuit);

                Canvas.SetLeft(stack, windowWidth * 0.18 + (btnLoadGame.Width * 3));
                Canvas.SetTop(stack, windowHeight * 0.18 + btnLoadGame.Height);
                canvasObj.Children.Add(stack);
            }

            else
            {
                RecreateCanvas();
                menuClickFlag = true;
            }
        }
        private void OnbtnQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
        private void OnbtnNextPlayer_Click(object sender, RoutedEventArgs e)
        {
            //warns the player if she hasn't moved
            if (players[currentPlayer].DieRolled == false)
            {
                lblNotification.Content = "You haven't rolled the die yet!";
            }
            else
            {
                
                try
                {
                    EndTurn(""); //parameter is used in other calls, "" is intentional
                }
                catch (Exception ex)
                {
                    MessageBox.Show("btnNextPlayer_Click: " + ex.Message);
                } 
            }
        }

        private void EndTurn(string message)
        {
            //turn refresh
            BLMethod.NextTurn(ref currentPlayer, ref players);
            RecreateCanvas();
            
            //displays (new) current player
            string notification = "Current player: " + players[currentPlayer].Name;
            lblNotification.Content = message + notification;
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
                        // cannot build on railwaystations with serie id 9
                        if (c.SerieId != 9)
                        {
                            Button btn = new Button
                            {
                                Height = windowHeight * 0.03,
                                Width = windowWidth * 0.2,
                                Content = c.Name,
                                Background = Brushes.White
                            };
                            btn.Click += new RoutedEventHandler(OnbtnBuyForCell_Click);
                            stack.Children.Add(btn);

                            i++;
                        }
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
                Canvas.SetLeft(g, windowWidth * 0.4);
                Canvas.SetTop(g, windowHeight * 0.4);
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
                        Button btnBuyHotel = new Button
                        {
                            Height = windowHeight * 0.03,
                            Width = windowWidth * 0.2,
                            Content = "Buy a Hotel",
                            Background = Brushes.White
                        };

                        btnBuyHotel.Click += new RoutedEventHandler(OnbtnBuyHotel_Click);
                        stack.Children.Add(btnBuyHotel);
                    }
                    else if (cells[propertyId].HotelCount == 1)
                    {
                        lblNotification.Content = "Property has maximum amount of hotels";
                    }
                    else
                    {
                        lblNotification.Content = "Property has " + cells[propertyId].HouseCount + " houses";
                        Button btnBuyHouse = new Button
                        {
                            Height = windowHeight * 0.03,
                            Width = windowWidth * 0.2,
                            Content = "Buy a House",
                            Background = Brushes.White
                        };

                        btnBuyHouse.Click += new RoutedEventHandler(OnbtnBuyHouse_Click);
                        stack.Children.Add(btnBuyHouse);
                    }
                    g.Children.Add(stack);

                    Canvas.SetLeft(g, windowWidth * 0.4);
                    Canvas.SetTop(g, windowHeight * 0.4);
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

        //player must own every property with same color to build
        private void CheckIfPlayerOwnsAllOfSameColorProperties(int serie)
        {
            List<Cell> cellserie = new List<Cell>();
            cellserie = cells.FindAll(x => x.SerieId.Equals(serie));
            int i = 0;

            int serieId = 0;

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

        //set cells housecount to 0 and add a hotel image on cell
        private void OnbtnBuyHotel_Click(object sender, RoutedEventArgs e)
        {
            cells[propertyId].HouseCount = 0;

            RecreateCanvas();

            AddImageToCanvas("/hotel.png", windowWidth / 100 * 3, windowWidth / 100 * 3, buildingPoints[propertyId * 4].X, buildingPoints[propertyId * 4].Y);

            cells[propertyId].HotelCount++;

            BLLayer.SetCellBuildingCountsToMySQL(cells[propertyId].Id, cells[propertyId].HotelCount, cells[propertyId].HouseCount);

            SetPlayerCashAndCellRent();
        }

        //add a house image on cell, possible to build 4 houses
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
            ListBox list = new ListBox();
            list.Width = windowWidth * 0.2;
            list.Height = windowHeight * 0.2;
            list.SelectionMode = SelectionMode.Single;

            foreach (int game in games)
            {
                list.Items.Add(game.ToString());
            }

            list.SelectionChanged += new SelectionChangedEventHandler(OnListSelectionChanged);

            Canvas.SetLeft(list, windowWidth * 0.4);
            Canvas.SetTop(list, windowHeight * 0.4);
            canvasObj.Children.Add(list);
        }

        private void OnListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string text = e.AddedItems[0].ToString();

                MessageBox.Show(text);
                int gamesession = 1;
                bool result = int.TryParse(text, out int i);

                if (result)
                {
                    gamesession = i;
                }

                //sets loaded game id, gets it's data and resreshs board
                Properties.Settings.Default.settingsCurrentGameId = gamesession;
                LoadPlayers();
                RecreateCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "List selection");
            }
        }

        private void OnbtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stack = new StackPanel();

            //select players for new game
            foreach (Player p in BLLayer.GetAllPlayersFromDt())
            {
                //shows available players
                Button btnSelectPlayer = new Button
                {
                    Height = windowHeight * 0.03,
                    Width = windowWidth * 0.2,
                    DataContext = p,
                    Content = p.Name,
                    Background = Brushes.White
                };
                btnSelectPlayer.Click += new RoutedEventHandler(OnbtnSelectPlayer_Click);

                stack.Children.Add(btnSelectPlayer);
            }

            //create necessary buttons
            Button btnConfirm = new Button
            {
                Height = windowHeight * 0.03,
                Width = windowWidth * 0.2,
                Content = "Confirm",
                Background = Brushes.White
            };
            btnConfirm.Click += new RoutedEventHandler(OnbtnConfirm_Click);
            stack.Children.Add(btnConfirm);

            Button btnCancel = new Button
            {
                Height = windowHeight * 0.03,
                Width = windowWidth * 0.2,
                Content = "Cancel",
                Background = Brushes.White
            };
            btnCancel.Click += new RoutedEventHandler(OnbtnCancel_Click);

            stack.Children.Add(btnCancel);

            Canvas.SetLeft(stack, windowWidth* 0.4);
            Canvas.SetTop(stack, windowHeight* 0.4);
            canvasObj.Children.Add(stack);

            //generate new game id
            newgame.GameId = BLMethod.NewGameId();
        }

        private void OnbtnCancel_Click(object sender, RoutedEventArgs e)
        {
            newplayers.Clear();
            RecreateCanvas();
        }

        private void OnbtnSelectPlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button b = (Button)sender;

                //cannot select same player multiple times
                var p = BLLayer.GetAllPlayersFromDt().Find(x => x.Name.Contains(b.Content.ToString()));
                bool alreadySelected = newplayers.Exists(x => x.Id.ToString().Contains(p.Id.ToString()));
                if (alreadySelected)
                {
                    lblNotification.Content = "Player " + p.Name + " has already been selected";
                }
                //add player to new game
                else
                {
                    lblNotification.Content = "Player " + p.Name + " added to new game";
                    newplayers.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btnConfirm_Click");
            }
        }

        private void OnbtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newgame.NewPlayers = newplayers;
                //newgame to db
                BLMethod.NewGame(newgame, newplayers[0].Id);
                //clear newplayers for next new game button click
                newplayers = new List<Player>();
                MessageBox.Show("New Game Id: " + newgame.GameId.ToString());
                LoadPlayers();
                RecreateCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnConfirm_Click: " + ex.Message);
            }
        }
    }
}