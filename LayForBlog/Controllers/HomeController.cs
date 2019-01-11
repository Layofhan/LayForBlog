using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LayForBlog.Models;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Data.Model;
using Blog.Data.Contracts;
using Blog.Data.Interface;

namespace LayForBlog.Controllers
{
    public class HomeController : Controller
    {
        public ITestContract TestContract { get; set; }
        public ICommentContract CommentContracts { get; set; }
        //首页
        public ActionResult Index()
        {
            return View();
        }
        //关于
        public ActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        //详情
        public ActionResult Details(int id)
        {
            ViewData["articleid"] = id;  
            return View();
        }

        //发表评论
        public ActionResult Comment()
        {
            return View();
        }

        //留言
        public ActionResult Message()
        {
            return View();
        }




        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Test()
        {
            //EFContext context = new EFContext();
            //Blog_TestTable a = new Blog_TestTable();
            //a.Name = "Lay";
            //a.PassWord = "Success";
            //context.Blog_TestTables.Add(a);
            //context.SaveChanges();
            ////Console.WriteLine("ok");
            //string s = TestContract.CodeTest();
            Class1 s = new Class1();
            s.hello();
            var test = TestContract.CodeTest();
            return test.ToMvcJson();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
