using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using DF.Helper.Consul;
using System.Collections.Concurrent;
using Consul;

namespace DF.Clients.Mall.MVC.Helper
{
    public class ServiceHelper : IServiceHelper
    {
        private readonly IConfiguration _configuration;
        private ConsulSetting _consulSetting;

        private readonly ConsulClient _consulClient;
        private ConcurrentBag<string> _goodsServiceUrls;
        private ConcurrentBag<string> _orderServiceUrls;

        public ServiceHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _consulSetting = _configuration.GetSection("ConsulSetting").Get<ConsulSetting>();
            _consulClient = new ConsulClient(c =>
            {
                //consul地址
                c.Address = new Uri(_consulSetting.ConsulAddress);
            });

        }

        public async Task<string> GetGoods()
        {
            if (_goodsServiceUrls == null)
                return await Task.FromResult("【商品服务】正在初始化服务列表...");

            var client = new RestClient(_goodsServiceUrls.ElementAt(new Random().Next(0, _orderServiceUrls.Count())));
            var request = new RestRequest("/Goods", Method.GET);

            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetOrder()
        {
            if (_orderServiceUrls == null)
                return await Task.FromResult("【订单服务】正在初始化服务列表...");

            var client = new RestClient(_orderServiceUrls.ElementAt(new Random().Next(0, _orderServiceUrls.Count())));
            var request = new RestRequest("/Order", Method.GET);

            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        /// <summary>
        /// 获取服务列表
        /// </summary>
        public void GetServices()
        {
            var serviceNames = new string[] { "GoodsService", "OrderService"};
            Array.ForEach(serviceNames, p =>
            {
                Task.Run(() =>
                {
                    //WaitTime默认为5分钟
                    var queryOptions = new QueryOptions { WaitTime = TimeSpan.FromMinutes(10) };
                    while (true)
                    {
                        GetServices(queryOptions, p);
                    }
                });
            });
        }

        private void GetServices(QueryOptions queryOptions, string serviceName)
        {
            var serviceUrls = ConsulHelper.GetServiceList(_consulClient, new ConsulSetting() { 
                ServiceName = serviceName,
                ConsulAddress = _consulSetting.ConsulAddress
            }, queryOptions);

            if(serviceUrls != null && serviceUrls.Length > 0)
            {                                                                        
                if (serviceName == "GoodsService")
                    _goodsServiceUrls = new ConcurrentBag<string>(serviceUrls);
                else if (serviceName == "OrderService")
                    _orderServiceUrls = new ConcurrentBag<string>(serviceUrls);
            }

        }

    }
}
