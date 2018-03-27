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
        public MainWindow()
        {
            InitializeComponent();

            Color b = Color.FromRgb(0, 0, 0);

            List<string> streets = new List<string>();

            double width = 520;
            double asdf = 100;
            for (int i = 0;i<36;i++)
            {
                streets.Add(i.ToString());
            }

            double widthpercent = width/100*10;
            string widthth = widthpercent.ToString(); ;

            for (int i = 0, j = -65;i< 10;i++, j+=65)
            {
                if (i == 0)
                {
                    Text(100/2, 50, streets[i], b);
                }
                else if (i == 1)
                {
                    Text(100 + 65 / 2, 50, streets[i], b);
                }
                else if (i == 9)
                {
                    Text(720-50, 50, streets[i], b);
                }
                else
                {
                    Text(100 + (65/2+j), 50, streets[i], b);
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
            /*
            for (int i = 0, j = 10; i < 36; i++, j += 10)
            {
                Text(i + j, i + j, "asdf", b);
            }

            for (int i = 0, j = 10; i < 36; i++, j += 10)
            {
                Text(i + j, i + j, "asdf", b);
            }

            for (int i = 0, j = 10; i < 36; i++, j += 10)
            {
                Text(i + j, i + j, "asdf", b);
            }
            */
            //PushEffectExample pee = new PushEffectExample();
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
        }

        private void CreateText(DrawingContext drawingContext)
        {
            string testString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor";

            // Create the initial formatted text string.
            FormattedText formattedText = new FormattedText(
                testString,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                32,
                Brushes.Black);

            // Set a maximum width and height. If the text overflows these values, an ellipsis "..." appears.
            formattedText.MaxTextWidth = 300;
            formattedText.MaxTextHeight = 240;

            // Use a larger font size beginning at the first (zero-based) character and continuing for 5 characters.
            // The font size is calculated in terms of points -- not as device-independent pixels.
            formattedText.SetFontSize(36 * (96.0 / 72.0), 0, 5);

            // Use a Bold font weight beginning at the 6th character and continuing for 11 characters.
            formattedText.SetFontWeight(FontWeights.Bold, 6, 11);

            // Use a linear gradient brush beginning at the 6th character and continuing for 11 characters.
            formattedText.SetForegroundBrush(
                                    new LinearGradientBrush(
                                    Colors.Orange,
                                    Colors.Teal,
                                    90.0),
                                    6, 11);

            // Use an Italic font style beginning at the 28th character and continuing for 28 characters.
            formattedText.SetFontStyle(FontStyles.Italic, 28, 28);

            // Draw the formatted text string to the DrawingContext of the control.
            //drawingContext.DrawText(formattedText, new Point(0, 0));

            drawingContext.DrawText(formattedText, new Point(0, 0));

            drawingContext.Close();
        }

        private void Text(double x, double y, string text, Color color)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;

            textBlock.Foreground = new SolidColorBrush(color);

            Canvas.SetLeft(textBlock, x);

            Canvas.SetTop(textBlock, y);
            
            gridObj.Children.Add(textBlock);

        }
    }
}
