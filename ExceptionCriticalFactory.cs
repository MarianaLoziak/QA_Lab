using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class ExceptionCriticalFactory
    {
        private static IExceptionCritical customException = null;

        public static IExceptionCritical Create()
        {
            if (customException != null)
                return customException;
            return new ExceptionCritical();
        }

        public static void SetException(IExceptionCritical cr)
        {
            customException = cr;
        }
    }
}
