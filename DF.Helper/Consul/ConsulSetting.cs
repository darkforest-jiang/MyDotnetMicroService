using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Helper.Consul
{
    public class ConsulSetting
    {
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务IP
        /// </summary>
        public string ServiceIP { get; set; }

        /// <summary>
        /// 服务端口号
        /// </summary>
        public string ServicePort { get; set; }

        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthCheckAddr { get; set; }

        /// <summary>
        /// Consul地址
        /// </summary>
        public string ConsulAddress { get; set; }

        /// <summary>
        /// 服务启动后几秒注册/s
        /// </summary>
        public int RegisTimeAfterStarted { get; set; }

        /// <summary>
        /// 健康检查间隔/s
        /// </summary>
        public int HealthCheckInternal { get; set; }

        /// <summary>
        /// 健康检查超时时间/s
        /// </summary>
        public int HealthCheckTimeout { get; set; }
    }
}
