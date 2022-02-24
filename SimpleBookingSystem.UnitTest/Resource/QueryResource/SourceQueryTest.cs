using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleBookingSystem.UnitTest.Resource
{
    public class SourceQueryTest
    {
        [Fact]
        public async Task GetResources_ReturnListOfResources()
        {
            //Arrange
            var resourceService = TestFactories.ResourceServiceTestFactory();

            //Act
            var resources = await resourceService.GetResourcesAsync();

            //Assert
            Assert.NotNull(resources);
            Assert.True(resources.Count == 3);
        }

    }
}
