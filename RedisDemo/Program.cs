using CSRedis;
using RedisDemo;

try
{
    {
    //    string redisConnString = "127.0.0.1,defaultDatabase=2,poolsize=3,tryit=0";
    //    CSRedisClient client = new CSRedisClient(redisConnString);
    //    client.Set("test1", "測試一下");
    //    string cResult = client.Get("test1");
    }
    //new RedisStringTest().ShowGetString();
    { 
    //new OverSell().Show1();
    //new OverSell().Show2();

    //new RedisHashTest().ShowHashExists();
    //new RedisHashTest().ShowGet();
    //new RedisHashTest().HashHIncre();
    //new RedisHashTest().HashKeys();
    }
    {
        //new RedisListTest().ApiShow();
        //new RedisListTest().BlogShow();
        //new RedisListTest().QueueStack();
    }
    {
        //new RedisSetTest().ShowApi();
        //new RedisSetTest().FriendManagerShow();
    }
    {
        //new RedisZSetTest().ShowApi();
        await new RedisZSetTest().ZSetRankShow();
    }
}
catch(Exception ex)
{

}