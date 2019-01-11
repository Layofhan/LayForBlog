using Blog.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Model
{
    public class Blog_About: EntityBase<int>
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int Praise { get; set; }
        /// <summary>
        /// 阅读数量
        /// </summary>
        public int ReadNum { get; set; }
    }
}
