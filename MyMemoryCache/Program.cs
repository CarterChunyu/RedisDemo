// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Diagnostics.CodeAnalysis;





CustomerCache customerCache = new CustomerCache();

Random r = new Random();
Parallel.For(0, 1000,  (i) =>
{
    string key = $"Key_{i}";
    customerCache.Set(key, 1000, 1000);
});
Console.WriteLine(customerCache.Get<int>("Key_50"));

class CustomerCache
{
    private static List<Dictionary<string, DataModel?>> cacheDictionaryList = new List<Dictionary<string, DataModel?>>();
    
    private static List<object> lock_objectList = new List<object>();

    private static int num = 4;

    static CustomerCache()
    {
        for (int i = 0; i < num; i++)
        {
            cacheDictionaryList.Add(new Dictionary<string, DataModel?>());
            lock_objectList.Add(new object());
        }

        Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(20));
                for (int i = 0; i < num; i++)
                {
                    lock (lock_objectList[i])
                    {
                        var removeList = cacheDictionaryList[i].Where(x => x.Value.DeadLine < DateTime.Now).Select(x => x.Key).ToList();
                        foreach (var item in removeList)
                        {
                            cacheDictionaryList[i].Remove(item);
                        }
                    }
                }
            }
        }).ContinueWith(t =>
        {
            if (t.IsFaulted)
            {

            }
        });
    }

    public void Set<T>(string key, T? t)
    {
        DataModel dataModel = new DataModel
        {
            Value = t,
            ObsloteType = ObsloteType.Never,
        };
        int index = GetIndex(key);
        lock (lock_objectList[index])
        {
            cacheDictionaryList[index].Add(key, dataModel);
        }
    }

    public void Set<T>(string key, T? t, int outputTime)
    {
        DataModel dataModel = new DataModel
        {
            Value = t,
            ObsloteType = ObsloteType.Absolutely,
            DeadLine = DateTime.Now.AddSeconds(outputTime)
        };
        int Index = GetIndex(key);
        lock (lock_objectList[Index])
        {
            cacheDictionaryList[Index].Add(key, dataModel);
        }
    }

    public void Set<T>(string key, T? t, TimeSpan timeSpan)
    {
        DataModel dataModel = new DataModel
        {
            Value = t,
            ObsloteType = ObsloteType.Relative,
            DeadLine = DateTime.Now.Add(timeSpan),
            Duration = timeSpan
        };
        int index = GetIndex(key);
        lock (lock_objectList[index])
        {
            cacheDictionaryList[index].Add(key, dataModel);
        }
    }

    public T? Get<T>(string key)
    {
        int index = GetIndex(key);
        if (Exists(key))
        {
            DataModel dataModel = cacheDictionaryList[index][key];
            return (T)dataModel.Value;
            
        }
        else
            return default(T?);
    }

    public bool Exists(string key)
    {
        int index = GetIndex(key);
        if (cacheDictionaryList[index].ContainsKey(key))
        {
            DataModel dataModel = cacheDictionaryList[index][key];
            if (dataModel.ObsloteType == ObsloteType.Never)
                return true;
            else if (dataModel.ObsloteType == ObsloteType.Absolutely)
            {
                if (dataModel.DeadLine >= DateTime.Now)
                    return true;
                else
                    return false;
            }
            else
            {
                if (dataModel.DeadLine < DateTime.Now)
                {
                    dataModel.DeadLine = DateTime.Now.Add(dataModel.Duration);
                    return true;
                }
                else
                    return false;

            }
        }
        else
            return false;
    }

    private int GetIndex(string Key)
    {
        int code = Key.GetHashCode();
        int index = code & 0x7fffffff % num;
        return index;
    }
}


class DataModel
{
    // 緩存數據
    public object? Value { get; set; }

    // 過期類型   
    public ObsloteType ObsloteType { get; set; }

    // 過期時間
    public DateTime DeadLine { get; set; }

    // 滑動時間
    public TimeSpan Duration { get; set; }
}

enum ObsloteType
{
    // 絕不
    Never,
    // 絕對時間過期
    Absolutely,
    // 滑動過期
    Relative,
}