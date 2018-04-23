using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class Player : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cash
        {
            get
            {
                return cash;
            }
            set
            {
                if ((cash + value) >= 0)
                {
                    cash += value;
                    BLLayer.SetPlayerCashToMySQL(Id, cash);
                }
            }
        }
        public int Position;
        public bool DieRolled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int PlayerPosition
        {
            get { return Position; }
            set
            {
                Position = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Position");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private int cash = 0;
    }
}
