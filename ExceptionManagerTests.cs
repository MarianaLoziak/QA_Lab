using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    [TestFixture]
    class ExceptionManagerTests
    {

        [TestCaseSource(typeof(CasesClass), nameof(CasesClass.CriticalCases))]
        public void IsExceptionCritical_CriticalException_ReturnsTrue(Exception e)
        {
            ExceptionManager exceptionManager = new ExceptionManager();//arrange

            bool result = exceptionManager.IsExceptionCritical(e);//act

            Assert.IsTrue(result);//assert
        }

        [TestCaseSource(typeof(CasesClass), nameof(CasesClass.NotCriticalCases))]
        public void IsExceptionCritical_NotCriticalException_ReturnsFalse(Exception e)
        {
            ExceptionManager exceptionManager = new ExceptionManager();//arrange

            bool result = exceptionManager.IsExceptionCritical(e);//act

            Assert.IsFalse(result);//assert
        }

        [TestCaseSource(typeof(CasesClass), nameof(CasesClass.CriticalCases))]
        public void HandleExceptionMethod_CriticalException_IncrementCriticalCounter(Exception e)
        {
            ExceptionManager exceptionManager = new ExceptionManager();
            int count_exc = 0;
            int count_critical = 0;

            exceptionManager.HandleExceptionMethod(e, ref count_critical, ref count_exc);

            Assert.AreEqual(count_critical, 1);
            Assert.AreEqual(count_exc, 0);
        }

        [TestCaseSource(typeof(CasesClass), nameof(CasesClass.NotCriticalCases))]
        public void HandleExceptionMethod_NotCriticalException_IncrementExceptionCounter(Exception e)
        {
            ExceptionManager exceptionManager = new ExceptionManager();
            int count_exc = 0;
            int count_critical = 0;

            exceptionManager.HandleExceptionMethod(e, ref count_critical, ref count_exc);

            Assert.AreEqual(count_critical, 0);
            Assert.AreEqual(count_exc, 1);
        }



    }

    public class CasesClass
    {
        public static object[] CriticalCases =
        {
            new ArgumentOutOfRangeException(),
            new NullReferenceException(),
            new IndexOutOfRangeException(),
            new OutOfMemoryException()
        };

        public static object[] NotCriticalCases =
        {
            new ArgumentNullException(),
            new DivideByZeroException(),
            new SystemException(),
            new FormatException(),
            new KeyNotFoundException()


        };
    }

}