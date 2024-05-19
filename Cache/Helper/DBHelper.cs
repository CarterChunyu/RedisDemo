using System.Net.Http.Headers;

namespace Cache.Helper
{
    public class DBHelper
    {
        public static async Task<List<T>> Query<T>(int index) where T : new()
        {
            // 耗時模擬
            await Task.Delay(TimeSpan.FromSeconds(10));

            List<T> list = new List<T>();
            for (int i = 0; i < index; i++)
            {
                list.Add((T)Activator.CreateInstance(typeof(T), null));
            }
            return list;
        } 
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }    
    }
}
