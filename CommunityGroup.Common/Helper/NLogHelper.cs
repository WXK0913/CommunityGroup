﻿using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGroup.Common
{
    public class NLogHelper
    {
        //每创建一个Logger都会有一定的性能损耗，所以定义静态变量
        private static Logger nLogger = LogManager.GetCurrentClassLogger();

        public static void Info(string msg)
        {
            nLogger.Info(msg);
        }

        public static void Error(string msg, Exception ex = null)
        {
            if (ex == null)
                nLogger.Error(msg);
            else
                nLogger.Error(ex, msg);
        }
    }
}
