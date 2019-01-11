using Blog.Data.Contracts;
using Blog.Data.Interface;
using Blog.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Services
{
    public class TestService : ITestContract
    {
        public ITestContract TestContract { get; set; }
        public string CodeTest()
        {
            return "Hello World!";
        }
    }
}
