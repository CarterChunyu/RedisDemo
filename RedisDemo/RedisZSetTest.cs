using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class RedisZSetTest:TestBase
    {
        public void ShowApi()
        {
            rds.NodesServerManager.FlushAll();

            string key1 = "Zset1";
            rds.ZAdd(key1, (1,1000));
            rds.ZAdd(key1, (1, 10000));
            rds.ZAdd(key1, (2, 2000));
            rds.ZAdd(key1, (2.5m, 20000));
            rds.ZAdd(key1, (3, 3000));
            rds.ZAdd(key1, (3, 3000));
            rds.ZAdd(key1, (4, 4000));
            rds.ZAdd(key1, (5, 5000));

            // 獲取集合數量
            long count1 = rds.ZCard(key1);
            // 計算評分區間數量
            long count2 = rds.ZCount(key1,1,3);
            // 給成員添加分數
            rds.ZIncrBy(key1, "1000", 9);

            // 通過索引 (排第一索引為0) 返回有續集和成員 
            var arrayList1 = rds.ZRange(key1, 1, 3);

            // 通過索引 返回有續集和指定區間成員
            var arrayList2 = rds.ZRangeWithScores(key1, 1, 2);

            // 通過分數返回有續集和成員  key min max count
            var arrayList3 = rds.ZRangeByScore(key1, 1, 5, 2);

            // 通過索引 返回有續集和指定區間成員
            var arrayList4 = rds.ZRangeByScoreWithScores(key1, 1, 2);

        } 

        public async Task ZSetRankShow()
        {
            List<string> users = new List<string>
            {
               "胖羽","爺爺竹","斯童", "安安", "冠勳", "阿昌","晉銓"
            };

            rds.NodesServerManager.FlushAll();

            string key = "潘宏愛玩狗";
            Task.Run(() =>
            {
                Parallel.ForEach(users, user =>
                {
                    while (true)
                    {
                        Thread.Sleep(800);
                        int money = new Random().Next(1, 51);
                        rds.ZIncrBy(key, user, money);
                    }
                });
            });
           


            // 排行榜
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);

                    int i = 0;

                    Console.WriteLine("***************************************");
                    foreach (var item in rds.ZRevRangeWithScores(key, 0 , 4))
                    {
                        Console.WriteLine($"第{++i}名 {item.member} 分數:{item.score}");
                    }
                }
            });
        }
    }
}
