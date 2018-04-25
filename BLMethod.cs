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
                BLLayer.SetDieRolledFlagToMySQL(players[currentPlayer].Id, players[currentPlayer].DieRolled);
            }
            catch
            {
                throw;
            }
        }

        //20180425 HO
        //generate new game id
        static public int NewGameId()
        {
            //get game id's from db
            List<int> gameIds = BLLayer.GetGameIdsFromMySQL();
            //generates new gameid
            int i = 0;
            while (true)
            {
                if ((i != gameIds[i]))
                {
                    break;
                }
                else
                {
                    i++;
                }
            }
            return i;
        }

        //20180425 HO
        //show players from db
        static public List<Player> ShowPlayers()
        {
            //get players id,name from db
            List<Player> players = BLLayer.GetPlayerIdsFromMySQL();
            return players;
        }
    }
}
