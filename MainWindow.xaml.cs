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
        public static int windowWidth = 0;
        public static int windowHeight = 0;
        List<string> streets = new List<string>();
        Color b = Color.FromRgb(0, 0, 0);
        Color bg = Color.FromRgb(10, 255, 10);

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
            windowWidth = (int)client.ActualWidth;
            windowHeight = (int)client.ActualHeight;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // get our game window area
            FrameworkElement client = this.Content as FrameworkElement;
            windowWidth = (int)client.ActualWidth;
            windowHeight = (int)client.ActualHeight;
            canvasObj.Children.Clear();
            PrintText(streets,b);
        }

        private void Text(double x, double y, string text, Color color, Color bg)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            textBlock.Background = new SolidColorBrush(bg);
            textBlock.Height = (double)windowHeight/10;
            textBlock.Width = (double)windowWidth /10;
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvasObj.Children.Add(textBlock);
        }

        private void PrintText(List<string> streets, Color b)
        {
            int j = 1;
            double height = 0;
            double width = 0;
            for (int i = 0; i < 36; i++)
            {
                if (i<10)
                {
                    Text(0, windowHeight - ((windowHeight / 10)*j), streets[i], b, bg);
                    j++;
                    if (j == 11)
                    {
                        j = 1;
                    }
                }
                else if (i<19)
                {
                    Text((windowWidth /10*j), 0, streets[i], b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 1;
                    }
                }
                else if (i<28)
                {
                    Text((windowWidth *0.9), ((windowHeight / 10) * j), streets[i], b, bg);
                    j++;
                    if (j == 10)
                    {
                        j = 2;
                    }
                }
                else
                {
                    Text((windowWidth - windowWidth / 10 * j), (windowHeight *0.9), streets[i], b, bg);
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