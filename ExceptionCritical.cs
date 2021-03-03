using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class ExceptionCritical:IExceptionCritical
    {
        public bool IsExceptionCritical(Exception e)
        {
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;

            foreach (string s in sAll.AllKeys)
                if (sAll.Get(s) == e.GetType().ToString())
                {
                    return true;
                }


            return false;
        }
    }
}
