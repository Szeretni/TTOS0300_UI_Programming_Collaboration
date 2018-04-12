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