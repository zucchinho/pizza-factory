namespace Ucas.TechTest.PizzaFactory.Restaurant
{
    using Ucas.TechTest.PizzaFactory.Model;

    public interface IPizzeriaWaiter
    {
        IPizzaOrder GetNextOrder();
    }
}
