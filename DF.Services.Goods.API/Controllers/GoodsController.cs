using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DF.Services.Goods.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoodsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get()
        {
            string result = $"【商品服务】：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} -- {Request.HttpContext.Connection.LocalIpAddress} : {Request.HttpContext.Connection.LocalPort}";
            return Ok(result);
        }
    }
}
