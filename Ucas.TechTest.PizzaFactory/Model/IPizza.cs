namespace Ucas.TechTest.PizzaFactory.Model
{
    using System;

    public interface IPizza : IPizzaOrder, IFormattable
    {
        TimeSpan CookingTime { get; }

        string ToString(string format);
    }
}
