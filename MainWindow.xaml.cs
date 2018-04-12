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

namespace TTOS0300_UI_Programming_Collaboration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Grid> grids = new List<Grid>();
        List<TextBlock> textBlocks = new List<TextBlock>();
        List<Player> players = new List<Player>();
        List<Cell> cells = new List<Cell>();
        List<Border> borders = new List<Border>();
        int bordernumber = 0;

        public static double windowWidth = 0;
        public static double windowHeight = 0;
        List<string> streets = new List<string>();
        Color b = Color.FromRgb(0, 0, 0);
        Color bg = Color.FromRgb(75, 143, 252);
        static int tenth = 10;
        static int larger = 7;
        static int corner = larger;

        public MainWindow()
        {
            InitializeComponent();
            LoadPlayers();
            for (int i = 0; i < 36; i++)
            {
                streets.Add(i.ToString());
            }
        }

        private void LoadPlayers()
        {
            try
            {
                players = BLLayer.GetAllPlayersFromDt();
                //players = BLLayer.GetPlayerList(); above is better
                dgDbTest.ItemsSource = players;
                cells = BLLayer.GetAllCellsFromDt();
                //celldbTest.ItemsSource = cells;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get our game window area
            FrameworkElement client = this.Content as FrameworkElement;
            windowWidth = (double)client.ActualWidth;
            windowHeight = (double)client.ActualHeight;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // get our game window area
            FrameworkElement client = this.Content as FrameworkElement;
            windowWidth = (double)client.ActualWidth;
            windowHeight = (double)client.ActualHeight;
            canvasObj.Children.Clear();
            bordernumber = 0;
            PrintGrid();
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

        private void AddGrid(double x, double y, Color color, Color bg,string side)
        {
            try
            {
                Grid g = new Grid();

                borders.Add(new Border());

                borders[bordernumber].BorderBrush = Brushes.Black;
                borders[bordernumber].BorderThickness = new Thickness(2);

                //grid dimension
                switch (side)
                {
                    default:
                        g.Height = windowHeight / tenth;
                        g.Width = windowWidth / tenth;
                        break;
                    case "left":
                        g.Height = (windowHeight - 2 * (windowHeight / corner)) / tenth;
                        g.Width = windowWidth / larger;
                        break;
                    case "top":
                        g.Height = windowHeight / larger;
                        g.Width = (windowWidth - 2 * (windowWidth / corner)) / tenth;
                        break;
                    case "right":
                        g.Height = (windowHeight - 2 * (windowHeight / corner)) / tenth;
                        g.Width = windowWidth / larger;
                        break;
                    case "bottom":
                        g.Height = windowHeight / larger;
                        g.Width = (windowWidth - 2 * (windowWidth / corner)) / tenth;
                        break;
                    case "corner":
                        g.Height = windowHeight / corner;
                        g.Width = windowWidth / corner;
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
                
                /*
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(@"D:\L4623\TTOS0300_UI_Programming_Collaboration\token2.png", UriKind.Relative);
                bi3.EndInit();
                Image img1 = new Image();
                //img1.Height = 40;
                //img1.Width = 40;
                img1.Stretch = Stretch.Fill;
                img1.Source = bi3;
                Grid.SetRow(img1, 3);
                Grid.SetColumn(img1, 0);
                */
                Rectangle r1 = new Rectangle();
                r1.Width = (double)windowHeight / 10 /4 - 5;
                r1.Height = (double)windowWidth / 10 /4 - 5;
                //Grid.SetRow(r1, 2);
                //Grid.SetColumn(r1, 0);
                r1.Fill = new ImageBrush(new BitmapImage(new Uri("../TTOS0300_UI_Programming_Collaboration/token2.png", UriKind.Relative)));

                Rectangle r2 = new Rectangle();
                r2.Width = (double)windowHeight / 10 /4 - 5;
                r2.Height = (double)windowWidth / 10 /4 - 5;
                //Grid.SetRow(r2, 2);
                //Grid.SetColumn(r2, 0);
                r2.Fill = new ImageBrush(new BitmapImage(new Uri("../TTOS0300_UI_Programming_Collaboration/token3.png", UriKind.Relative)));

                TextBlock txt2 = new TextBlock();
                if (cells[bordernumber].Price > 0)
                {
                    txt2.Text = cells[bordernumber].Price.ToString() + "$";
                }
                txt2.FontSize = 12;
                txt2.TextAlignment = TextAlignment.Center;
                Grid.SetRow(txt2, 3);
                Grid.SetColumn(txt2, 0);

                stack.Children.Add(r1);
                stack.Children.Add(r2);

                g.Children.Add(stack);
                g.Children.Add(r);
                g.Children.Add(txt1);
                g.Children.Add(txt2);

                borders[bordernumber].Child = g;
                Canvas.SetLeft(borders[bordernumber], x);
                Canvas.SetTop(borders[bordernumber], y);

                canvasObj.Children.Add(borders[bordernumber]);

                bordernumber++;
            }

            catch (Exception)
            {
                throw;
                //MessageBox.Show(ex.Message);
            }
        }

        private void PrintGrid()
        {
            int j = 1; //used to determine coordinates for printing
            int height = 0;
            int width = 0;
            double temph = 0;
            double tempw = 0;
            for (int i = 0; i < 36; i++)
            {
                if (i == 0) // bottom left corner
                {
                    temph = (windowHeight - ((windowHeight / 10) * j));
                    height = (int)Math.Round(temph, 0);
                    AddGrid(0, height, b, bg, "corner");
                    j++;
                }
                else if (i < 9) // left cells
                {
                    double available_height_between_corners = windowHeight - 2 * (windowHeight / larger);
                    temph = (windowHeight - ((available_height_between_corners / 8) * j));
                    height = (int)Math.Round(temph, 0);
                    AddGrid(0, height, b, bg, "left");
                    j++;
                }
                else if (i == 9) // top left corner
                {
                    temph = (windowHeight - ((windowHeight / 10) * j));
                    height = (int)Math.Round(temph, 0);
                    AddGrid(0, height, b, bg, "corner");
                    j = 1;
                }
                else if (i < 18) // top cells
                {
                    tempw = ((windowWidth / 10 * j));
                    width = (int)Math.Round(tempw, 0);
                    AddGrid(width, 0, b, bg, "top");
                    j++;
                }
                else if (i == 18) // top right corner
                {
                    temph = (windowHeight - ((windowHeight / 10) * j));
                    height = (int)Math.Round(temph, 0);
                    AddGrid(0, height, b, bg, "corner");
                    j = 1;
                }
                else if (i < 27) // right cells
                {
                    tempw = (windowWidth * 0.9);
                    width = (int)Math.Round(tempw, 0);
                    temph = ((windowHeight / 10) * j);
                    height = (int)Math.Round(temph, 0);
                    AddGrid(width, height, b, bg, "right");
                    j++;
                }
                else if (i == 27) // bottom right corner
                {
                    temph = (windowHeight - ((windowHeight / 10) * j));
                    height = (int)Math.Round(temph, 0);
                    AddGrid(0, height, b, bg, "corner");
                    j = 2;
                }
                else // bottom cells
                {
                    tempw = (windowWidth - windowWidth / 10 * j);
                    width = (int)Math.Round(tempw, 0);
                    temph = (windowHeight * 0.9);
                    height = (int)Math.Round(temph, 0);
                    AddGrid(width, height - 20, b, bg, "bottom");
                    j++;
                    if (j == 10)
                    {
                        j = 1;
                    }
                }
            }
        }

        private void btnDice_Click(object sender, RoutedEventArgs e)
        {
            Random rng = new Random();

            players[0].Position += rng.Next(1, 6);
        }
    }
}