using System.Collections.Generic;

namespace Ucas.TechTest.PizzaFactory.Model
{
    public interface IOrder
    {
        IReadOnlyList<IPizza> Pizzas { get; }
        string OrderNumber { get; }
        OrderStatus Status { get; }
    }
}