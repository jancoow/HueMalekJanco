using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HueLamp
{
    class NetworkHandler
    {
        string ip;
        string port;
        public NetworkHandler(string ip, string port)
        {
            this.ip = ip;
            this.port = port;
        }

        public async Task<string> GetCommand(string url)
        {
            url = "http://" + ip + ":" + port + "/" + url;
            System.Diagnostics.Debug.WriteLine(url);

            using (HttpClient hc = new HttpClient())
            {
                try
                {
                    var response = await hc.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());
                    return await response.Content.ReadAsStringAsync(); ;
                }
                catch
                {
                    return "[{\"error\":{\"address\":\"\",\"description\":\"Network error\",\"type\":\"0\"}}]";
                }
            }
        }

        public async Task<string> PutCommand(string url, string Data)
        {
            url = "http://" + ip + ":" + port + "/" + url;
            HttpContent content = new StringContent(Data,Encoding.UTF8,"application/json");
            using (HttpClient hc = new HttpClient())
            {
                try
                {
                    var response = await hc.PutAsync(url, content);
                    response.EnsureSuccessStatusCode();
                    System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());
                    return await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    return "[{\"error\":{\"address\":\"\",\"description\":\"Network error\",\"type\":\"0\"}}]";
                }
            }
        }

        public async Task<string> DeleteCommand(string url)
        {
            url = "http://" + ip + ":" + port + "/" + url;
            using (HttpClient hc = new HttpClient())
            {
                try
                {
                    var response = await hc.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                    System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());
                    return await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    return "[{\"error\":{\"address\":\"\",\"description\":\"Network error\",\"type\":\"0\"}}]";
                }
            }
        }

        public async Task<string> PostCommand(string url, string Data )
        {
            url = "http://" + ip + ":" + port + "/" + url;
            HttpContent content = new StringContent(Data, Encoding.UTF8, "application/json");
            using (HttpClient hc = new HttpClient())
            {
                try
                {
                    var response = await hc.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
                    System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());
                    return await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    return "[{\"error\":{\"address\":\"\",\"description\":\"Network error\",\"type\":\"0\"}}]";
                }
            }
        }
    }
}
