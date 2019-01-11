using Blog.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Model
{
    public class Blog_ArticleComment : EntityBase<int>
    {
        /// <summary>
        /// 文章主键
        /// </summary>
        public int AricleId { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 评价点赞数量
        /// </summary>
        public int Praise { get; set; }
    }
}
