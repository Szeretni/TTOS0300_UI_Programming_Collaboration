using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class BLMethod
    {
        // !!!
        // Founded this class in order to avoid cluttering BLLayer.cs and MainWindows.xaml.cs
        // !!!
        static public void NextTurn(ref int currentPlayer,ref List<Player> players)
        {
            //in case of 0 players
            try
            {
                //loops players
                currentPlayer++;
                if (currentPlayer == players.Count())
                {
                    currentPlayer = 0;
                }
                //reset die roll for next turn
                players[currentPlayer].DieRolled = false;

                // !!!
                // Here must be db update whose turn will be when reloaded
                // !!!
            }
            catch
            {
                throw;
            }
        }
    }
}
