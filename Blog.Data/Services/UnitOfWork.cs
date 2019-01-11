using Blog.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {


        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        //属性方式注入
        //public DbContext Context { get; set; }
        //无法修改DbContext使其继承IDependency空接口，导致其不能进行注入。 
        //想法：DbContextBase 可继承IUnitOfWork接口，但这样需修改仓储，单元控制和上下文中的代码
        public DbContext Context = new EFContext();
        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted { get; private set; }

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public DataResult Commit()
        {
            if (IsCommitted)
            {
                //return 0;
                return DataProcess.Failure("当前操作已提交");
            }
            try
            {
                int result = Context.SaveChanges();
                IsCommitted = true;
                //return result;
                //这边可能还 需要添加result的结果判断
                return DataProcess.Success(result);
            }
            catch (Exception es)
            {
                return DataProcess.Failure(es.Message);
            }
        }

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            IsCommitted = false;
        }

        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }
            Context.Dispose();
        }

        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        public DbSet<TEntity> Set<TEntity, Tkey>() where TEntity : EntityBase<Tkey>
        {
            return Context.Set<TEntity>();
        }

        /// <summary>
        ///     注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            IsCommitted = false;
        }

        /// <summary>
        ///     批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<TKey>
        {
                foreach (TEntity entity in entities)
                {
                    RegisterNew<TEntity, TKey>(entity);
                }
        }

        /// <summary>
        ///     注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterModified<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<TEntity>().Attach(entity);
            }
            Context.Entry(entity).State = EntityState.Modified;
            IsCommitted = false;
        }

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TEntity, TKey>(TEntity entity) where TEntity : EntityBase<TKey>
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;
        }

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : EntityBase<TKey>
        {
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted<TEntity, TKey>(entity);
                }
        }
    }
}
