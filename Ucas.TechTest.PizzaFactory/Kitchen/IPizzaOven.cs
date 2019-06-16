namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Model;

    public interface IPizzaOven
    {
        Task CookAsync(
            IPizzaOrder pizzaOrder,
            double cookingTimeMs,
            CancellationToken cancellationToken);
    }
}
