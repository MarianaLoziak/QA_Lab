using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentOutOfRangeException ar = new ArgumentOutOfRangeException();
            ExceptionCritical exc = new ExceptionCritical();
            ExceptionManager manager = new ExceptionManager(exc);
            Console.WriteLine(manager.IsExceptionCritical(ar));
            Console.ReadLine();
        }

 
    }
}
