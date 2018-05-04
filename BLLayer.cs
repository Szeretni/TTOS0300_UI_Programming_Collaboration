using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class BLLayer
    {
        // For db methods

        public static List<Player> GetAllPlayersFromDt()
        {
            try
            {
                List<Player> players = new List<Player>();
                DataTable dt = DBLayer.GetPlayersFromMySQL();
                foreach (DataRow dr in dt.Rows)
                {
                    Player player = new Player
                    {
                        //PlayerId,PlayerName
                        Id = int.Parse(dr[0].ToString()),
                        Name = dr[1].ToString()
                    };
                    players.Add(player);
                }
                return players;
            }
            catch
            {
                throw;
            }
        }

        public static List<int> GetGameSessionsFromDt()
        {
            try
            {
                List<int> games = new List<int>();
                DataTable dt = DBLayer.GetGameSessionsFromMySQL();
                foreach (DataRow dr in dt.Rows)
                {
                    //gameSessionId
                    games.Add(int.Parse(dr[0].ToString()));
                }
                return games;
            }
            catch
            {
                throw;
            }
        }

        //current game's players
        public static List<Player> GetGamesPlayersFromDt()
        {
            try
            {
                List<Player> players = new List<Player>();
                DataTable dt = DBLayer.GetGamesPlayersFromMySQL();
                foreach (DataRow dr in dt.Rows)
                {
                    Player player = new Player
                    {
                        //PlayerId,PlayerName
                        Id = int.Parse(dr[0].ToString()),
                        Name = dr[1].ToString()
                    };
                    players.Add(player);
                }
                return players;
            }
            catch
            {
                throw;
            }
        }

        public static List<Cell> GetAllCellsFromDt()
        {
            try
            {
                List<Cell> cells = new List<Cell>();
                DataTable dt = DBLayer.GetCellsFromMySQL();
                //create all cells for board
                foreach (DataRow dr in dt.Rows)
                {
                    Cell cell = new Cell
                    {
                        //CellId,Name,Rent,Price,SerieId,CellTypeId
                        Id = int.Parse(dr[0].ToString()) - 1, //cellId=1 is position[0], therefore -1
                        Name = dr[1].ToString(),
                        Rent = int.Parse(dr[2].ToString()),
                        Price = int.Parse(dr[3].ToString()),
                        SerieId = int.Parse(dr[4].ToString()),
                        CellTypeId = int.Parse(dr[5].ToString()),
                    };
                    cells.Add(cell);
                }

                //add owner information to cells, if applicable,  by gameid
                dt = DBLayer.GetCellOwnerFromMySQL();
                foreach (DataRow dr in dt.Rows)
                {
                    //PlayerId,CellId
                    int pid = int.Parse(dr[0].ToString());
                    int cid = int.Parse(dr[1].ToString());
                    cells[cid - 1].Owner = pid;
                }                             
            return cells;
            }
            catch
            {
                throw;
            }
        }

        public static int GetPlayerPositionFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = DBLayer.GetPlayerPositionFromMySQL(playerid);
                int position = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    position = int.Parse(dr[0].ToString())-1; //cellId=1 is position[0], therefore -1
                }
                return position;
            }
            catch
            {
                throw;
            }
        }

        public static void SetPlayerPositionToMySQL(int playerid, int position)
        {
            try
            {
                DBLayer.SetPlayerPositionToMySQL(playerid, position+1); //position[0] is cellId=1, therefore +1
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static int GetPlayerCashFromMySQL(int playerId)
        {
            try
            {
                DataTable dt = DBLayer.GetPlayerCashFromMySQL(playerId);
                int cash = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    cash = int.Parse(dr[0].ToString());
                }
                return cash;
            }
            catch
            {
                throw;
            }
        }

        public static void SetPlayerCashToMySQL(int playerid, int cash)
        {
            try
            {
                DBLayer.SetPlayerCashToMySQL(playerid, cash);
            }
            catch
            {
                throw;
            }
        }

        public static int GetCurrentPlayerIdFromMySQL()
        {
            try
            {
                DataTable dt = DBLayer.GetCurrentPlayerIdFromMySQL();
                int pid = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //playerId
                    pid = int.Parse(dr[0].ToString());
                }
                return pid;
            }
            catch
            {
                throw;
            }
        }

        //set current player id to db
        public static void SetCurrentPlayerIdToMySQL(int playerid)
        {
            try
            {
                DBLayer.SetCurrentPlayerIdToMySQL(playerid);
            }
            catch
            {
                throw;
            }
        }

        //this is used for new games (needs new gameid)
        public static void SetCurrentPlayerIdToMySQL(int playerid, int gamesessionid)
        {
            try
            {
                DBLayer.SetCurrentPlayerIdToMySQL(playerid, gamesessionid);
            }
            catch
            {
                throw;
            }
        }

        //gets players die rolled status from db
        public static bool GetDieRolledFlagFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = DBLayer.GetPlayersDieRolledFlagFromMySQL(playerid);
                bool rolled = false;
                foreach (DataRow dr in dt.Rows)
                {
                    rolled = bool.Parse(dr[0].ToString());
                }
                return rolled;
            }
            catch
            {
                throw;
            }
        }

        //sets player's die rolled status to db
        public static void SetDieRolledFlagToMySQL(int playerid, bool dieRolled)
        {
            try
            {
                DBLayer.SetPlayerDieRolledFlagToMySQL(playerid, dieRolled);
            }
            catch
            {
                throw;
            }
        }

        //gets player's rent paid status
        public static bool GetPlayerRentPaidFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = DBLayer.GetPlayerRentPaidFromMySQL(playerid);
                bool rolled = false;
                foreach (DataRow dr in dt.Rows)
                {
                    rolled = bool.Parse(dr[0].ToString());
                }
                return rolled;
            }
            catch
            {
                throw;
            }
        }

        //sets player's rent paid status
        public static void SetRentPaidToMySQL(int playerid, bool dieRolled)
        {
            try
            {
                DBLayer.SetRentPaidToMySQL(playerid, dieRolled);
            }
            catch
            {
                throw;
            }
        }

        public static void SetCellBuildingCountsToMySQL(int cellid, int hotelcount, int housecount)
        {
            try
            {
                DBLayer.SetCellBuildingCountsToMySQL(cellid, hotelcount, housecount);
            }
            catch
            {
                throw;
            }
        }

        public static void SetCellOwnerToMySQL(int playerid, int cellid)
        {
            try
            {
                DBLayer.SetCellOwnerToMySQL(playerid, cellid+1); //position[0] is cellId=1, therefore +1
            }
            catch
            {
                throw;
            }
        }

        //gets gameid from db
        public static List<int> GetGameIdsFromMySQL()
        {
            try
            {
                DataTable dt = DBLayer.GetGameIdsFromMySQL();
                List<int> ids = new List<int>();
                foreach (DataRow dr in dt.Rows)
                {
                    ids.Add(int.Parse(dr[0].ToString()));
                }
                return ids;
            }
            catch
            {
                throw;
            }
        }

        public static List<Player> GetPlayerIdsFromMySQL()
        {
            try
            {
                List<Player> players = new List<Player>();
                DataTable dt = DBLayer.GetPlayerIdsFromMySQL();

                foreach (DataRow dr in dt.Rows)
                {
                    Player player = new Player
                    {
                        //PlayerId,PlayerName
                        Id = int.Parse(dr[0].ToString()),
                        Name = dr[1].ToString()
                    };
                    players.Add(player);
                }
                return players;
            }
            catch
            {
                throw;
            }
        }

        //set new game id to db
        public static void SetNewGameIdToMySQL(int gamesessionid, int playerid)
        {
            try
            {
                DBLayer.SetNewGameIdToMySQL(gamesessionid, playerid);
            }
            catch
            {
                throw;
            }
        }

        public static void SetPlayerToNewGameToMySQL(int newPlayerId, int gameSessionId)
        {
            try
            {
                DBLayer.SetPlayerToNewGameToMySQL(gameSessionId,newPlayerId);
            }
            catch
            {
                throw;
            }
        }
    }
}