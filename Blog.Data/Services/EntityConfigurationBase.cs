using Blog.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Services
{
    public class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>, IEntityMapper
        where TEntity : EntityBase<int>
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
           
            throw new NotImplementedException();
        }

        public void RegistTo(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TEntity>(this);
        }
    }
}
