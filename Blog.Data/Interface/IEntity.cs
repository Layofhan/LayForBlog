using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Interface
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
