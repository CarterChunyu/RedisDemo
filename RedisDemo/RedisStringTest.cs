using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class RedisStringTest : TestBase
    {
        public void ShowGetString()
        {
            {
                // Append 追加
                //rds.NodesServerManager.FlushAll();
                //var key = "TestAppend_null";
                //rds.Set(key, base.String);
                //Console.WriteLine(rds.Get(key));
                //rds.Set(key, "yuyu");
                //Console.WriteLine(rds.Get(key));
                //rds.Append(key, base.Null);
            }
            {
                // 將資料存入Steam
                //rds.NodesServerManager.FlushAll();
                //var key = "TestAppend_String";
                //rds.Set(key, base.String);
                //rds.Append(key, base.String);
                //MemoryStream ms = new MemoryStream();
                //rds.Get(key, ms);
                //string result2 = Encoding.UTF8.GetString(ms.ToArray());
                //Console.WriteLine(result2);
            }
            {
                //存入 Byte
                //string key = "TestAppend_bytes";
                //rds.Set(key, base.Bytes);
                //rds.Append(key, base.Bytes);
                ////var result = Convert.ToBase64String(rds.Get<byte[]>(key));
                ////Console.WriteLine(result);
                ////// Decode
                ////byte[] bytedwcode = Convert.FromBase64String(result);
                ////Console.WriteLine(Encoding.UTF8.GetString(bytedwcode));

                //// 自動解碼
                //Console.WriteLine(rds.Get(key));
            }
            {
                // 取一定範圍的值
                //string key = "TestGetRange";
                //rds.Set(key, "abcdef");
                //Console.WriteLine(rds.GetRange(key,2,4));
                //Console.WriteLine(rds.GetRange(key, 0, -1));
            }
            {
                // GetSet
                //string key = "TestGetSet";
                //rds.Set(key, "oldv");
                //Console.WriteLine(rds.GetSet(key, "newValue"));
                //Console.WriteLine(rds.Get(key));
            }
            {
                //rds.NodesServerManager.FlushAll();
                //// 自增自減
                //string key = "TestIncrBy_null";
                //rds.IncrBy(key,1);
                //Console.WriteLine(rds.Get(key));
                //rds.IncrBy(key, 1);
                //Console.WriteLine(rds.Get(key));
                //rds.IncrBy(key, 10);
                //Console.WriteLine(rds.Get(key));
                //rds.IncrBy(key, -1);
                //Console.WriteLine(rds.Get(key));
            }
            {
                // MSet MGet
                //rds.NodesServerManager.FlushAll();
                //// MSet
                //rds.MSet("key1", "1", "keynull", base.Null, "keystring", base.String, "keybyte", base.Bytes);
                //// MGet
                //var result = rds.MGet("key1", "keystring", "keybyte");
                //Console.WriteLine(result[2]);
                //Console.WriteLine(result.Length);
            }
            {
                //rds.NodesServerManager.FlushAll();
                //string key = "Append_Class";
                //// 如果保存對象-- 序列化成json字符串保存
                //rds.Set(key, base.Class);
                //// 獲取到的就是json字符串
                //var reusltString = rds.Get(key);
                //Console.WriteLine(reusltString);
                //// 泛型-- 反序列化成指定類型對象
                //TestClass test = rds.Get<TestClass>(key);
                //Console.WriteLine(test.Name);
                
                //// 這操作不是原子性操作有風險, 要用Hash結構來解決
                //test.Name = "testClass1";
                //rds.GetSet(key, test);
            }
        }
    }
}
