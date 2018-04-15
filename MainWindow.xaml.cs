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
        List<Player> players = new List<Player>();
        List<Cell> cells = new List<Cell>();
        List<Border> borders = new List<Border>();
        int bordernumber = 0;

        public static double windowWidth = 0;
        public static double windowHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
            LoadPlayers();
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
            try
            {
                bordernumber = 0;
                PrintGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1" + ex.Message);
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
                if (bordernumber == 0 || bordernumber == 9 || bordernumber == 18 || bordernumber == 27)
                {
                }
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
                        g.Height = windowHeight / 100 * 18;
                        g.Width = windowWidth / 100 * 8;
                        break;
                    case "sides":
                        g.Height = windowHeight / 100 * 8;
                        g.Width = windowWidth / 100 * 18;
                        break;
                    case "top":
                        g.Height = windowHeight / 100 * 18;
                        g.Width = windowWidth / 100 * 8;
                        break;
                    case "corner":
                        g.Height = windowHeight / 100 * 18;
                        g.Width = windowWidth / 100 * 18;
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
            double htop = windowHeight / 100 * 18;
            double wtop = windowWidth / 100 * 8;
            double hsides = windowHeight / 100 * 8;
            double wsides = windowWidth / 100 * 18;
            for (int i = 0; i < 36; i++)
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
                    j = 1;
                }
                
                else if (i < 18) // top cells, left x - top y
                {
                    AddGrid(windowWidth - wsides - (wtop * j), 0, "top");
                    j++;
                }
                
                else if (i == 18) // top right corner, left x - top y
                {
                    AddGrid(windowWidth - wsides, 0, "corner");
                    j = 1;
                }
                
                else if (i < 27) // right cells, left x - top y
                {
                    AddGrid(windowWidth - wsides, windowHeight - htop - (hsides * j), "sides");
                    j++;
                }
                
                else if (i == 27) // bottom right corner, left x - top y
                {
                    AddGrid(windowWidth - wsides, windowHeight - htop, "corner");
                    j = 1;
                }

                else // bottom cells, left x - top y
                {
                    //(windowWidth - wsides - (wtop * j)
                    AddGrid(windowWidth - wsides - (wtop*j), windowHeight - htop, "top");
                    j++;
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