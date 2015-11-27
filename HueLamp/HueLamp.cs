using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    class HueLamp
    {
        public string id;
        public string OnLamp;
        public string BrightnesLamp;
        public string ColorLamp;
        public string Sat;
        public string x;
        public string y;
        public string CT;
        public string alert;
        public string effect;
        public string colormode;
        public string reachable;
        public string type;
        public string name;
        public string modelid;
        public string swversion;

        public HueLamp(HueHandler hh, string id, string OnLamp, string BrightnesLamp, string ColorLamp, string Sat, string x, string y,string CT,
            string alert, string effect, string colormode, string reachable, string type, string name, string modelid, string swversion)
        {
            this.id = id;
            this.OnLamp = OnLamp;
            this.BrightnesLamp = BrightnesLamp;
            this.ColorLamp = ColorLamp;
            this.Sat = Sat;
            this.x = x;
            this.y = y;
            this.CT = CT;
            this.alert = alert;
            this.effect = effect;
            this.colormode = colormode;
            this.reachable = reachable;
            this.type = type;
            this.name = name;
            this.modelid = modelid;
            this.swversion = swversion;

        }
    }

}
