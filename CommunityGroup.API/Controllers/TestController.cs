using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityGroup.Common;
using Microsoft.AspNetCore.Authorization;

namespace CommunityGroup.API.Controllers
{
    [Route("api/redis")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Cookies")]//全局身份验证
    public class TestController : ControllerBase
    {
        private readonly IDatabase _redis;
        public TestController(RedisHelper client)
        {
            _redis = client.GetDatabase();
        }

        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {
            //往Redis里面存入数据
            _redis.StringSet("Name", "Tom");
            
            NLogHelper.Info("这里是info日志");
            NLogHelper.Error("这是error日志", new Exception("测试"));
            //从Redis里面取数据
            string name = _redis.StringGet("Name");
            return name;
        }
    }
}
