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

        private void Text(double x, double y, string text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvasObj.Children.Add(textBlock);
        }

        private void PrintText(List<string> streets, Color b)
        {
            for (int i = 0; i < 10; i++)
            {
                Text((windowWidth / 10) * (1 + i), windowHeight / 10, streets[i], b);
            }
            for (int i = 0; i < 10; i++)
            {
                Text(windowWidth / 10, (windowHeight / 10) * (1 + i), streets[i], b);
            }
            /*
            for (int i = 0, j = -65; i < 10; i++, j += 65)
            {
                if (i == 0)
                {
                    Text(100 / 2, 50, streets[i], b);
                }
                else if (i == 1)
                {
                    Text(100 + 65 / 2, 50, streets[i], b);
                }
                else if (i == 9)
                {
                    Text(720 - 50, 50, streets[i], b);
                }
                else
                {
                    Text(100 + (65 / 2 + j), 50, streets[i], b);
                }
            }

            for (int i = 0, j = -65; i < 10; i++, j += 65)
            {
                if (i == 0)
                {

                }
                else if (i == 1)
                {
                    Text(50, 100 + 65 / 2, streets[i], b);
                }
                else if (i == 9)
                {

                }
                else
                {
                    Text(50, 100 + (65 / 2 + j), streets[i], b);
                }
            }
            */
        }
    }
}