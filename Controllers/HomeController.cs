using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomPass.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPass.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            
            string password = RandomString(14).ToLower();
            int? cnt = HttpContext.Session.GetInt32("cnt");
            if(cnt==null){
                cnt=0;
            }
            cnt++;
            HttpContext.Session.SetInt32("cnt",(int)cnt);

            ViewBag.Password = password;
            ViewBag.Cnt = cnt;

            return View();
        }

        [HttpGet("pass")]
        public JsonResult RandomPassword()
        {
            
            string password = RandomString(14).ToLower();
            int cnt = (int)((HttpContext.Session.GetInt32("cnt")!=null)?HttpContext.Session.GetInt32("cnt"):0)+1;
            HttpContext.Session.SetInt32("cnt",(int)cnt);

            var ret = new Dictionary<string, string>
            {
                { "password", password },
                { "cnt", cnt.ToString() }
            };

            return Json(ret);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    
}
