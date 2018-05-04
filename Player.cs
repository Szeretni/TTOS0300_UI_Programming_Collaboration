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
        protected virtual void Changed(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int dieResult;
        
        public string name;
        public bool InJail = false;
        public int JailTime = 0;
        public int position = 0;
        private int cash = 0;
        private bool rentPaid = false;

        public bool DieRolled { get; set; }
        public int Id { get; set; }

        public bool RentPaid
        {
            get
            {
                return rentPaid;
            }
            set
            {
                rentPaid = value;
                BLLayer.SetRentPaidToMySQL(Id, rentPaid);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                    // Call Changed whenever the property is updated
                    Changed("name");
                }
            }
        }

        public int Cash
        {
            get
            {
                return cash;
            }

            set
            {
                if (cash != value)
                {
                    //updates cash and db
                    cash = value;
                    BLLayer.SetPlayerCashToMySQL(Id, cash);
                    // Call Changed whenever the property is updated
                    Changed("cash");
                }
            }
        }

        public int Position
        {
            get
            {
                return position; 
            }

            set
            {
                if (position != value)
                {
                    //updates position and db
                    position = value;
                    BLLayer.SetPlayerPositionToMySQL(Id, position);
                    // Call Changed whenever the property is updated
                    Changed("position");
                }
            }
        }

        public int DieResult
        {
            get
            {
                return dieResult;
            }

            set
            {
                if (dieResult != value)
                {
                    dieResult = value;
                    // Call Changed whenever the property is updated
                    Changed("dieResult");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}