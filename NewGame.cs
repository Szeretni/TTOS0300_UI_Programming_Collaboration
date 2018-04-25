using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class NewGame
    {
        //this manages new game information
        //used to avoid many db connections
        //only one db connection when committed
        public NewGame()
        {
            
        }
        public int GameId { get; set; }
        public List<Player> NewPlayers { get; set; }
    }
}
