using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class TestBase
    {
        protected CSRedisClient rds = new CSRedisClient("127.0.0.1,defaultDataBase=0,poolsize=3,tryit=0");
        protected readonly object Null = null;
        protected readonly string String = "I am a Chinese";
        protected readonly byte[] Bytes = Encoding.UTF8.GetBytes("I am a Chinese");
        protected readonly TestClass Class = new TestClass
        {
            Id = 1,
            Name = "Class名稱",
            CreateTime = DateTime.Now,
            TagId = new[] { 1, 3, 3, 3 },
        };

    }

    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public int[] TagId { get; set; } 
    }
}
