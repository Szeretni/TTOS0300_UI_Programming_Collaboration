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
using System.Drawing;

namespace TTOS0300_UI_Programming_Collaboration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Grid> grids = new List<Grid>();
        List<TextBlock> textBlocks = new List<TextBlock>();

        public static double windowWidth = 0;
        public static double windowHeight = 0;
        List<string> streets = new List<string>();
        Color b = Color.FromRgb(0, 0, 0);
        Color bg = Color.FromRgb(75, 143, 252);

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0;i<36;i++)
            {
                streets.Add(i.ToString());
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
            //canvasObj.Children.Clear();
            //PrintText(streets,b);
            canvasObj.Children.Clear();
            PrintGrid();

            PlayerTest();

            foreach (Grid g in grids)
            {
                canvasObj.Children.Add(g);
            }
            /*
            foreach (TextBlock t in textBlocks)
            {
                canvasObj.Children.Add(t);
            }
            */
        }

        private void PlayerTest()
        {
            Rectangle r = new Rectangle();

            r.Fill = Brushes.Chocolate;

            r.Width = 15;
            r.Height = 15;
            Grid.SetRow(r, 3);
            Grid.SetColumn(r, 0);

            grids[15].Children.Add(r);
        }

        private void AddGrid(double x, double y, Color color, Color bg)
        {
            Grid g = new Grid();

            g.Height = (double)windowHeight / 10;
            g.Width = (double)windowWidth / 10;

            g.Background = new SolidColorBrush(bg);
            g.ShowGridLines = true;
            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            g.ColumnDefinitions.Add(colDef1);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            g.RowDefinitions.Add(rowDef1);
            g.RowDefinitions.Add(rowDef2);
            g.RowDefinitions.Add(rowDef3);

            Rectangle r = new Rectangle();

            r.Fill = Brushes.SkyBlue;
            Grid.SetRow(r, 0);
            Grid.SetColumn(r, 0);

            TextBlock txt1 = new TextBlock();
            txt1.Text = "asdf";
            txt1.FontSize = 14;
            txt1.FontWeight = FontWeights.Bold;
            Grid.SetRow(txt1, 1);
            Grid.SetColumn(txt1, 0);

            g.Children.Add(r);
            g.Children.Add(txt1);

            Canvas.SetLeft(g, x);
            Canvas.SetTop(g, y);

            grids.Add(g);
        }

        private void AddVerticalGrid(double x, double y, Color color, Color bg)
        {
            Grid g = new Grid();

            g.Height = (double)windowHeight / 10;
            g.Width = (double)windowWidth / 10;

            g.Background = new SolidColorBrush(bg);
            g.ShowGridLines = true;
            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();
            g.ColumnDefinitions.Add(colDef1);
            g.ColumnDefinitions.Add(colDef2);
            g.ColumnDefinitions.Add(colDef3);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            g.RowDefinitions.Add(rowDef1);

            Rectangle r = new Rectangle();

            r.Fill = Brushes.SkyBlue;
            Grid.SetRow(r, 0);
            Grid.SetColumn(r, 0);

            TextBlock txt1 = new TextBlock();
            txt1.Text = "asdf";
            txt1.FontSize = 14;
            txt1.FontWeight = FontWeights.Bold;
            Grid.SetRow(txt1,0);
            Grid.SetColumn(txt1, 1);

            g.Children.Add(r);
            g.Children.Add(txt1);

            Canvas.SetLeft(g, x);
            Canvas.SetTop(g, y);

            grids.Add(g);
        }

        private void Text(double x, double y, string text, Color color, Color bg)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            textBlock.Background = new SolidColorBrush(bg);
            textBlock.Height = (double)windowHeight/10;
            textBlock.Width = (double)windowWidth /10;
            textBlock.TextAlignment = TextAlignment.Center;

            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);

            textBlocks.Add(textBlock);
        }
        private void PrintGrid()
        {
            int j = 1;
            int height = 0;
            int width = 0;
            double temph = 0;
            double tempw = 0;
            for (int i = 0; i < 36; i++)
            {
                if (i < 10)
                {
                    temph = (windowHeight - ((windowHeight / 10) * j));
                    height = (int)Math.Round(temph, 0);
                    AddGrid(0, height, b, bg);
                    j++;
                    if (j == 11)
                    {
                        j = 1;
                    }
                }
                else if (i < 19)
                {
                    tempw = ((windowWidth / 10 * j));
                    width = (int)Math.Round(tempw, 0);
                    AddGrid(width, 0, b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 1;
                    }
                }
                else if (i < 28)
                {
                    tempw = (windowWidth * 0.9);
                    width = (int)Math.Round(tempw, 0);
                    temph = ((windowHeight / 10) * j);
                    height = (int)Math.Round(temph, 0);
                    AddVerticalGrid(tempw, temph, b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 2;
                    }
                }
                else
                {
                    tempw = (windowWidth - windowWidth / 10 * j);
                    width = (int)Math.Round(tempw, 0);
                    temph = (windowHeight * 0.9);
                    height = (int)Math.Round(temph, 0);
                    AddGrid(width, height, b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 1;
                    }
                }
            }
        }
        private void PrintText(List<string> streets, Color b)
        {
            int j = 1;
            int height = 0;
            int width = 0;
            double temph = 0;
            double tempw = 0;
            for (int i = 0; i < 36; i++)
            {
                if (i<10)
                {
                    temph = (windowHeight - ((windowHeight / 10) * j));
                    height = (int)Math.Round(temph,0);
                    Text(0, height, streets[i], b, bg);
                    j++;
                    if (j == 11)
                    {
                        j = 1;
                    }
                }
                else if (i<19)
                {
                    tempw = ((windowWidth / 10 * j));
                    width = (int)Math.Round(tempw, 0);
                    Text(width, 0, streets[i], b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 1;
                    }
                }
                else if (i<28)
                {
                    tempw = (windowWidth * 0.9);
                    width = (int)Math.Round(tempw, 0);
                    temph = ((windowHeight / 10) * j);
                    height = (int)Math.Round(temph, 0);
                    Text(tempw, temph, streets[i], b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 2;
                    }
                }
                else
                {
                    tempw = (windowWidth - windowWidth / 10 * j);
                    width = (int)Math.Round(tempw, 0);
                    temph = (windowHeight * 0.9);
                    height = (int)Math.Round(temph, 0);
                    Text(width, height, streets[i], b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 1;
                    }
                }
            }
        }
    }
}