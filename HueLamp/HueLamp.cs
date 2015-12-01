using System;
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
        public Boolean OnLamp;
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
            this.OnLamp = Boolean.Parse(OnLamp);
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

        public void SetPower(Boolean state)
        {
            if(OnLamp != state)
            {
                OnLamp = state;
                hh.sendCommando(id, "{\"on\":"+state+"}");
            }
        }
        
        public void setRGBValue(float r, float g, float b)
        {
            double[] normalizedToOne = new double[3];
            normalizedToOne[0] = (r / 255);
            normalizedToOne[1] = (g / 255);
            normalizedToOne[2] = (b / 255);
            float red, green, blue;

            if (normalizedToOne[0] > 0.04045)
            {
                red = (float)Math.Pow(
                        (normalizedToOne[0] + 0.055) / (1.0 + 0.055), 2.4);
            }
            else
            {
                red = (float)(normalizedToOne[0] / 12.92);
            }

            if (normalizedToOne[1] > 0.04045)
            {
                green = (float)Math.Pow((normalizedToOne[1] + 0.055)
                        / (1.0 + 0.055), 2.4);
            }
            else
            {
                green = (float)(normalizedToOne[1] / 12.92);
            }

            if (normalizedToOne[2] > 0.04045)
            {
                blue = (float)Math.Pow((normalizedToOne[2] + 0.055)
                        / (1.0 + 0.055), 2.4);
            }
            else
            {
                blue = (float)(normalizedToOne[2] / 12.92);
            }

            float X = (float)(red * 0.649926 + green * 0.103455 + blue * 0.197109);
            float Y = (float)(red * 0.234327 + green * 0.743075 + blue * 0.022598);
            float Z = (float)(red * 0.0000000 + green * 0.053077 + blue * 1.035763);

            float x = X / (X + Y + Z);
            float y = Y / (X + Y + Z);

            hh.sendCommando(id, "{ \"xy\":[" + x + ", " + y + "]}");
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
