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
                        //PlayerId,PlayerName,SUM(Value) as TotalCash
                        Id = int.Parse(dr[0].ToString()),
                        Name = dr[1].ToString(),
                        Cash = int.Parse(dr[2].ToString()),
                        Position = int.Parse(dr[3].ToString())
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
                    Cell cell = new Cell
                    {
                        //CellId,Name,Rent,Price,SerieId,CellTypeId
                        Id = int.Parse(dr[0].ToString()),
                        Name = dr[1].ToString(),
                        Rent = int.Parse(dr[2].ToString()),
                        Price = int.Parse(dr[3].ToString()),
                        SerieId = int.Parse(dr[4].ToString()),
                        CellTypeId = int.Parse(dr[5].ToString())
                    };
                    cells.Add(cell);
                }
                return cells;
            }
            catch
            {
                throw;
            }
        }

        //20180422
        public static int GetPlayerPositionFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = DBLayer.GetPlayerPositionFromMySQL(playerid);
                int position = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    position = int.Parse(dr[0].ToString());
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
                DBLayer.SetPlayerPositionToMySQL(playerid, position);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //20180422
        public static int GetPlayerCashFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = DBLayer.GetPlayerCashFromMySQL(playerid);
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