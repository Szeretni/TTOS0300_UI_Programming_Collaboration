using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static void MoveTo(this Image img, double newX, double newY)
        {
            TranslateTransform trans = new TranslateTransform();
            img.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(newY, TimeSpan.FromSeconds(3));
            DoubleAnimation anim2 = new DoubleAnimation(newX, TimeSpan.FromSeconds(3));
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
        }
    }
}
