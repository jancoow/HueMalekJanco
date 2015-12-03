﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    class HueGroup
    {
        public List<HueLamp> lamps;
        private HueHandler hh;
        public string id;
        public Boolean OnLamp { get; set; }
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
        public string name { get; set; }

        public HueGroup(HueHandler hh, String id, String OnLamp, String BrightnesLamp, String ColorLamp, String Sat, String x, String y, String CT, String alert, String effect, String ColorMode, String Reachable, String name, List<HueLamp> lamps)
        {
            this.hh = hh;
            this.id = id;
            this.OnLamp = Boolean.Parse(OnLamp);
            this.BrightnesLamp = Int32.Parse(BrightnesLamp);
            this.ColorLamp = Int32.Parse(ColorLamp);
            this.Sat = Int32.Parse(Sat);
            this.x = x;
            this.y = y;
            this.CT = CT;
            this.alert = alert;
            this.effect = effect;
            this.colormode = ColorMode;
            this.reachable = Reachable;
            this.name = name;
            this.lamps = lamps;
        }

        public void SetPower(Boolean state)
        {
            if (OnLamp != state)
            {
                OnLamp = state;
                hh.sendGroupCommando(id, "{\"on\":" + state + "}");
            }
        }
        public void setRGBValue(float r, float g, float b)
        {
            double h, s, v;
            ColorUtil.RGBtoHSV(r, g, b, out h, out s, out v);
            SetHSLValue((int)((h / 360.0f) * 65535.0f), (int)s * 254, (int)v - 1);
        }

        public void getRGBValue(out double r, out double g, out double b)
        {
            ColorUtil.RGBtoHSV(ColorLamp, Sat, BrightnesLamp, out r, out g, out b);
        }

        public void SetHSLValue(int h, int s, int b)
        {
            dynamic jsonObject = new JObject();
            if (h != ColorLamp)
            {
                ColorLamp = h;
                jsonObject.hue = ColorLamp;
            }
            if (s != Sat)
            {
                Sat = s;
                jsonObject.sat = Sat;
            }
            if (b != BrightnesLamp)
            {
                BrightnesLamp = b;
                jsonObject.bri = BrightnesLamp;
            }
            String json = ((object)jsonObject).ToString();
            hh.sendGroupCommando(id, json);
        }
    }
}
