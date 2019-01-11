using Blog.Data.Interface;
using Blog.Data.Model;
using Blog.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class EFContext:DbContext
    {
        public static string ConString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"data source=.;initial catalog=LayForBlog;integrated security=false;persist security info=True;User ID=sa;Password=xgh523635212;");

            //配置数据链接
            optionsBuilder.UseSqlServer(ConString, b => b.UseRowNumberForPaging());
        }
        //初始化上下文时自动执行
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //获取实体配置信息
        //    Type[] entityMappers = InitialEntityMapper.Initial;
        //    //将实体配置信息 注册到上下文中
        //    //foreach (IEntityMapper mapper in entityMappers)
        //    //{
        //    //    //mapper.RegistTo(modelBuilder.Model);
        //    //}
        //    foreach (var model in entityMappers)
        //    {
        //        modelBuilder.Entity(model);
        //    }
        //}
        /// <summary>
        /// 测试
        /// </summary>
        public DbSet<Blog_TestTable> Blog_TestTable { get; set; }
        /// <summary>
        /// 留言
        /// </summary>
        public DbSet<Blog_Comment> Blog_Comment { get; set; }
        /// <summary>
        /// 博客
        /// </summary>
        public DbSet<Blog_Article> Blog_Article { get; set; }
        /// <summary>
        /// 文章评论
        /// </summary>
        public DbSet<Blog_ArticleComment> Blog_ArticleComment { get; set; }
        /// <summary>
        /// 关于
        /// </summary>
        public DbSet<Blog_About> Blog_About { get; set; }
    }
}
