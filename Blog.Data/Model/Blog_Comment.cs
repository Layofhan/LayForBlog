using Blog.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Model
{
    public class Blog_Comment : EntityBase<int>
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Praise { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Contents { get; set; }
    }
}
