using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class Player : IGameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cash { get; set; }
        public int Position { get; set; }
        public bool DieRolled { get; set; }
    }
}
