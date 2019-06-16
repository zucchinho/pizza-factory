namespace Ucas.TechTest.PizzaFactory.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public class Program
    {
        private const double BaseTimeMs = 3000;
        private const double CookingIntervalMs = 1000;
        private const double TimePerToppingLetterMs = 100;
        private const int PizzasRequired = 5;
        private const string OutputFilePath = @"E:\Lew\Documents\CookedPizzas.txt";

        private static readonly IReadOnlyDictionary<string, double> PizzaBaseCookingTimeMultipliers = new Dictionary<string, double>
        {
            { "Deep Pan", 2 },
            { "Stuffed Crust", 1.5},
            { "Thin and Crispy", 2 }
        };

        private static readonly IList<string> Toppings = new List<string>
        {
            "Ham and Mushroom",
            "Pepperoni",
            "Vegetable"
        };

        private static readonly IList<string> CookedPizzas = new List<string>();

        static void Main(string[] args)
        {
            var pizzaCount = 0;

            var pizzaBases = PizzaBaseCookingTimeMultipliers.Keys.ToList();
            var rnd = new Random();
            var basesCount = pizzaBases.Count;
            var toppingsCount = Toppings.Count;

            while(pizzaCount < PizzasRequired)
            {
                double cookingTimeMs;

                // Get random base
                var pizzaBase = pizzaBases[rnd.Next(basesCount)];
                var baseMultiplier = PizzaBaseCookingTimeMultipliers[pizzaBase];

                // Get random topping
                var topping = Toppings[rnd.Next(toppingsCount)];

                // Generate pizza description
                var pizzaDesc = $"{pizzaBase} w/ {topping}";

                // Calculate cooking time
                cookingTimeMs = baseMultiplier * BaseTimeMs;
                var toppingMultiplier = Convert.ToDouble(
                    topping.Count(c => char.IsLetter(c)));

                cookingTimeMs += (toppingMultiplier * TimePerToppingLetterMs);

                // Simulate cooking
                Thread.Sleep(
                    TimeSpan.FromMilliseconds(cookingTimeMs));

                CookedPizzas.Add(pizzaDesc);

                // Wait cooking interval
                Thread.Sleep(
                    TimeSpan.FromMilliseconds(CookingIntervalMs));

                // Increment counter
                ++pizzaCount;
            }

            File.WriteAllLines(
                OutputFilePath,
                CookedPizzas);
        }
    }
}
