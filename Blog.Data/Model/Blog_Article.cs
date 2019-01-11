using Blog.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Model
{
    public class Blog_Article : EntityBase<int>
    {
        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 博客文章
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 文章地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int Praise { get; set; }
        /// <summary>
        /// 阅读数量
        /// </summary>
        public int ReadCounts { get; set; }
    }
}
