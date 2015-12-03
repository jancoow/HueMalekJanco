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
            nw = new NetworkHandler("145.48.205.190", "80");
            lamps = new ObservableCollection<HueLamp>();
            groups = new ObservableCollection<HueGroup>();
            InitLights();
        }

        public async Task<Boolean> createUser(string name)
        {
            apikey = "14a01512180476bf133718e038ce829f";
            return true;
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
                JObject o = JObject.Parse(jsonreturn);
                foreach (var i in o)
                {
                    JObject citem= JObject.Parse(o["" + i.Key].ToString());
                    JObject citemstate = JObject.Parse(citem["state"].ToString());
                    lamps.Add(new HueLamp(
                        this,
                        i.Key,
                        getKey(citemstate, "on"),
                        getKey(citemstate, "bri"),
                        getKey(citemstate, "hue"),
                        getKey(citemstate, "sat"),
                        "0",
                        "0",
                        getKey(citemstate, "ct"),
                        getKey(citemstate, "alert"),
                        getKey(citemstate, "effect"),
                        getKey(citemstate, "colormode"),
                        getKey(citemstate, "reachable"),
                        getKey(citem, "type"),
                        getKey(citem, "name"),
                        getKey(citem, "modelid"),
                        getKey(citem, "swversion")
                        ));
                }

                //groepen
                jsonreturn = await nw.GetCommand("api/" + apikey + "/groups");
                o = JObject.Parse(jsonreturn);
                foreach (var i in o)
                {
                    var citem = o["" + i.Key];
                    String groupinformation = await nw.GetCommand("api/" + apikey + "/groups/" + i.Key);
                    JObject jobject = JObject.Parse(groupinformation);
                    JObject jobjectaction;
                    try {jobjectaction = JObject.Parse(jobject["action"].ToString()); } catch {jobjectaction = jobject; };
                    List<HueLamp> grouplamps = new List<HueLamp>();
                    foreach(var l in jobject["lights"])
                    {
                       // grouplamps.Add(lamps[Int32.Parse(l.ToString())]);
                    }
                    groups.Add(new HueGroup(
                        this, 
                        i.Key,
                        getKey(jobjectaction, "on"),
                        getKey(jobjectaction, "bri"),
                        getKey(jobjectaction, "hue"),
                        getKey(jobjectaction, "sat"),
                        "0",
                        "0",
                        getKey(jobjectaction, "ct"),
                        getKey(jobjectaction, "alert"),
                        getKey(jobjectaction, "effect"),
                        getKey(jobjectaction, "colormode"),
                        getKey(jobjectaction, "reachable"),
                        getKey(jobject, "name"),
                        grouplamps
                    ));           
                }
            };
            groups[0].setRGBValue(0, 255, 0);
        }

        public String getKey(JObject jobject, String arrayplace)
        {
            try
            {
                return jobject[arrayplace].ToString();
            }
            catch
            {
                return "0";
            }
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
