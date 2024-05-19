using CSRedis;

try
{
    Console.ForegroundColor = ConsoleColor.Red;
    string path = AppDomain.CurrentDomain.BaseDirectory;
    string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
    using(var redis = new CSRedisClient("127.0.0.1,defaultDatabase=0,poolsize=3,tryit=0"))
    {
        redis.NodesServerManager.FlushAll();
        string channel = "頻道001";
        for (int i = 1; i > 0; i++)
        {
            string mes = $"消息{i}";
            Console.WriteLine($"發布者={tag} 已發出消息: {mes}");

            // 向某一個頻道去發布消息
            redis.Publish(channel, mes);
            Thread.Sleep(1000);
        }
    }
    Console.ReadLine();
}
catch(Exception ex)
{

}