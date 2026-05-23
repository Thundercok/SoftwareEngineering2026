using WebMVC_Core.Models;
using Xunit;

namespace LabTests
{
    public class ItemTests
    {
        [Fact]
        public void CreateItem_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var expectedName = "Test Rolex";
            var expectedSize = "40mm";

            // Act
            var item = new Item 
            { 
                ItemID = 1, 
                ItemName = expectedName, 
                Size = expectedSize 
            };

            // Assert
            Assert.Equal(expectedName, item.ItemName);
            Assert.Equal(expectedSize, item.Size);
        }
    }
}