using Blog.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Data.Model
{
    [Table("Blog_TestTable")]
    public class Blog_TestTable :EntityBase<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        //[Key]
        //public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
