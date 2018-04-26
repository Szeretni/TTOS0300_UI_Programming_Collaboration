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
        // !!!
        // Only Db methods
        // !!!
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
                        //PlayerId,PlayerName,SUM(Value) as TotalCash 20180425T2000
                        //20180425T2000
                        //PlayerId,PlayerName
                        Id = int.Parse(dr[0].ToString()),
                        Name = dr[1].ToString()
                        //Cash = int.Parse(dr[2].ToString()), 20180425T2000
                        //Position = int.Parse(dr[3].ToString()) 20180425T2000
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
                foreach (DataRow dr in dt.Rows)
                {
                    int i = 0;
                    bool ownerNotNull = int.TryParse(dr[6].ToString(), out i);
                    Cell cell = new Cell
                    {
                        //CellId,Name,Rent,Price,SerieId,CellTypeId
                        Id = int.Parse(dr[0].ToString())-1, //20180604position
                        Name = dr[1].ToString(),
                        Rent = int.Parse(dr[2].ToString()),
                        Price = int.Parse(dr[3].ToString()),
                        SerieId = int.Parse(dr[4].ToString()),
                        CellTypeId = int.Parse(dr[5].ToString()),
                    };
                    if (i != 0)
                    {
                        cell.Owner = i;
                    }
                    cells.Add(cell);
                }
                return cells;
            }
            catch
            {
                throw;
            }
        }

        //20180426 HO dynamic gamesessionid
        //20180422
        public static int GetPlayerPositionFromMySQL(int playerid, int gameSessionId)
        {
            try
            {
                DataTable dt = DBLayer.GetPlayerPositionFromMySQL(playerid,gameSessionId);
                int position = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    position = int.Parse(dr[0].ToString())-1; //20180604position -1 bc return cellid 1-36, should draw cell[0-35]
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
                DBLayer.SetPlayerPositionToMySQL(playerid, position+1,Properties.Settings.Default.settingsCurrentGameId); //20180604position +1 cell[0-35] => cellid 1-35
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        //20180425
        public static int DynamicGetPlayerCashFromMySQL(int playerId,int gameSessionId)
        {
            try
            {
                DataTable dt = DBLayer.DynamicGetPlayerCashFromMySQL(playerId,gameSessionId);
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

        //20180423 HO
        //public static void SetPlayerCashToMySQL(int playerid, int cash)
        //{
        //    try
        //    {
        //        DBLayer.SetPlayerCashToMySQL(playerid, cash);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //20180426 HO
        public static void DynamicSetPlayerCashToMySQL(int playerid, int cash, int gameSessionId)
        {
            try
            {
                DBLayer.DynamicSetPlayerCashToMySQL(playerid, cash,gameSessionId);
            }
            catch
            {
                throw;
            }
        }

        //20180423 HO
        //gets current player id from db
        public static int GetCurrentPlayerIdFromMySQL()
        {
            try
            {
                DataTable dt = DBLayer.GetCurrentPlayerIdFromMySQL();
                int pid = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    pid = int.Parse(dr[0].ToString());
                }
                return pid;
            }
            catch
            {
                throw;
            }
        }

        //20180423 HO
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

        //20180423 HO
        //set current player id to db
        public static void DynamicSetCurrentPlayerIdToMySQL(int playerid, int gamesessionid)
        {
            try
            {
                DBLayer.DynamicSetCurrentPlayerIdToMySQL(playerid, gamesessionid);
            }
            catch
            {
                throw;
            }
        }

        //20180423 HO
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

        //20180423 HO
        //set players die rolled status to db
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

        //20180426rent 
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

        //20180426rent
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
                DBLayer.SetCellOwnerToMySQL(playerid, cellid+1); //20180604position +1 bc cell[x] = cellid x+1
            }
            catch
            {
                throw;
            }
        }

        //20180425 HO
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

        //20180425 HO
        //set new game id to db
        public static void SetNewGameIdToMySQL(int gamesessionid)
        {
            try
            {
                DBLayer.SetNewGameIdToMySQL(gamesessionid);
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

        //20180426 HO INPROGRESS is this necessary?
        //public static List<Cell> GetCellOwnerFromMySQL()
        //{
        //    try
        //    {
        //        List<Player> players = new List<Player>();
        //        DataTable dt = DBLayer.GetCellOwnerFromMySQL();

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            Cell cell = new Cell
        //            {
        //                //PlayerId,CellId
        //                Id = int.Parse(dr[0].ToString()),
        //                Name = int.Parse(dr[1].ToString())
        //            };
        //            players.Add(player);
        //        }
        //        return players;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //obsoleted 20180426
        //20180422
        //public static int GetPlayerCashFromMySQL(int playerid)
        //{
        //    try
        //    {
        //        DataTable dt = DBLayer.GetPlayerCashFromMySQL(playerid);
        //        int cash = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            cash = int.Parse(dr[0].ToString());
        //        }
        //        return cash;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        /* list-style obsolete
        public static List<Player> GetPlayerList()
        {
            try
            {
                List<Player> players = new List<Player>();
                players = DBLayer.GetPlayerListsFromMySQL();
                return players;
            }
            catch
            {
                throw;
            }
        }
        */
    }
}