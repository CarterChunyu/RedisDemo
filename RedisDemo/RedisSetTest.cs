using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class RedisSetTest:TestBase
    {
        public void ShowApi()
        {
            rds.NodesServerManager.FlushAll();

            string key1 = "list1";
            string key2 = "list2";

            rds.SAdd(key1, "111");
            rds.SAdd(key1, "112");
            rds.SAdd(key1, "113", "114");
            // 不同型別 改存{}
            //rds.SAdd(key1, new TestBase());

            // 集合個數
            long count = rds.SCard(key1);

            rds.SAdd(key2, "111");
            rds.SAdd(key2, "113", "114");

            // 兩集合的差集
            string[] sDiffList = rds.SDiff(key1, key2);

            // 兩集合的交集
            string[] sInnerList = rds.SInter(key1, key2);

            // 兩集合的差集
            string[] sUnionList = rds.SUnion(key1, key2);

            // 是否在集合中
            bool flag = rds.SIsMember(key1, "111");
            // 返回所有集合元素
            string[] l = rds.SMembers(key1);
            // 隨機返回一個
            string number1 = rds.SRandMember(key1);
            // 隨機返回多個
            string[] number2 = rds.SRandMembers(key1,2);
            // 隨機移除返回一個
            string number3 = rds.SPop(key1);
            // 隨機移除返回多個
            string[] number4 = rds.SPop(key1, 2);



            
            rds.NodesServerManager.FlushAll();

            // 去重
            string key = "list";

            rds.SAdd(key, "111");
            rds.SAdd(key, "112");
            rds.SAdd(key, "113", "114");
            rds.SAdd(key, "111");
            rds.SAdd(key, "112");
            rds.SAdd(key, "113", "114");

            string[] all = rds.SMembers(key);
        }

       

        // 2次好友
        public void FriendManagerShow()
        {
            rds.NodesServerManager.FlushAll();

            rds.SAdd("yu", "勳哥");
            rds.SAdd("yu", "鄧碧");
            rds.SAdd("yu", "斯童");

            rds.SAdd("Dru", "大頭");
            rds.SAdd("Dru", "鄧碧");
            rds.SAdd("Dru", "黃澄");

            // 共同好友 交集
            string[] result1 = rds.SInter("yu", "Dru");
            // 可能認識 差集
            string[] result2 = rds.SDiff("yu", "Dru");
            string[] result3 = rds.SDiff("Dru", "yu");
            // 和集
            string[] result4 = rds.SUnion("yu", "Dru");
        }
    }
}
