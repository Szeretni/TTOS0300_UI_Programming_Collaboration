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
        public int cash;
        public int position;
        public bool DieRolled { get; set; }

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
                    cash = value;
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
                    position = value;
                    // Call Changed whenever the property is updated
                    Changed("position");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
