using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.Clients.Mall.MVC.Helper
{
    public interface IServiceHelper
    {
        Task<string> GetGoods();

        Task<string> GetOrder();

        /// <summary>
        /// 获取服务列表
        /// </summary>
        void GetServices();
    }
}
