using Moq;
using RouteSummitTask.BLL.Dtos;
using RouteSummitTask.BLL.Interfaces;
using RouteSummitTask.BLL.Services;
using RouteSummitTask.DAL.Entities;

namespace RouteSummitTask.Test
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly OrderService _orderService;
        public OrderServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _orderService = new OrderService(_mockUnitOfWork.Object);
        }

        [Theory]
        [InlineData(50, 3, 7.5)]
        [InlineData(100, 3, 30)]
        [InlineData(40, 2, 0)]
        public void CalculateDiscountAmount_ShouldReturnCorrectDiscount(decimal productPrice, int quantity, decimal expected)
        {
            //Act

            var actual = _orderService.CalculateDiscountAmount(productPrice, quantity);

            //Asert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateOrder_ShouldCreateOrderSuccessfully()
        {
            // Arrange
            int customerId = 1;
            var model = new CreateOrderDto
            {
                PaymentMethod = PaymentMethod.Card,
                OrderItems = new List<CreateOrderItemDto>
                {
                    new() { ProductId = 1, Quantity = 2 }
                }
            };
            var product = new Product { Id = 1, Name = "Product1", Price = 100, Stock = 10 };

            _mockUnitOfWork.Setup(u => u.ProductRepository.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            // Act
            var order = await _orderService.CreateOrder(customerId, model);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(model.PaymentMethod, order.PaymentMethod);
            Assert.Single(order.OrderItems);
            Assert.Equal(product.Id, order.OrderItems.First().ProductId);
            Assert.Equal(2, order.OrderItems.First().Quantity);
        }

    }
}