using Blog.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Data.Services
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }
    }
}
