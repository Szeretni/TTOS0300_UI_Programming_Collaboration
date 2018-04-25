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
                //update next player to db
                BLLayer.SetCurrentPlayerIdToMySQL(players[currentPlayer].Id);
            }
            catch
            {
                throw;
            }
        }

        //20180425 HO
        //generate new game id
        //insted of loop get last
        static public int NewGameId()
        {
            try
            {
                //get game id's from db
                List<int> gameIds = BLLayer.GetGameIdsFromMySQL();
                //generates new gameid
                int i = 1;
                foreach (int id in gameIds)
                {
                    if (id == i)
                    {
                        i++;
                    }
                }
                //while (true)
                //{
                //    if ((i != (gameIds[0]-1)))
                //    {
                //        break;
                //    }
                //    else
                //    {
                //        i++;
                //    }
                //}
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //20180425 HO
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

        //20180425
        //create new game to db
        static public void NewGame(NewGame newGame)
        {
            try
            {
                //new gamesessionid to db
                BLLayer.SetNewGameIdToMySQL(newGame.GameId);
                //players to gamesession
                foreach (Player p in newGame.NewPlayers)
                {
                    BLLayer.SetPlayerToNewGameToMySQL(p.Id, newGame.GameId);
                }
                //set first player as current player
                BLLayer.DynamicSetCurrentPlayerIdToMySQL(newGame.NewPlayers.First().Id, newGame.GameId);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
