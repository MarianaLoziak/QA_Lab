using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class ExceptionManager
    {
        public List<String> CriticalExceptions = new List<String>()
        {
            "System.NullReferenceException" ,
            "System.ArgumentOutOfRangeException",
            "System.IndexOutOfRangeException",
            "System.OutOfMemoryException"
        };


        public bool IsExceptionCritical(Exception e)
        {
            String TypeOfException = e.GetType().ToString();
            foreach (String item in CriticalExceptions)
            {
                if (TypeOfException == item)
                    return true;
            }
            return false;

        }

        public void HandleExceptionMethod(Exception e, ref int CountCritical, ref int CountExceptional)
        {
            if (IsExceptionCritical(e))
            {
                CountCritical++;
            }
            else
            {
                CountExceptional++;
            }
        }

    }
}
