namespace Ucas.TechTest.PizzaFactory.Console
{
    using System;
    using System.Configuration;
    using System.Threading;
    using Ucas.TechTest.PizzaFactory.Kitchen;
    using Ucas.TechTest.PizzaFactory.Model;
    using Ucas.TechTest.PizzaFactory.Restaurant;
    using Unity;
    using Unity.Lifetime;

    public class Program
    {
        static void Main(string[] args)
        {
            var partySize = Convert.ToInt32(
                ConfigurationManager.AppSettings["PizzaFactory.PartySize"]);

            using(var container = new UnityContainer())
            using(var cts = new CancellationTokenSource())
            {
                ConfigureDependencies(container);

                var pizzeria = container.Resolve<IPizzeria>();

                pizzeria.CaterAsync(
                    partySize,
                    cts.Token).Wait(
                    cts.Token);
            }
        }

        private static void ConfigureDependencies(IUnityContainer container)
        {
            container.RegisterType<IPizzeria, Pizzeria>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPizzeriaWaiter, RpgWaiter>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPizzaMenu, DummyPizzaMenu>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPizzaKitchen, PizzaKitchen>(new ContainerControlledLifetimeManager());

            container.RegisterType<IPizzaOven, FilePizzaOven>(new ContainerControlledLifetimeManager());
        }
    }
}
