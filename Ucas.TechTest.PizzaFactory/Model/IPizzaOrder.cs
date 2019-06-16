using System;
using System.Collections.Generic;
using System.Text;

namespace Ucas.TechTest.PizzaFactory.Model
{
    public interface IPizzaOrder : IPizzaBase
    {
        string Topping { get; }

        string BaseName { get; }
    }
}
