using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Interface
{
    public class DataResult<TData>
    {
        //数据主体
        public TData Data { get; set; }
        //执行信息
        public string Message { get; set; }
        //执行标志 True为成功 反而反之
        public bool Success { get; set; }
    }
}
