using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;

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

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseURL);
            request.AddParameter("api_key", ConfigurationManager.AppSettings["giphyKey"]);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const String message = "Error retrieving response.";
                var giphyException = new ApplicationException(message, response.ErrorException);
                throw giphyException;
            }
            return response.Data;
        }

        public RandomGif GetRandomGif()
        {
            var request = new RestRequest();
            request.Resource = "/v1/gifs/random";
            request.RootElement = "data";

            return Execute<RandomGif>(request);
        }
    }
}
