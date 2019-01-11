using Blog.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Data.Services
{
    public class InitialEntityMapper
    {
        #region
        private static readonly ICollection<Assembly> _mapperAssemblies = new List<Assembly>();//用于存放mapper对象--未使用

        /// <summary>
        ///     加载实体映射对象程序集到集合-在Global中初始化
        /// </summary>
        /// <param name="assembly"></param>
        /// 问题：由于在测试form的progress.cs中初始化该函数 会出现当前函数中EntityMappers()方法里找不到EntityConfigurationBase类行程序集
        /// 解决方法：第52行代码，在当前运行程序中获取当前运行程序集
        /// 原理：尚不清楚，初步认为是在程序入口中加载 与运行后加载 虽然是两个相同的程序集，但是 是两个不同的对象，故找不到
        public static void InitialMapperAssembly(Assembly assembly)
        {

            if (assembly != null)
            {
                if (_mapperAssemblies.Any(a => a == assembly)) return;
                _mapperAssemblies.Add(assembly);
            }
        }

        /// <summary>
        ///     初始化实体属性-外部调用
        /// </summary>
        public static Type[] Initial { get { return EntityMappers(); } }
        /// <summary>
        ///     初始化Mapper对象 调用对象 OnModelCreating
        /// </summary>
        /// <returns></returns>
        private static Type[] EntityMappers()//IEntityMapper
        {
            try
            {
                //设置 IEntityMapper 类型
                Type baseType = typeof(IEntityMapper);
                Type baseType2 = typeof(IEntityMapper);
                //获取当前程序集中所有的类型
                var allTypes = Assembly.GetExecutingAssembly().GetTypes();
                //获取当前程序集中 IEntityMapper 类型
                var mapperTypes = allTypes
                    .Where(type => baseType.IsAssignableFrom(type) && type != baseType
                        && !type.IsAbstract).ToArray();
                //设置 EntityBase<int> 类型
                baseType = typeof(EntityBase<int>);
                baseType2 = typeof(EntityBase<string>);
                //在当前程序集中 找出EntityBase<int> 类型 的model
                var models = allTypes.Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
                var models2 = allTypes.Where(type => baseType2.IsAssignableFrom(type) && type != baseType2 && !type.IsAbstract).ToArray();
                ///申明结果集
                //List<IEntityMapper> result = new List<IEntityMapper>();
                //将model中转化为IEntityMapper类型 存入结果集中
                //foreach (var model in models)
                //{
                //    result.AddRange(mapperTypes.Select(type =>
                //    Activator.CreateInstance(type.MakeGenericType(model, typeof(int))) as IEntityMapper).ToArray());
                //    //难点  Activator.CreateInstance作用为动态创建一个类的实例，MakeGenericType为泛型类指定类型
                //}
                //foreach (var model in models2)
                //{
                //    result.AddRange(mapperTypes.Select(type =>
                //    Activator.CreateInstance(type.MakeGenericType(model, typeof(string))) as IEntityMapper).ToArray());
                //    //难点  Activator.CreateInstance作用为动态创建一个类的实例，MakeGenericType为泛型类指定类型
                //}
                return models;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


#endregion
    }
}
