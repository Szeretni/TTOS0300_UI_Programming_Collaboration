using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class BLMethod
    {
        // When time allows, refactor code to here in order to avoid cluttering BLLayer.cs and MainWindows.xaml.cs

        //next turn logic
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
                //reset rent paid for next turn
                players[currentPlayer].RentPaid = false;
                //update next player to db
                BLLayer.SetCurrentPlayerIdToMySQL(players[currentPlayer].Id);
            }
            catch
            {
                throw;
            }
        }

        //generate new game id
        static public int NewGameId()
        {
            try
            {
                //get game id's from db
                List<int> gameIds = BLLayer.GetGameIdsFromMySQL();
                //generates new gameid
                int i = gameIds.Last()+1;
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //show players from db
        static public List<Player> ShowPlayers()
        {
            try
            {
                //get players id,name from db
                List<Player> players = BLLayer.GetPlayerIdsFromMySQL();
                return players;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //create new game to db
        static public void NewGame(NewGame newGame, int playerid)
        {
            try
            {
                //new gamesessionid to db
                BLLayer.SetNewGameIdToMySQL(newGame.GameId, playerid);
                //players to gamesession
                foreach (Player p in newGame.NewPlayers)
                {
                    BLLayer.SetPlayerToNewGameToMySQL(p.Id, newGame.GameId);
                }
                //sets first player as current player to new game (needs new gameid)
                BLLayer.SetCurrentPlayerIdToMySQL(newGame.NewPlayers.First().Id, newGame.GameId);
                //sets new game id to local program instance 
                Properties.Settings.Default.settingsCurrentGameId = newGame.GameId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
