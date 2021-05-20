using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Test
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using NLog;
    using Ucas.TechTest.PizzaFactory.Kitchen;
    using Ucas.TechTest.PizzaFactory.Restaurant;

    [TestClass]
    public class PizzeriaTest
    {
        private static readonly Mock<IPizzaKitchen> MockPizzaKitchen = new Mock<IPizzaKitchen>();
        private static readonly Mock<IPizzeriaWaiter> MockPizzeriaWaiter = new Mock<IPizzeriaWaiter>();
        private static readonly Mock<ILogger> MockLogger = new Mock<ILogger>();

        [TestMethod]
        public async Task CaterAsync_Success()
        {
            // Assume
            const int partySize = 3;
            var dummyToken = CancellationToken.None;
            var dummyOrders = new[]
            {
                new PizzaOrder("Gluten-Free", 2.5, "Tuna"),
                new PizzaOrder("Thin", 2.5, "Ham and Pineapple"),
                new PizzaOrder("Thick", 2.5, "Spinach"),
            };

            MockPizzeriaWaiter.SetupSequence(
                w => w.GetNextOrder())
                .Returns(dummyOrders[0])
                .Returns(dummyOrders[1])
                .Returns(dummyOrders[2]);

            MockPizzaKitchen.Setup(
                o => o.ProcessOrderAsync(
                    It.IsAny<IPizzaOrder>(),
                    It.IsAny<CancellationToken>()))
                .Returns(
                Task.FromResult(true));

            // Act
            var pizzeria = new Pizzeria(
                MockPizzeriaWaiter.Object,
                MockPizzaKitchen.Object,
                MockLogger.Object);
            await pizzeria.CaterAsync(                
                dummyToken);

            // Assert
            MockPizzeriaWaiter.Verify(
                w => w.GetNextOrder(),
                Times.Exactly(3));
            foreach (var dummyOrder in dummyOrders)
            {
                MockPizzaKitchen.Verify(
                    k => k.ProcessOrderAsync(
                        dummyOrder,
                        dummyToken),
                    Times.Once);
            }
            for (var i = 1; i <= partySize; i++)
            {
                var i2 = i;
                MockLogger.Verify(
                    l => l.Info(
                        "Beginning to process new order: {0}",
                        i2),
                    Times.Once);
                var i1 = i;
                MockLogger.Verify(
                    l => l.Info(
                        "Finished processing order: {0}",
                        i1),
                    Times.Once);
            }
        }
    }
}
