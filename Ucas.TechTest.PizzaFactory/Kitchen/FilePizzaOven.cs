namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Model;

    public class FilePizzaOven : IPizzaOven
    {
        private static readonly Lazy<string> OutputFilePathLazy = new Lazy<string>(
            () =>
            {
                return ConfigurationManager.AppSettings["FilePizzaOven.OutputFilePath"];
            });

        public async Task CookAsync(
            IPizzaOrder pizzaOrder, 
            double cookingTimeMs, 
            CancellationToken cancellationToken)
        {
            var pizza = new Pizza(
                pizzaOrder,
                cookingTimeMs);

            // Simulate cooking
            await Task.Delay(
                TimeSpan.FromMilliseconds(cookingTimeMs));

            using(var sw = new StreamWriter(OutputFilePathLazy.Value, true))
            {
                await sw.WriteLineAsync(pizza.ToString("C"));
            }
        }
    }
}
