using Domain.Models;
using FullStackCodingChallenge.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCodingChallenge.Tests.Controllers
{


    public class ItemsControllerTests
    {
        private readonly Mock<IItemService> _mockItemService;
        private readonly ItemsController _controller;

        public ItemsControllerTests()
        {
            _mockItemService = new Mock<IItemService>();
            _controller = new ItemsController(_mockItemService.Object);  // Usamos el mock del servicio directamente
        }

        [Fact]
        public async Task GetAllItems_ShouldReturnOk_WhenItemsExist()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Test Item", Description = "This is a description of the item" },
                new Item { Id = 4, Name = "test3", Description = "test3" }
            };
            _mockItemService.Setup(service => service.GetAllItemsAsync()).ReturnsAsync(items);

            // Act
            var result = await _controller.GetAllItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItems = Assert.IsType<List<Item>>(okResult.Value);
            Assert.Equal(2, returnedItems.Count);
        }

        [Fact]
        public async Task GetItemById_ShouldReturnNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            _mockItemService.Setup(service => service.GetItemByIdAsync(99)).ReturnsAsync((Item)null);

            // Act
            var result = await _controller.GetItemById(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
