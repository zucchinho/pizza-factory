using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Test
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using NLog;
    using Ucas.TechTest.PizzaFactory.Kitchen;

    [TestClass]
    public class PizzaKitchenTest
    {
        private static readonly Mock<IPizzaOven> MockPizzaOven = new Mock<IPizzaOven>();
        private static readonly Mock<ILogger> MockLogger = new Mock<ILogger>();

        [TestMethod]
        public async Task ProcessOrderAsync_Success()
        {
            // Assume
            var pizzaBase = new PizzaBase("Gluten-Free", 2.5);
            var pizzaOrder = new PizzaOrder(
                pizzaBase,
                "Tuna");
            var dummyToken = CancellationToken.None;
            double expectedCookingTimeMs = (2.5 * 3000) + (4 * 100);

            MockPizzaOven.Setup(
                o => o.CookAsync(
                    It.IsAny<IPizzaOrder>(),
                    It.IsAny<double>(),
                    It.IsAny<CancellationToken>()))
                .Returns(
                Task.FromResult(true));

            // Act
            var pizzaKitchen = new SingleOvenKitchen(
                MockPizzaOven.Object,
                MockLogger.Object);
            await pizzaKitchen.ProcessOrderAsync(
                pizzaOrder,
                dummyToken);

            // Assert
            MockPizzaOven.Verify(
                o => o.CookAsync(
                    pizzaOrder,
                    expectedCookingTimeMs,
                    dummyToken),
                Times.Once);
            MockLogger.Verify(
                l => l.Debug(
                    "Calculated total cooking time for pizza: {0}ms",
                    expectedCookingTimeMs),
                Times.Once);
        }
    }
}
