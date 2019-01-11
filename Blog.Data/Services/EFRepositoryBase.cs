using Blog.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.Data.Services
{
    public class EFRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        #region 属性

        public IUnitOfWork UnitOfWork { get; set; }

        public IUnitOfWork EFContext
        {
            get
            {
                if (UnitOfWork is IUnitOfWork)
                {
                    return UnitOfWork as IUnitOfWork;
                }
                throw new Exception(string.Format("数据仓储上下文对象类型不正确，应为IUnitOfWorkContext，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }
        /// <summary>
        /// 获取 当前单元操作对象
        /// </summary>

        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity, TKey>(); }
        }
        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Insert(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterNew<TEntity, TKey>(entity);
            return isSave ? EFContext.Commit() : null;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Delete(object id, bool isSave = true)
        {
            TEntity entity = EFContext.Set<TEntity, TKey>().Find(id);
            return entity != null ? Delete(entity, isSave) : null;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Delete(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterDeleted<TEntity, TKey>(entity);
            return isSave ? EFContext.Commit() : null;
        }


        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Update(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterModified<TEntity, TKey>(entity);
            return isSave ? EFContext.Commit() : null;
        }

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(object key)
        {
            return EFContext.Set<TEntity, TKey>().Find(key);
        }
        #endregion
    }
}
