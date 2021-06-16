using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGroup.IRepository
{
    public interface IBaseRepository<T> where T :class,new()
    {
        //查询数据集合
        List<T> GetTs();

        int Add(T t);
        int Del(int id);
        int Upd(T t);

    }
}
