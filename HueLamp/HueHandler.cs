using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    class HueHandler
    {
        NetworkHandler nw;
        public ObservableCollection<HueLamp> lamps;
        public ObservableCollection<HueGroup> groups;
        string apikey;

        public HueHandler()
        {
            nw = new NetworkHandler("localhost", "8000");
            lamps = new ObservableCollection<HueLamp>();
            groups = new ObservableCollection<HueGroup>();
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
            if ((await createUser("hueapplication")) == true)
            {
                //lampen
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
                //groepen
                jsonreturn = await nw.GetCommand("api/" + apikey + "/groups");
                array = JArray.Parse("[" + jsonreturn + "]");
                for (int i = 1; i <= array[0].Count(); i++)
                {
                    var citem = array[0][i.ToString()];
                    String groupinformation = await nw.GetCommand("api/" + apikey + "/groups/" + i);
                    JObject jobject = JObject.Parse(groupinformation);
                    List<HueLamp> grouplamps = new List<HueLamp>();
                    foreach(var l in jobject["lights"])
                    {
                        grouplamps.Add(lamps[Int32.Parse(l.ToString())]);
                    }
                    groups.Add(new HueGroup(
                        this, 
                        i.ToString(),
                        jobject["action"]["on"].ToString(),
                        jobject["action"]["bri"].ToString(),
                        jobject["action"]["hue"].ToString(),
                        jobject["action"]["sat"].ToString(),
                        jobject["action"]["xy"][0].ToString(),
                        jobject["action"]["xy"][1].ToString(),
                        jobject["action"]["ct"].ToString(),
                        jobject["action"]["alert"].ToString(),
                        jobject["action"]["effect"].ToString(),
                        jobject["action"]["colormode"].ToString(),
                        jobject["action"]["reachable"].ToString(),
                        jobject["name"].ToString(),
                        grouplamps
                    ));           
                }
            };
        }

        public async void addGroup(String name, List<HueLamp> lights)
        {
            JArray jsonarray = new JArray();
            foreach (HueLamp l in lights)
            {
                jsonarray.Add(l.id);
            }
            dynamic jsonObject = new JObject();
            jsonObject.name = name;
            jsonObject.lights = jsonarray;
            String jsonreturn = await nw.PostCommand("api/" + apikey + "/groups", ((object)jsonObject).ToString());
            JArray array = JArray.Parse(jsonreturn);
            String groupid = array[0]["success"]["id"].ToString().Substring(8);
            groups.Add(new HueGroup(this, groupid, "false", "0", "0", "0", "0", "", "", "", "", "", "", name, lights));
        }

        public async void sendLightCommando(string lightid, string json)
        {
            await nw.PutCommand("api/" + apikey + "/lights/" + lightid+"/state", json);
        }

        public async void sendGroupCommando(string groupid, string json)
        {
            await nw.PutCommand("api/" + apikey + "/groups/" + groupid + "/action", json);
        }
    }
}
