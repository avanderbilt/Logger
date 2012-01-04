using System;
using LoggerLibrary;
using Moq;
using NUnit.Framework;
using Ninject;

namespace LoggerTests
{
    [TestFixture]
    internal class ConfigurationManagerTests
    {
        private const string LogFileNameConfigurationKey = "LogFileName";
        private const string WindowOpacityConfigurationKey = "WindowOpacity";
        private const string DefaultIntervalConfigurationKey = "DefaultInterval";
        private const string LogFileName = "logfilename.txt";
        private const string WindowOpacity = ".777";
        private const string BadNumber = "IAMBAD";
        private const string DefaultInterval = "10";

        private Mock<IReadConfigurationFile> _mockConfigurationFileReader;
        private Mock<IReadConfigurationFile> _mockConfigurationFileReaderWithNullFileName;
        private Mock<IReadConfigurationFile> _mockConfigurationFileReaderWithNullWindowOpacity;
        private Mock<IReadConfigurationFile> _mockConfigurationFileReaderWithBadWindowOpacity;
        private Mock<IReadConfigurationFile> _mockConfigurationFileReaderWithNullDefaultInterval;
        private Mock<IReadConfigurationFile> _mockConfigurationFileReaderWithBadDefaultInterval;

        private StandardKernel _kernel;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            /* Good configuration parameters.
             */

            _mockConfigurationFileReader = new Mock<IReadConfigurationFile>();

            _mockConfigurationFileReader.Setup(mcfr => mcfr.ReadAppSetting(LogFileNameConfigurationKey)).Returns(
                LogFileName);
            _mockConfigurationFileReader.Setup(mcfr => mcfr.ReadAppSetting(WindowOpacityConfigurationKey)).Returns(
                WindowOpacity);
            _mockConfigurationFileReader.Setup(mcfr => mcfr.ReadAppSetting(DefaultIntervalConfigurationKey)).Returns(
                DefaultInterval);

            _kernel = new StandardKernel();

            _kernel.
                Bind<IManageConfiguration>().
                To<ManageConfiguration>().
                Named("GoodConfiguration").
                WithConstructorArgument("configurationFileReader", _mockConfigurationFileReader.Object);

            /* Null File Name
             */

            _mockConfigurationFileReaderWithNullFileName = new Mock<IReadConfigurationFile>();

            _mockConfigurationFileReaderWithNullFileName.Setup(mcfr => mcfr.ReadAppSetting(LogFileNameConfigurationKey))
                .Returns((string) null);
            _mockConfigurationFileReaderWithNullFileName.Setup(
                mcfr => mcfr.ReadAppSetting(WindowOpacityConfigurationKey)).Returns(
                    WindowOpacity);
            _mockConfigurationFileReaderWithNullFileName.Setup(mcfr => mcfr.ReadAppSetting(DefaultIntervalConfigurationKey)).Returns(
                DefaultInterval);

            _kernel.
                Bind<IManageConfiguration>().
                To<ManageConfiguration>().
                Named("NullFileNameConfiguration").
                WithConstructorArgument("configurationFileReader", _mockConfigurationFileReaderWithNullFileName.Object);

            /* Null Window Opacity
             */

            _mockConfigurationFileReaderWithNullWindowOpacity = new Mock<IReadConfigurationFile>();

            _mockConfigurationFileReaderWithNullWindowOpacity.Setup(
                mcfr => mcfr.ReadAppSetting(LogFileNameConfigurationKey)).Returns(LogFileName);
            _mockConfigurationFileReaderWithNullWindowOpacity.Setup(
                mcfr => mcfr.ReadAppSetting(WindowOpacityConfigurationKey)).Returns((string) null);
            _mockConfigurationFileReaderWithNullWindowOpacity.Setup(mcfr => mcfr.ReadAppSetting(DefaultIntervalConfigurationKey)).Returns(
                DefaultInterval);

            _kernel.
                Bind<IManageConfiguration>().
                To<ManageConfiguration>().
                Named("NullWindowOpacityConfiguration").
                WithConstructorArgument(
                    "configurationFileReader", _mockConfigurationFileReaderWithNullWindowOpacity.Object);

            /* Bad Window Opacity
             */

            _mockConfigurationFileReaderWithBadWindowOpacity = new Mock<IReadConfigurationFile>();

            _mockConfigurationFileReaderWithBadWindowOpacity.Setup(
                mcfr => mcfr.ReadAppSetting(LogFileNameConfigurationKey)).Returns(LogFileName);
            _mockConfigurationFileReaderWithBadWindowOpacity.Setup(
                mcfr => mcfr.ReadAppSetting(WindowOpacityConfigurationKey)).Returns(BadNumber);
            _mockConfigurationFileReaderWithBadWindowOpacity.Setup(mcfr => mcfr.ReadAppSetting(DefaultIntervalConfigurationKey)).Returns(
                DefaultInterval);

            _kernel.
                Bind<IManageConfiguration>().
                To<ManageConfiguration>().
                Named("BadWindowOpacityConfiguration").
                WithConstructorArgument("configurationFileReader",
                                        _mockConfigurationFileReaderWithBadWindowOpacity.Object);

            /* Null Default Interval
             */

            _mockConfigurationFileReaderWithNullDefaultInterval = new Mock<IReadConfigurationFile>();

            _mockConfigurationFileReaderWithNullDefaultInterval.Setup(
                mcfr => mcfr.ReadAppSetting(LogFileNameConfigurationKey)).Returns(LogFileName);
            _mockConfigurationFileReaderWithNullDefaultInterval.Setup(mcfr => mcfr.ReadAppSetting(WindowOpacityConfigurationKey)).Returns(
                WindowOpacity);
            _mockConfigurationFileReaderWithNullDefaultInterval.Setup(
                mcfr => mcfr.ReadAppSetting(DefaultIntervalConfigurationKey)).Returns((string) null);

            _kernel.
                Bind<IManageConfiguration>().
                To<ManageConfiguration>().
                Named("NullDefaultIntervalConfiguration").
                WithConstructorArgument(
                    "configurationFileReader", _mockConfigurationFileReaderWithNullDefaultInterval.Object);

            /* Bad Window Opacity
             */

            _mockConfigurationFileReaderWithBadDefaultInterval = new Mock<IReadConfigurationFile>();

            _mockConfigurationFileReaderWithBadDefaultInterval.Setup(
                mcfr => mcfr.ReadAppSetting(LogFileNameConfigurationKey)).Returns(LogFileName);
            _mockConfigurationFileReaderWithBadDefaultInterval.Setup(mcfr => mcfr.ReadAppSetting(WindowOpacityConfigurationKey)).Returns(
                WindowOpacity);
            _mockConfigurationFileReaderWithBadDefaultInterval.Setup(
                mcfr => mcfr.ReadAppSetting(DefaultIntervalConfigurationKey)).Returns(BadNumber);

            _kernel.
                Bind<IManageConfiguration>().
                To<ManageConfiguration>().
                Named("BadDefaultIntervalConfiguration").
                WithConstructorArgument("configurationFileReader",
                                        _mockConfigurationFileReaderWithBadDefaultInterval.Object);
        }

        [Test]
        public void CanReadAppSettings()
        {
            var configurationManager = _kernel.Get<IManageConfiguration>("GoodConfiguration");

            Assert.AreEqual(LogFileName, configurationManager.FileName);
            Assert.AreEqual(double.Parse(WindowOpacity), configurationManager.WindowOpacity);
        }

        [Test]
        [ExpectedException(typeof (Exception),
            ExpectedMessage = "The LogFileName configuration parameter was null or empty.")]
        public void FailOnNullFileName()
        {
            _kernel.Get<IManageConfiguration>("NullFileNameConfiguration");
        }

        [Test]
        [ExpectedException(typeof (Exception),
            ExpectedMessage = "The WindowOpacity configuration parameter was null or empty.")]
        public void FailOnNullWindowOpacity()
        {
            _kernel.Get<IManageConfiguration>("NullWindowOpacityConfiguration");
        }

        [Test]
        [ExpectedException(typeof (Exception),
            ExpectedMessage = "The value of the WindowOpacity configuration parameter is not a valid double.")]
        public void FailOnBadWindowOpacity()
        {
            _kernel.Get<IManageConfiguration>("BadWindowOpacityConfiguration");
        }

        [Test]
        [ExpectedException(typeof(Exception),
            ExpectedMessage = "The DefaultInterval configuration parameter was null or empty.")]
        public void FailOnNullDefaultInterval()
        {
            _kernel.Get<IManageConfiguration>("NullDefaultIntervalConfiguration");
        }

        [Test]
        [ExpectedException(typeof(Exception),
            ExpectedMessage = "The value of the DefaultInterval configuration parameter is not a valid integer.")]
        public void FailOnBadDefaultInterval()
        {
            _kernel.Get<IManageConfiguration>("BadDefaultIntervalConfiguration");
        }
    }
}