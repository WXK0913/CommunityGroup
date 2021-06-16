using System;
using System.Collections.Generic;
using CommunityGroup.IRepository;
using CommunityGroup.Common;
using System.Linq;
using System.Linq.Expressions;

namespace CommunityGroup.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly Context _oAContext;

        public BaseRepository()
        {
            _oAContext = new Context();
        }
        public BaseRepository(Context oAContext)
        {
            _oAContext = oAContext;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Add(T t)
        {
            _oAContext.Set<T>().Add(t);
            return _oAContext.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">根据参数id删除</param>
        /// <returns></returns>
        public int Del(int id)
        {
            T t = _oAContext.Set<T>().Find(id);
            _oAContext.Set<T>().Remove(t);
            return _oAContext.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Upd(T t)
        {
            _oAContext.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _oAContext.SaveChanges();
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetTs()
        {
            return _oAContext.Set<T>().ToList();
        }
        /// <summary>
        /// 带查询条件
        /// </summary>
        /// <param name="lambada"></param>
        /// <returns></returns>
        public List<T> GetTs(Expression<Func<T, bool>> lambada)
        {
            return _oAContext.Set<T>().Where(lambada).ToList();
        }
    }
}
