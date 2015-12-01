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
        private ObservableCollection<HueLamp> _items;
        HueHandler h;

        public MainViewModel(HueHandler h)
        {
            this.h = h;
        
        }

        public ObservableCollection<HueLamp> Items
        {
            get { return h.lamps; }
        }
    }
}