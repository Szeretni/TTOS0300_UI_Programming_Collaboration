using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TTOS0300_UI_Programming_Collaboration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //https://blogs.msdn.microsoft.com/patrickdanino/2008/07/23/user-settings-in-wpf/
        private void OnExit(object sender, ExitEventArgs e)
        {
            //saves gameid locally, continues the game when program is launched again
            TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.Save();
        }
    }
}
