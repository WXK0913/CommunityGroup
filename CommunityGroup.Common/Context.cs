using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGroup.Common
{
    //上下文
    public class Context:DbContext
    {
        //连接sqlserver数据库
        public string sqlConn=ConfigHelper.constr;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(sqlConn);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
