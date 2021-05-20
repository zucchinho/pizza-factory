using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ucas.TechTest.PizzaFactory.Core.Model;
using Ucas.TechTest.PizzaFactory.Kitchen;

namespace Ucas.TechTest.PizzaFactory.Consumers.Restaurant
{
    public class MultipleOvenKitchen : PizzaKitchen
    {
        private readonly BlockingCollection<IPizzaOven> _ovensBlock;

        public MultipleOvenKitchen(
            Func<IPizzaOven> ovenFactory,
            int ovenCount = 3) : base()
        {
            var ovens = Enumerable.Range(0, ovenCount)
                .Select(i => ovenFactory()).ToList();
            this._ovensBlock = new BlockingCollection<IPizzaOven>(
                new ConcurrentBag<IPizzaOven>(ovens),
                ovenCount);
        }

        protected override async Task ProcessAsyncImpl(
            IPizzaOrder pizzaOrder,
            double cookingTimeMs,
            CancellationToken cancellationToken)
        {
            IPizzaOven oven;
            
            bool ovenAvailable;
            do
            {
                ovenAvailable = this._ovensBlock.TryTake(
                    out oven,
                    500, 
                    cancellationToken);
            } while (!ovenAvailable);

            await oven.CookAsync(
                pizzaOrder,
                cookingTimeMs,
                cancellationToken);
            
            this._ovensBlock.Add(oven, cancellationToken);
        }
    }
}