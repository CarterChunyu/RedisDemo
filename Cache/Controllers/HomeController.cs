using Cache.Filters;
using Cache.Helper;
using Cache.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Cache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _cache;

        public HomeController(ILogger<HomeController> logger,IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            string key = "index";
            string Data = _cache.Get<string>(key);
            if (string.IsNullOrEmpty(Data))
            {
                List<UserInfo> users = await DBHelper.Query<UserInfo>(3);
                Data = JsonConvert.SerializeObject(users);
                _cache.Set(key, Data, DateTime.Now.AddMinutes(3));
            }

            ViewBag.Data = Data; 
            return View();
        }

        public async Task<IActionResult> Index1()
        {            
            return View();
        }

        [HttpGet, CacheResultFilter]
        public async Task<string> Query(string key)
        {
            List<UserInfo> userInfos = await DBHelper.Query<UserInfo>(5);
            string data = JsonConvert.SerializeObject(userInfos);
            _cache.Set(key, data);
            return data;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}