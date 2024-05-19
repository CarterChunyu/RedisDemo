using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class RedisHashTest : TestBase
    {
         public void ShowHashExists()
        {
            rds.NodesServerManager.FlushAll();
            rds.HMSet("TestHASH1", "string1", base.String, "byte1", base.Bytes, "class1", base.Class);

            rds.HDel("TestHASH1", "string1", "byte1", "class1");

            bool exists = rds.HExists("TestHASH1", "null1");
            Console.WriteLine(exists);
        }

        public void ShowGet()
        {
            rds.NodesServerManager.FlushAll();

            rds.HMSet("TestHGet", "null1", base.Null, "string1", base.String, "byte1", base.Bytes,
                "class1", base.Class, "classArray", new[] { base.Class, base.Class });

            string result1 = rds.HGet("TestHGet", "null1");
            string result2 = rds.HGet("TestHGet", "string1");
            byte[] result3 = rds.HGet<byte[]>("TestHGet", "byte1");
            TestClass result4 = rds.HGet<TestClass>("TestHGet", "null1");
            TestClass[] result5 = rds.HGet<TestClass[]>("TestHGet", "classArray");

            Dictionary<string, string> dicResult = rds.HGetAll("TestHGet");
            var result6 = rds.HGetAll("TestHGet")["string1"];
            var result7 = rds.HGetAll("TestHGet")["byte1"];
            var result8 = rds.HGetAll("TestHGet")["class1"];
        }

        public void HashHIncre()
        {
            rds.NodesServerManager.FlushAll();

            rds.HMSet("HashHIncre", "null1", 10, "string1", base.String, "byte1", base.Bytes,
                "class1", base.Class, "classArray", new[] { base.Class, base.Class });

            long result1 = rds.HIncrBy("HashHIncre", "null1", 1);
            long result2 = rds.HIncrBy("HashHIncre", "null1", 1);
        }

        public void HashKeys()
        {
            rds.NodesServerManager.FlushAll();

            rds.HMSet("HashKeys", "null1", 10, "string1", base.String, "byte1", base.Bytes,
                "class1", base.Class, "classArray", new[] { base.Class, base.Class });

            foreach (var key in rds.HKeys("HashKeys"))
            {
                Console.WriteLine(key);
            }
            long count = rds.HLen("HashKeys");
            Console.WriteLine(count);
        }

    }
}
