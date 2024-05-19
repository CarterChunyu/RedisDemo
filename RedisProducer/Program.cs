using CSRedis;

try
{
    Console.ForegroundColor = ConsoleColor.Red;
    string path = AppDomain.CurrentDomain.BaseDirectory;
    string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
    Console.WriteLine($"這裡是生產者 {tag} 啟動了");

    using(var redis = new CSRedisClient("127.0.0.1,defaultDatabase=0,poolsize=3,tryit=0"))
    {
        string queueKey = "queueKey";
        for (int i = 1; i > 0; i++)
        {
            string mes = $"{tag}: 消息----{i}";
            redis.RPush(queueKey, mes);
            Console.WriteLine($"生產者: {tag}已發出消息 {mes}");
            Thread.Sleep(1000);
        }
    }
    Console.Read();
}
catch(Exception ex)
{

}