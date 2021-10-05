using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WordCount.APICalls
{
    public class CommitDetailsAPI
    {
        public List<string> GetCommitDetais(string UserName, string token, string repourl)
        {
            HttpMessageHandler handler = new HttpClientHandler()
            {
            };
            string baseurl = "https://api.github.com/";
            string url = "repos/" + UserName + "/" + repourl.Split('/').Last() + "/commits";
            string credentials = UserName + ":" + token;
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseurl),
                Timeout = new TimeSpan(0, 2, 0)
            };

            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            //This is the key section you were missing    
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(credentials);
            string val = System.Convert.ToBase64String(plainTextBytes);
            httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            List<string> content = new List<string>();
            string strresult = null;
            

            if (response.IsSuccessStatusCode)
            {
                strresult = response.Content.ReadAsStringAsync().Result;
                //using (Stream stream = new StreamReader(response.Content.ReadAsStreamAsync()))
                //{
                //    StreamReader sr = new StreamReader(stream);
                //    strresult = sr.ReadToEnd();
                //    sr.Close();
                //}
            }
                
            //using (StreamReader stream = new StreamReader(response.Content.ReadAsStreamAsync().Result, System.Text.Encoding.GetEncoding(_encoding)))
            //{
            //    content.Add(stream.ReadToEnd());
            //}

            return content;
        }
    }
}