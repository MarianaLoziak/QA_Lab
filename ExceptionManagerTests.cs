using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    [TestFixture]
    class ExceptionManagerTests
    {
        ArgumentNullException test = new ArgumentNullException();
        [Test]
        public void IsExceptionCritical_CriticalException_ReturnsTrue()
        {
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = true;
            ExceptionManager exceptionManager = new ExceptionManager(fakeException);

            bool result = exceptionManager.IsExceptionCritical(test);

            Assert.IsTrue(result);

        }

        [Test]
        public void IsExceptionCrititcal_NotCriticalException_ReturnsFalse()
        {
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = false;
            ExceptionManager exceptionManager = new ExceptionManager(fakeException);

            bool result = exceptionManager.IsExceptionCritical(test);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsExceptionCritical_Critical_ReturnsTrue()//через фабричний клас
        {
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = true;
            ExceptionCriticalFactory.SetException(fakeException);
            ExceptionManager exceptionManager = new ExceptionManager();


            bool result = exceptionManager.IsExceptionCritical(test);

            Assert.IsTrue(result);

        }

        [Test]
        public void ManageException_CriticalException_CallServer()
        {
            IWebServer web = Substitute.For<IWebServer>();
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = true;
            ExceptionManager exceptionManager = new ExceptionManager(fakeException);
            exceptionManager.Server = web;

            exceptionManager.ManageException(test);
            
            web.Received().SendCriticalReport(test);

        }

        [Test]
        public void ManageException_NotCriticalException_DoNotCallServer()
        {
            IWebServer web = Substitute.For<IWebServer>();
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = false;
            ExceptionManager exceptionManager = new ExceptionManager(fakeException);
            exceptionManager.Server = web;

            exceptionManager.ManageException(test);

            web.DidNotReceive().SendCriticalReport(test);

        }



        [Test]
        public void ManageException_CriticalReportFailed_AddFailCount()
        {
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = true;
            FakeWebServer fakeWebServer = new FakeWebServer();
            fakeWebServer.ResultOfSending = false;
            ExceptionManager exceptionManager = new ExceptionManager(fakeException);
            exceptionManager.Server = fakeWebServer;

            exceptionManager.ManageException(test);

            Assert.AreEqual(exceptionManager.GetFail_count(), 1);
            Assert.AreEqual(exceptionManager.GetCritical_count(), 1);
            Assert.AreEqual(exceptionManager.GetExc_Count(), 0);
        }

        [Test]
        public void ManageException_CriticalReportSuccied_AddCriticalCount()
        {
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = true;
            FakeWebServer fakeWebServer = new FakeWebServer();
            fakeWebServer.ResultOfSending = true;
            ExceptionManager exceptionManager = new ExceptionManager(fakeException);
            exceptionManager.Server = fakeWebServer;

            exceptionManager.ManageException(test);

            Assert.AreEqual(exceptionManager.GetFail_count(), 0);
            Assert.AreEqual(exceptionManager.GetCritical_count(), 1);
            Assert.AreEqual(exceptionManager.GetExc_Count(), 0);
        }

        [Test]
        public void ManageException_NotCritical_AddExceptionCount()
        {
            FakeExceptionCritical fakeException = new FakeExceptionCritical();
            fakeException.WillBeCritical = false;
            FakeWebServer fakeWebServer = new FakeWebServer();
            ExceptionManager exceptionManager = new ExceptionManager(fakeException);
            exceptionManager.Server = fakeWebServer;

            exceptionManager.ManageException(test);

            Assert.AreEqual(exceptionManager.GetFail_count(), 0);
            Assert.AreEqual(exceptionManager.GetCritical_count(), 0);
            Assert.AreEqual(exceptionManager.GetExc_Count(), 1);
        }



    }
    class FakeExceptionCritical : IExceptionCritical
    {
        public bool WillBeCritical;

        public bool IsExceptionCritical(Exception e)
        {
            return WillBeCritical;
        }
    }

    class FakeWebServer : IWebServer
    {
        public bool ResultOfSending;

        public bool SendCriticalReport(Exception e)
        {
            return ResultOfSending;
        }
    }
}
