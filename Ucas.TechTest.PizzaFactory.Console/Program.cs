namespace Ucas.TechTest.PizzaFactory.Console
{
    using NLog;
    using System;
    using System.Configuration;
    using System.Threading;
    using Ucas.TechTest.PizzaFactory.Kitchen;
    using Ucas.TechTest.PizzaFactory.Model;
    using Ucas.TechTest.PizzaFactory.Restaurant;
    using Unity;
    using Unity.Lifetime;

    /// <summary>
    /// Creates a pizzeria and caters to the pizza party
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var partySize = Convert.ToInt32(
                ConfigurationManager.AppSettings["PizzaFactory.PartySize"]);
            logger.Info(
                "Catering for party size: {0}",
                partySize);

            using(var container = new UnityContainer())
            using (var cts = new CancellationTokenSource())
            {
                ConfigureDependencies(container);

                logger.Trace(
                    "Creating a new instance of a pizzeria.");
                var pizzeria = container.Resolve<IPizzeria>();

                logger.Debug(
                    "Beginning to cater for the party");

                pizzeria.CaterAsync(
                    partySize,
                    cts.Token).Wait(
                    cts.Token);

                logger.Debug(
                    "Finished catering for the party");
            }
        }

        /// <summary>
        /// Configures the dependencies.
        /// </summary>
        /// <param name="container">The container.</param>
        private static void ConfigureDependencies(IUnityContainer container)
        {
            // Register the logger
            container.RegisterInstance(logger, new ContainerControlledLifetimeManager());

            // Register restaurant
            container.RegisterType<IPizzeria, Pizzeria>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPizzeriaWaiter, RpgWaiter>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPizzaMenu, DummyPizzaMenu>(new ContainerControlledLifetimeManager());

            // Register kitchen
            container.RegisterType<IPizzaKitchen, PizzaKitchen>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPizzaOven, FilePizzaOven>(new ContainerControlledLifetimeManager());
        }
    }
}
