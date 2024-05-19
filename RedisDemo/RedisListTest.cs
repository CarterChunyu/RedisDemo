using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class RedisListTest:TestBase
    {
        public void ApiShow()
        {
            rds.NodesServerManager.FlushAll();

            string key = "newBlog";
            // 頭部插入
            rds.LPush(key, "1Value");
            rds.LPush(key, "2Value");
            rds.LPush(key, "3Value");
           
            // 尾部插入
            rds.RPush(key, "4Value");
            rds.RPush(key, "5Value");
            rds.RPush(key, "6Value");
            rds.RPush<TestClass>(key, new TestClass());

            // 獲取數據
            Console.WriteLine(rds.LIndex(key,0));

            // 將值插入到已存在列表
            rds.LInsertBefore(key, "4Value", "3.5Value");

            // 跳出並移除第一個
            string result = rds.LPop(key);

            // 範圍查詢 -- 分頁
            string[] arratlist1 = rds.LRange(key, 1, 3);


            // 根據Key和索引移除相同的值
            rds.LRem(key, 0, "1Value");

            // 根據Key和索引修改值
            rds.LSet(key, 1, "321Value");

            // 移除範圍外的數據
            rds.LTrim(key, 1, 2);

            // 彈出最後一個
            string result1 = rds.RPop(key);
        }
        public void BlogShow()
        {
            rds.NodesServerManager.FlushAll();
            string key = "PK-Title";
            for (int i = 0; i < 10; i++)
            {
                rds.LPush(key, $"Id{i}-Title{i}");
            }
            // 每插入一組ID-標題
            rds.LPush(key, "最新Id-最新Title");
            // 就修剪前五條數據
            rds.LTrim(key, 0, 4);
        }

        public void QueueStack()
        {
            // stack
            rds.NodesServerManager.FlushAll();
            string key = "stack";
            rds.LPush(key, "s1");
            rds.LPush(key, "s2");
            rds.LPush(key, "s3");
            string result = rds.LPop(key);

            // queue
            rds.NodesServerManager.FlushAll();
            key = "queue";
            rds.LPush(key, "s1");
            rds.LPush(key, "s2");
            rds.LPush(key, "s3");
            string result1 = rds.RPop(key);
        }
    }
}
