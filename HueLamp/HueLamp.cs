﻿using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    class HueLamp
    {
        private HueHandler hh;
        public string id;
        public string OnLamp;
        public int BrightnesLamp;
        public int ColorLamp;
        public int Sat;
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
            this.hh = hh;
            this.id = id;
            this.OnLamp = OnLamp;
            this.BrightnesLamp = Int32.Parse(BrightnesLamp);
            this.ColorLamp = Int32.Parse(ColorLamp);
            this.Sat = Int32.Parse(Sat);
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

        public void SetHSBValue(int h, int s, int b)
        {
            dynamic jsonObject = new JObject();
            if(h != ColorLamp)
            {
                ColorLamp = h;
                jsonObject.hue = ColorLamp;
            }
            if(s != Sat)
            {
                Sat = s;
                jsonObject.sat = Sat;
            }
            if(b != BrightnesLamp)
            {
                BrightnesLamp = b;
                jsonObject.bri = BrightnesLamp;
            }
            String json = ((object)jsonObject).ToString();
            hh.sendCommando(id, json);
        }
    }

}
