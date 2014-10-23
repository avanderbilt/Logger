using System;
using System.IO;
using LoggerLibrary;
using Ninject;
using NUnit.Framework;

namespace LoggerTests
{
    [TestFixture]
    class FileManagerTests : IDisposable
    {
        private const string FileName = "testfile.txt";
        private const string DestinationFileName = "destination.txt";

        private StandardKernel _kernel;
        private IManageFiles _fileManager;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<IManageFiles>().
                To<ManageFiles>();

            _fileManager = _kernel.Get<IManageFiles>();
        }

        [SetUp]
        public void SetUp()
        {
            using (File.Create(FileName))
            {
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_fileManager.Exists(FileName))
                _fileManager.Delete(FileName);

            if (_fileManager.Exists(DestinationFileName))
                _fileManager.Delete(DestinationFileName);
        }

        [Test]
        public void FileManagerCanCheckFileExists()
        {
            Assert.IsTrue(_fileManager.Exists(FileName));
        }

        [Test]
        public void FileManagerCanDeleteFile()
        {
            _fileManager.Delete(FileName);
            
            Assert.IsFalse(_fileManager.Exists(FileName));
        }

        [Test]
        public void FileManagerCanMoveFile()
        {
            _fileManager.Move(FileName, DestinationFileName);

            Assert.IsFalse(_fileManager.Exists(FileName));
            Assert.IsTrue(_fileManager.Exists(DestinationFileName));
        }

        [Test]
        public void ProgramDirectoryIsNotNull()
        {
            Assert.IsNotNullOrEmpty(_fileManager.ProgramDirectory);
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}