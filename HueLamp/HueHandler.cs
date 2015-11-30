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
        string apikey;

        public HueHandler()
        {
            nw = new NetworkHandler("localhost", "8000");
            lamps = new List<HueLamp>();
            InitLights();
        }

        public async Task<Boolean> createUser(string name)
        {
            JArray array = JArray.Parse(await nw.PostCommand("api", "  {  \"devicetype\":  \" " + name + " \"}  "));
            if(array[0]["error"] != null)
            {
                System.Diagnostics.Debug.WriteLine("Error:" + array[0]["error"]["description"]);
                return false;
                //Hier moet iets met de error gedaan worden
            }else if(array[0]["success"] != null)
            {
                System.Diagnostics.Debug.WriteLine("Username key:" + array[0]["success"]["username"]);
                apikey = array[0]["success"]["username"].ToString();
                return true;
            }
            return false;
        }

        public async void InitLights()
        {
            if (await createUser("hueapplication") == true)
            {
                String jsonreturn = await nw.GetCommand("api/" + apikey + "/lights");
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
            };
            lamps[0].SetHSBValue(1200, 100, 254);
        }

        public async void sendCommando(string lightid, string json)
        {
            await nw.PutCommand("api/" + apikey + "/lights/" + lightid+"/state", json);
        }
    }
}
