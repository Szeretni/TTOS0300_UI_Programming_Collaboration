using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class BLMethod
    {
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
                //die rolled, cannot roll again
                players[currentPlayer].DieRolled = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
