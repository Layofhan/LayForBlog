using Blog.Data.Contracts;
using Blog.Data.Interface;
using Blog.Data.Model;
using System;

namespace Blog.Data
{

    public class Class1:IDependency
    {
        public IRepository<Blog_TestTable, int> TestTableRepository { set; get; }
        public ITestContract TestContract { get; set; }
        public void hello()
        {
            string s = "ddd";
        }
    }
}
