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
        public Task<List<string>> GetCommitDetais(string UserName, string token, string repourl)
        {
            HttpMessageHandler handler = new HttpClientHandler()
            {
            };
            string url = "https://api.github.com/repos/" + UserName + "/" + repourl.Split('/').Last() + "/commits";
            string credentials = UserName + ":" + token;
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
                Timeout = new TimeSpan(0, 2, 0)
            };

            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            //This is the key section you were missing    
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(credentials);
            string val = System.Convert.ToBase64String(plainTextBytes);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            List<string> content = new List<string>();

            //using (StreamReader stream = new StreamReader(response.Content.ReadAsStreamAsync().Result, System.Text.Encoding.GetEncoding(_encoding)))
            //{
            //    content.Add(stream.ReadToEnd());
            //}

            return Task.FromResult(content);
        }
    }
}