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
        private string name;
        public int Cash { get; set; }
        public int Position { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
