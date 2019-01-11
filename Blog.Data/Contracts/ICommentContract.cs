using Blog.Data.Interface;
using Blog.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Contracts
{
    public interface ICommentContract: IDependency
    {
        /// <summary>
        /// 加载留言
        /// </summary>
        /// <param name="nums">页数</param>
        /// <returns></returns>
        DataResult LoadComment(int nums = 1);
    }
}
