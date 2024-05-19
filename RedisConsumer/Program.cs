using CSRedis;
using System.Globalization;

try
{
    // 消費者
    Console.ForegroundColor = ConsoleColor.Green;
    string path = AppDomain.CurrentDomain.BaseDirectory;
    string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
    Console.WriteLine($"這裡是消費者 {tag} 啟動了");

    while (true)
    {
        using(var redis = new CSRedisClient("127.0.0.1,defaultDatabase=0,poolsize=3,tryit=0"))
        {
            // 阻塞訂閱
            (string key, string vlaue)? result = redis.BLPopWithKey(500, "queueKey");
            if(!string.IsNullOrEmpty(result.Value.key))
            {
                
                Console.WriteLine($"消費者={tag}收到消息: {result.Value.vlaue}");
            }
            Thread.Sleep(1000);
        }
    }
    Console.Read();


}
catch (Exception ex)
{

}