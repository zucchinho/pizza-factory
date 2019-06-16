namespace Ucas.TechTest.PizzaFactory.Model
{
    using System.Collections.Generic;

    public interface IPizzaMenu
    {
        IReadOnlyList<IPizzaBase> PizzaBases { get; }

        IReadOnlyList<string> Toppings { get; }
    }
}
