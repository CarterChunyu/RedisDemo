using CSRedis;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class OverSell
    {       
        private Random R = new Random();
        private readonly static object LOCK = new object();
        // 超賣
        public void Show1()
        {
            int Count = 10;
            Parallel.For(1, 100000, (i) =>
            {
                if(Count > 0)
                {
                    Thread.Sleep(R.Next(5, 30));
                    Count = Count - 1;
                    Console.WriteLine($"用戶{i}參與秒殺, 產品剩下{Count}件");
                }
            });
            Console.WriteLine($"秒殺結束, 數量為{Count}");
        }
        // 加鎖 沒意義變成單線程
        // 也無法只在判斷和減Count時加鎖, 因為有可能判斷時Count還大於0
        public void Show2()
        {
            int Count = 10;
            bool isGoON = true;
            Parallel.For(1, 100000, (i) =>
            {
                if (isGoON)
                {
                    Thread.Sleep(R.Next(5, 30));
                    int index;
                    lock (LOCK)
                    {
                        index = --Count;
                    }
                    if(index >= 0)
                    {
                        Console.WriteLine($"客戶{i}參與秒殺成功, 商品剩下{index}件");
                    }
                    else
                    {
                        isGoON = false;
                        Console.WriteLine($"客戶{i}參與秒殺成功, 商品剩下{index}件");
                    }

                }
            });
        }
        // redis
        public void Show3()
        {
            string redisConnStr = "127.0.0.1,defaultDataBase=0,poolsize=3,tryit=0";
            CSRedisClient client = new CSRedisClient(redisConnStr);
            client.NodesServerManager.FlushAll();
            bool isGoON = true;
            string key = "stock";
            client.Set(key, 10);

            Parallel.For(1,1000, (i) =>
            {
                Thread.Sleep(R.Next(5, 30));
                if (isGoON)
                {
                    long index = client.IncrBy(key, -1);

                    if(index >= 0)
                    {
                        Console.WriteLine($"客戶{i}參與秒殺成功, 商品剩下{index}件");
                    }
                    else
                    {
                        if (isGoON)
                        {
                            isGoON = false;
                        }
                        Console.WriteLine($"客戶{i}參與秒殺失敗, 商品剩下{index}件");
                    }
                }
            });
        }
    }
}
