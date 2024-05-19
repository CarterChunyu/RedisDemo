using CSRedis;
using System;
using static CSRedis.CSRedisClient;

try
{
    // 訂閱者
    Console.ForegroundColor = ConsoleColor.Green;
    string path = AppDomain.CurrentDomain.BaseDirectory;
    string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
    Console.WriteLine($"這裡是訂閱者={tag} 啟動了");
    using (var redis = new CSRedisClient("127.0.0.1,defaultDatabase=0,poolsize=3,tryit=0"))
    {
        string channel = "頻道001";
        redis.Subscribe((channel, messageEventArgs =>
        {
             Console.WriteLine($"訂閱者={tag}: 收到第{messageEventArgs.MessageId}消息 消息內容:{messageEventArgs.Body}");          
        }
        ));

        Console.WriteLine($"Subscribed to channel: channel");
    }
    Console.ReadLine();
}
catch(Exception ex)
{

}
