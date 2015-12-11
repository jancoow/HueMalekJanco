using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HueLamp
{
    class HueHandler
    {
        public static ApplicationDataContainer LOCAL_SETTINGS = ApplicationData.Current.LocalSettings;
        NetworkHandler nw;
        public ObservableCollection<HueLamp> lamps;
        public ObservableCollection<HueGroup> groups;
        string apikey;
        public HueLamp hue;
        public HueHandler()
        {
            //nw = new NetworkHandler("hue.imegumii.space", "80");
            nw = new NetworkHandler("Localhost", "8000");
            lamps = new ObservableCollection<HueLamp>();
            groups = new ObservableCollection<HueGroup>();
            //if (LOCAL_SETTINGS.Values["apikey"] != null)
             //   apikey = LOCAL_SETTINGS.Values["apikey"].ToString();
            InitLights();
        }

        public async Task<Boolean> createUser(string name)
        {
            if (apikey == null) { 
                JArray array = JArray.Parse(await nw.PostCommand("api", "  {  \"devicetype\":  \" " + name + " \"}  "));
                if (array[0]["error"] != null)
                {
                    System.Diagnostics.Debug.WriteLine("Error:" + array[0]["error"]["description"]);
                    return false;
                //Hier moet iets met de error gedaan worden
                }
                else if (array[0]["success"] != null)
                {
                    System.Diagnostics.Debug.WriteLine("Username key:" + array[0]["success"]["username"]);
                    apikey = array[0]["success"]["username"].ToString();
                    LOCAL_SETTINGS.Values["apikey"] = apikey;
                    return true;
                }
                return false;
            }
            else
            {

                return true;
            }
        }

        public async void InitLights()
        {
            if ((await createUser("hueapplication")) == true)
            {
                //lampen
                String jsonreturn = await nw.GetCommand("api/" + apikey + "/lights");
                JObject lampsjson = JObject.Parse(jsonreturn);
                foreach (var i in lampsjson)
                {
                    JObject lamp= JObject.Parse(lampsjson["" + i.Key].ToString());
                    JObject lampstate = JObject.Parse(lamp["state"].ToString());
                    lamps.Add(hue = new HueLamp(
                        this,
                        i.Key,
                        getKey(lampstate, "on"),
                        getKey(lampstate, "bri"),
                        getKey(lampstate, "hue"),
                        getKey(lampstate, "sat"),
                        "0",
                        "0",
                        getKey(lampstate, "ct"),
                        getKey(lampstate, "alert"),
                        getKey(lampstate, "effect"),
                        getKey(lampstate, "colormode"),
                        getKey(lampstate, "reachable"),
                        getKey(lamp, "type"),
                        getKey(lamp, "name"),
                        getKey(lamp, "modelid"),
                        getKey(lamp, "swversion")
                        ));
                    
                }

                //groepen
                jsonreturn = await nw.GetCommand("api/" + apikey + "/groups");
                foreach (var i in JObject.Parse(jsonreturn))
                {
                    string groupinformation = await nw.GetCommand("api/" + apikey + "/groups/" + i.Key);
                    JObject group = JObject.Parse(groupinformation);
                    JObject groupaction;
                    try {groupaction = JObject.Parse(group["action"].ToString()); } catch {groupaction = group; };
                    List<HueLamp> grouplamps = new List<HueLamp>();
                    foreach(var l in group["lights"])
                    {
                        foreach(HueLamp h in lamps)
                        {
                            if(h.id == l.ToString())
                                grouplamps.Add(h);
                        }
                    }
                    groups.Add(new HueGroup(
                        this, 
                        i.Key,
                        getKey(groupaction, "on"),
                        getKey(groupaction, "bri"),
                        getKey(groupaction, "hue"),
                        getKey(groupaction, "sat"),
                        "0",
                        "0",
                        getKey(groupaction, "ct"),
                        getKey(groupaction, "alert"),
                        getKey(groupaction, "effect"),
                        getKey(groupaction, "colormode"),
                        getKey(groupaction, "reachable"),
                        getKey(group, "name"),
                        grouplamps
                    ));           
                }
            };
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
