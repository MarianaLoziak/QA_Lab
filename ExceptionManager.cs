using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class ExceptionManager
    {
        private IExceptionCritical critical;
        private IWebServer server;
        private int exc_count;
        private int critical_count;
        private int fail_report_count;

        public  int GetExc_Count()
        {
            return exc_count;
        }

        public int GetCritical_count()
        {
            return critical_count;
        }

        public int GetFail_count()
        {
            return fail_report_count;
        }

        // ExceptionCritical передаємо через конструктор, WebServer - через властивість
        public ExceptionManager(IExceptionCritical exc_cr)//через конструктор
        {
            critical = exc_cr;
            server = new WebServer();//через властивість
            exc_count = 0;
            critical_count = 0;
            fail_report_count = 0;
        }

        public ExceptionManager()//залежність через фабричний клас
        {
            critical = ExceptionCriticalFactory.Create();

        }


        public IWebServer Server
        {
            get { return server; }
            set { server = value; }
        }

        public bool IsExceptionCritical(Exception e)
        {
            return critical.IsExceptionCritical(e);
        }

        public void ManageException(Exception e)
        {
            if (IsExceptionCritical(e))
            {
                critical_count++;
                if (!server.SendCriticalReport(e)){
                    fail_report_count++;
                }
            }
            else
            {
                exc_count++;
            }
        }



    }
}
