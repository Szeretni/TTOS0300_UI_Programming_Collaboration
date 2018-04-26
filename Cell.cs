using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class Cell : IGameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rent { get; set; }
        public int Price { get; set; }
        public int SerieId { get; set; }
        public int CellTypeId { get; set; }
        public int HouseCount { get; set; }
        public int HotelCount { get; set; }
        public int Owner { get; set; }
    }
}
