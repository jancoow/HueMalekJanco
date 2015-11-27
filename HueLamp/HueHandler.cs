using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    class HueHandler
    {
        NetworkHandler nw;
        List<HueLamp> lamps;

        public HueHandler()
        {
            nw = new NetworkHandler("localhost", "8000");
            lamps = new List<HueLamp>();
            InitLights();
        }

        public void  createUser(string name)
        {
            nw.PostCommand("api", "  {  \"devicetype\":  \" "+name+" \"}  ");
        }

        public async void InitLights()
        {
            String jsonreturn = await nw.GetCommand("api/13aa97ff29f9520838b64e0258a45e8/lights");
            JArray array = JArray.Parse("[" + jsonreturn + "]");
            for (int i = 1; i <= array[0].Count(); i++)
            {
                var citem = array[0][i.ToString()];
                lamps.Add(new HueLamp(
                    this,
                    i.ToString(),
                    citem["state"]["on"].ToString(),
                    citem["state"]["bri"].ToString(),
                    citem["state"]["hue"].ToString(),
                    citem["state"]["sat"].ToString(),
                    citem["state"]["xy"][0].ToString(),
                    citem["state"]["xy"][1].ToString(),
                    citem["state"]["ct"].ToString(),
                    citem["state"]["alert"].ToString(),
                    citem["state"]["effect"].ToString(),
                    citem["state"]["colormode"].ToString(),
                    citem["state"]["reachable"].ToString(),
                    citem["type"].ToString(),
                    citem["name"].ToString(),
                    citem["modelid"].ToString(),
                    citem["swversion"].ToString()
                    ));
            }
        }

        public void setLightPower(int lightid, Boolean state)
        {
            nw.PutCommand("api/13aa97ff29f9520838b64e0258a45e8/lights/"+lightid+"/state", "{ \"on\": \""+state+"\"}");
        }
    }
}
