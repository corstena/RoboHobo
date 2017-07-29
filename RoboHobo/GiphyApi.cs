using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace RoboHobo
{
    public class GiphyApi
    {
        const String BaseURL = "http://api.giphy.com";
        String _apiKey;

        public GiphyApi(string apiKey)
        {
            _apiKey = apiKey;
        }

        public String runRequest(RestRequest request)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseURL);
            request.AddParameter("api_key", ConfigurationManager.AppSettings["giphyKey"]);

            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
            {
                const String message = "Error retrieving response. ";
                Console.WriteLine(message + response.ErrorException);
                var giphyException = new ApplicationException(message, response.ErrorException);
                throw giphyException;
            }
            return response.Content;
        }

        public String GetRandomGif()
        {
            var request = new RestRequest();
            request.Resource = "/v1/gifs/random";
            request.RootElement = "data";
            
            var giphyResponseText = runRequest(request);
            JObject giphyResponse = JObject.Parse(giphyResponseText);

            String url = (String)giphyResponse.SelectToken("data.url");
            return url;
        }

        public String GetAnimeGif(String searchQuery)
        {
            var request = new RestRequest();
            request.Resource = "/v1/gifs/search";
            request.RootElement = "data";
            request.AddParameter("q", searchQuery + "+anime");
            request.AddParameter("limit", 5);

            String giphyResponseText = runRequest(request);
            JObject giphyResponse = JObject.Parse(giphyResponseText);
            IList<JToken> results = giphyResponse["data"].Children().ToList();

            Random r = new Random();
            String url = (String)giphyResponse.SelectToken("data[" + r.Next(0, results.Count) + "].url");
            return url;
        }

    }
}
