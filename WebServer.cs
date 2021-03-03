using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class WebServer:IWebServer
    {
        public bool SendCriticalReport(Exception cr_exception)
        {
            String message = cr_exception.ToString();
            //надсилання повідомлення на сервер
            return false;
        }
    }
}
