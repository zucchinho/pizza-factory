namespace Ucas.TechTest.PizzaFactory.Model
{
    public class PizzaOrder : PizzaBase, IPizzaOrder
    {
        public PizzaOrder(
            string pizzaBaseName,
            double baseMultiplier,
            string topping)
            : base(pizzaBaseName, baseMultiplier)
        {
            this.Topping = topping;
        }

        public PizzaOrder(
            IPizzaBase pizzaBase,
            string topping)
            :base(pizzaBase.Name, pizzaBase.Multiplier)
        {
            this.Topping = topping;
        }

        public string Topping { get; }

        public string BaseName => this.Name;
    }
}
