using System.Collections.Generic;

namespace Ucas.TechTest.PizzaFactory.Model
{
    public interface IOrder
    {
        IEnumerable<IPizzaOrder> Pizzas { get; }
        string OrderNumber { get; }
        OrderStatus Status { get; }
    }
}