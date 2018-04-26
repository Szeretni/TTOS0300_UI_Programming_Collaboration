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

        public int Id { get; set; }
        public string name;
        private int cash = 0;
        private bool rentPaid = false; //20180426rent
        public int position = 0;
        public bool DieRolled { get; set; }
        public bool RentPaid //20180426rent
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
        public int dieResult;

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
                    //updates cash and db 20180426
                    cash = value;
                    BLLayer.DynamicSetPlayerCashToMySQL(Id, cash, Properties.Settings.Default.settingsCurrentGameId);
                    // Call Changed whenever the property is updated
                    Changed("cash");
                }
            }
        }

        public int Position
        {
            get
            {
                //cellid1 = cell[0]
                return position; 
            }

            set
            {
                if (position != value)
                {
                    position = value; //20180426T1300
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
