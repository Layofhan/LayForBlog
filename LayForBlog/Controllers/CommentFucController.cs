using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.Interface;
using Blog.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace LayForBlog.Controllers
{
    public class CommentFucController : Controller
    {
        public IRepository<Blog_Comment, int> CommentRepository { set; get; }

        public IRepository<Blog_Article, int> ArticleRepository { set; get; }

        public IRepository<Blog_ArticleComment, int> ArticleCommentRepository { set; get; }

        public IRepository<Blog_About, int> AboutRepository { set; get; }

        public IQueryable<Blog_Comment> Comment
        {
            get { return CommentRepository.Entities; }
        }

        public IQueryable<Blog_Article> Articles
        {
            get { return ArticleRepository.Entities; }
        }

        public IQueryable<Blog_ArticleComment> ArticleComment
        {
            get { return ArticleCommentRepository.Entities; }
        }

        public IQueryable<Blog_About> Abouts
        {
            get { return AboutRepository.Entities; }
        }

        public static int CurrentPage = 1;

        /// <summary>
        /// 加载留言
        /// </summary>
        /// <param name="limit">一页允许数量</param>
        /// <param name="page">当前页数</param>
        /// <returns></returns>
        public ActionResult LoadComment(int limit,int page)
        {
            try
            {
                var list = Comment.ToList();
                //list = list.Skip(limit * (page - 1)).Take(limit).ToList();
                return DataProcess.Success(list).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure("内部问题").ToMvcJson();
            }
        }
        /// <summary>
        /// 向数据库添加留言
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public ActionResult AddComment(string content)
        {
            try
            {
                Blog_Comment cont = new Blog_Comment();
                cont.Contents = content;
                cont.Praise = 0;
                CommentRepository.Insert(cont);
                return DataProcess.Success().ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure("内部问题").ToMvcJson();
            }
        }

        /// <summary>
        /// 加载文章
        /// </summary>
        /// <param name="limit">一页允许数量</param>
        /// <param name="page">当前页数</param>
        /// <returns></returns>
        [AuthorizationFilter(ActionName = "ACE")]
        public ActionResult LoadArticle(int limit = 10,int page = 1)
        {
            try
            {
                var list = Articles.ToList();
                foreach(var li in list)
                {
                    li.Url = "/Home/Details?id=" + li.Id;
                }
                return DataProcess.Success(list).ToMvcJson("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="url">图片路径</param>
        /// <returns></returns>
        public ActionResult AddArticle(string title,string content,string url)
        {
            try
            {
                Blog_Article article = new Blog_Article();
                article.Time = DateTime.Now;
                article.Contents = content;
                article.Pic = url;
                article.Title = title;
                ArticleRepository.Insert(article);
                return DataProcess.Success().ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 加载相应文章的详细
        /// </summary>
        /// <param name="id">文章的主键</param>
        /// <returns></returns>
        public ActionResult LoadArticleDetai(int id)
        {
            try
            {
                Blog_Article article = ArticleRepository.GetByKey(id);
                return DataProcess.Success(article).ToMvcJson("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 加载文章评论
        /// </summary>
        /// <param name="id">文章主键</param>
        /// <returns></returns>
        public ActionResult LoadArticleComment(int id)
        {
            try
            {
                var list = ArticleComment.Where(a => a.AricleId == id).ToList();

                return DataProcess.Success(list).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 添加文章评论
        /// </summary>
        /// <param name="id">文章主键</param>
        /// <param name="content">评论内容</param>
        /// <returns></returns>
        public ActionResult AddArticleComment(int id,string content)
        {
            try
            {
                Blog_ArticleComment articlecomment = new Blog_ArticleComment();
                articlecomment.AricleId = id;
                articlecomment.Contents = content;
                articlecomment.Praise = 0;
                ArticleCommentRepository.Insert(articlecomment);

                return DataProcess.Success().ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 加载关于
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadAbout()
        {
            try
            {
                Blog_About about = Abouts.FirstOrDefault();
                if (about != null)
                {
                    return DataProcess.Success(about).ToMvcJson();
                }
                else
                {
                    return DataProcess.Failure("没有内容").ToMvcJson();
                }
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 修改关于
        /// </summary>
        /// <param name="content">修改的内容</param>
        /// <returns></returns>
        public ActionResult ModifyAbout(string content)
        {
            try
            {
                Blog_About about = AboutRepository.GetByKey(1);
                about.Contents = content;
                AboutRepository.Update(about);

                return DataProcess.Success().ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
        /// <summary>
        /// 首页按钮 下一页
        /// </summary>
        /// <returns></returns>
        public ActionResult ArticleNext(int limit = 10)
        {
            try
            {
                var list = Articles.ToList();
                if(list.Count <= 10) { return DataProcess.Failure("无其他内容").ToMvcJson(); }
                CurrentPage++;
                list = list.Skip(limit * (CurrentPage - 1)).Take(limit).ToList();
                return DataProcess.Success(list).ToMvcJson();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message).ToMvcJson();
            }
        }
    }
}