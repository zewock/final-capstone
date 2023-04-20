using Microsoft.AspNetCore.DataProtection.KeyManagement;
using RestSharp;
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;

namespace Capstone.Services
{
    public class RandomFactService : IRandomFactService
    {
        protected static RestClient client = null;

        public RandomFactService()
        {
            if (client == null)
            {
                client = new RestClient("https://uselessfacts.jsph.pl/api/v2/facts/random");
            }
        }

        public string GetRandomFact()
        {
            try
            {
                RestRequest request = new RestRequest();
                var response = client.Get<RandomFactModel>(request);
                return response.Data.Text;
            }
            catch
            {
                return "Unable to get random fact";
            }
        }
    }
}
