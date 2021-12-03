using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.Clients.Mall.MVC.Helper
{
    public class GatewayServiceHelper : IServiceHelper
    {
        public async Task<string> GetGoods()
        {
            var Client = new RestClient("http://localhost:9417");
            var request = new RestRequest("/goods", Method.GET);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetOrder()
        {
            var Client = new RestClient("http://localhost:9417");
            var request = new RestRequest("/order", Method.GET);

            var response = await Client.ExecuteAsync(request);
            return response.Content;
        }

        public void GetServices()
        {
            throw new NotImplementedException();
        }
    }
}
