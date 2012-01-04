using System;
using LoggerLibrary;
using Moq;
using NUnit.Framework;
using Ninject;

namespace LoggerTests
{
    [TestFixture]
    class LogFileManagerTests
    {
        private DateTime _now;

        private StandardKernel _kernel;
        private IManageConfiguration _configurationManager;
        private IManageFiles _fileManager;
        private IManageLogFiles _logFileManager;
        private IManageLogFiles _logFileManagerLocalNow;

        private Mock<IProvideDateTime> _mockDateTimeProvider;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _mockDateTimeProvider = new Mock<IProvideDateTime>();

            _now = DateTime.Now;

            _mockDateTimeProvider.Setup(dtp => dtp.Now).Returns(_now);
            _mockDateTimeProvider.Setup(dtp => dtp.GetDateTimeStamp(_now)).Returns("NOW");

            _kernel = new StandardKernel();

            _kernel.Bind<IReadConfigurationFile>().
                To<ReadConfigurationFile>();

            _kernel.Bind<IManageFiles>().
                To<ManageFiles>();

            _kernel.Bind<IProvideDateTime>().
                To<ProvideDateTime>();

            _kernel.Bind<IManageConfiguration>().
                To<ManageConfiguration>();

            _kernel.Bind<IManageLogFiles>().
                To<ManageLogFiles>().
                Named("RealNow");

            _kernel.Bind<IManageLogFiles>().
                To<ManageLogFiles>().
                Named("LocalNow").
                WithConstructorArgument("dateTimeProvider", _mockDateTimeProvider.Object);

            _configurationManager = _kernel.Get<IManageConfiguration>();
            _fileManager = _kernel.Get<IManageFiles>();
            _logFileManager = _kernel.Get<IManageLogFiles>("RealNow");
            _logFileManagerLocalNow = _kernel.Get<IManageLogFiles>("LocalNow");
        }

        [SetUp]
        public void SetUp()
        {
            if (_logFileManager.Exists())
                _logFileManager.Delete();
        }

        [TearDown]
        public void TearDown()
        {
            if (_logFileManager.Exists())
                _logFileManager.Delete();
        }

        [Test]
        public void WriteLineWritesALine()
        {
            _logFileManager.WriteLine("This is a test.");

            Assert.IsTrue(_logFileManager.Exists());
        }

        [Test]
        public void CanCheckIfLogFileExists()
        {
            _logFileManager.WriteLine("This is a test.");

            Assert.IsTrue(_logFileManager.Exists());
        }

        [Test]
        public void CanDeleteLogFile()
        {
            _logFileManager.Delete();

            _logFileManager.WriteLine("This is a test.");

            _logFileManager.Delete();

            Assert.IsFalse(_logFileManager.Exists());
        }

        [Test]
        public void CanArchiveLogFile()
        {
            var archiveFileName = _logFileManagerLocalNow.GetArchiveFileName(_now);

            _logFileManagerLocalNow.WriteLine("This is a test.");
            _logFileManagerLocalNow.Archive();

            Assert.IsTrue(_fileManager.Exists(archiveFileName));
            Assert.IsFalse(_fileManager.Exists(_configurationManager.FileName));

            _fileManager.Delete(archiveFileName);
        }

        [Test]
        public void WontArchiveIfLogFileDoesntExist()
        {
            var archiveFileName = _logFileManagerLocalNow.GetArchiveFileName(_now);

            _logFileManagerLocalNow.WriteLine("This is a test.");
            _logFileManager.Delete();
            _logFileManagerLocalNow.Archive();

            Assert.IsFalse(_fileManager.Exists(archiveFileName));
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Archive file already exists.")]
        public void FailOnArchiveFileExists()
        {
            var archiveFileName = _logFileManagerLocalNow.GetArchiveFileName(_now);

            try
            {
                _logFileManagerLocalNow.WriteLine("This is a test.");
                _logFileManagerLocalNow.Archive();
                _logFileManagerLocalNow.WriteLine("This is a test.");
                _logFileManagerLocalNow.Archive();
            }
            finally
            {
                _fileManager.Delete(archiveFileName);
            }
        }
    }
}