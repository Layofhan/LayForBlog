using Blog.Data.Contracts;
using Blog.Data.Interface;
using Blog.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Data.Services
{
    public class CommentService : ICommentContract
    {
        public IRepository<Blog_TestTable, int> TestTableRepository { set; get; }
        public ITestContract TestContract { get; set; }
        public IQueryable<Blog_TestTable> TestTable
        {
            get { return TestTableRepository.Entities; }
        }
        public DataResult LoadComment(int nums = 1)
        {
            Blog_TestTable n = new Blog_TestTable();
            n.Name = "s";
            n.PassWord = "sdf";
            TestTableRepository.Insert(n);
            var list = TestTable.ToList();
            return DataProcess.Success(list);
        }
    }
}
