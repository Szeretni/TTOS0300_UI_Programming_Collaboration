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
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int Id { get; set; }
        public string Name;
        public int Cash { get; set; }
        public int Position { get; set; }
        public bool DieRolled { get; set; }

        public string PlayerName
        {
            get
            {
                return Name;
            }

            set
            {
                if (Name != value)
                {
                    Name = value;
                    // Call Changed whenever the property is updated
                    Changed("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
