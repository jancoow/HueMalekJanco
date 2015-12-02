using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    class MainViewModel
    {
        HueHandler h;

        public MainViewModel(HueHandler h)
        {
            this.h = h;
        
        }
        public ObservableCollection<HueGroup> Groups
        {
            get { return h.groups; }
        }

        public ObservableCollection<HueLamp> Items
        {
            get { return h.lamps; }
        }
    }
}