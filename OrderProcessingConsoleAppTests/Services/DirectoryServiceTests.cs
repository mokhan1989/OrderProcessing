using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Services;
using Xunit;

namespace OrderProcessingConsoleAppTests.Services
{
    public class DirectoryServiceTests
    {
        private readonly DirectoryService _directoryService;

        public DirectoryServiceTests()
        {
            _directoryService = new DirectoryService();
        }

        [Fact]
        public void ConverterService_ExtendsIDiscovertInterface()
        {
            Assert.IsAssignableFrom<IDirectoryService>(_directoryService);
        }

        [Fact]
        public void GetFilePath_ReturnsStringType()
        {
            var retrievedPath = _directoryService.GetFilePath("Test");

            Assert.IsType<string>(retrievedPath);
        }

        [Fact]
        public void GetFilePath_ReturnsFilePath()
        {
            var retrievedPath = _directoryService.GetFilePath("CountryList.json");

            Assert.NotEmpty(retrievedPath);
        }
    }
}
