using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Helper.Consul
{
    public static class ConsulHelper
    {
        /// <summary>
        /// 服务注册到consul 并设置应用程序终止时取消注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime lifetime, ConsulSetting consulSetting)
        {
            var consulClient = new ConsulClient(c =>
            {
                //consul地址
                c.Address = new Uri(consulSetting.ConsulAddress);
            });

            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),//服务实例唯一标识
                Name = consulSetting.ServiceName,//服务名
                Address = consulSetting.ServiceIP, //服务IP
                Port = int.Parse(consulSetting.ServicePort),//服务端口 因为要运行多个实例，端口不能在appsettings.json里配置，在docker容器运行时传入
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulSetting.RegisTimeAfterStarted),//服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(consulSetting.HealthCheckInternal),//健康检查时间间隔
                    HTTP = $"http://{consulSetting.ServiceIP}:{consulSetting.ServicePort}{consulSetting.HealthCheckAddr}",//健康检查地址
                    Timeout = TimeSpan.FromSeconds(consulSetting.HealthCheckTimeout)//超时时间
                }
            };

            //服务注册
            consulClient.Agent.ServiceRegister(registration).Wait();

            //应用程序终止时，取消注册
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }


        /// <summary>
        /// 获取服务列表
        /// </summary>
        /// <param name="consulSetting"></param>
        /// <returns></returns>
        public static string[] GetServiceList(ConsulClient consulClient, ConsulSetting consulSetting, QueryOptions queryOptions)
        {
            string[] serviceUrls = null;

            var res = consulClient.Health.Service(consulSetting.ServiceName, null, true, queryOptions).Result;

            if(queryOptions.WaitIndex != res.LastIndex)
            {
                queryOptions.WaitIndex = res.LastIndex;

                serviceUrls = res.Response.Select(p => $"http://{p.Service.Address}:{p.Service.Port}").ToArray();
                return serviceUrls;
            }

            return serviceUrls;
        }

    }
}
