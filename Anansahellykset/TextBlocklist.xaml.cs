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
        List<TextBlock> textBlocks = new List<TextBlock>();

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

            textBlocks[1].Text = "asdf";

            foreach (TextBlock t in textBlocks)
            {
                canvasObj.Children.Add(t);
            }
        }

        private void Text(double x, double y, string text, Color color, Color bg)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;

            textBlock.Foreground = new SolidColorBrush(color);
            textBlock.Background = new SolidColorBrush(bg);

            textBlock.Height = windowHeight / 10;
            textBlock.Width = windowWidth / 10;

            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);

            textBlocks.Add(textBlock);
        }

        private void PrintText(List<string> streets, Color b)
        {
            for (int i = 0; i < 10; i++)
            {
                Text((windowWidth / 10) * (i), 0, streets[i], b, bg);
            }
            for (int i = 0; i < 10; i++)
            {
                Text(0, (windowHeight / 10) * (i), streets[i], b, bg);
            }
        }
    }
}