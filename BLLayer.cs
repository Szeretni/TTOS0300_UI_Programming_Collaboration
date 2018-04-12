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