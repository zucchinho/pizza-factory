namespace Ucas.TechTest.PizzaFactory.Model
{
    using System;

    public class Pizza : PizzaOrder, IPizza
    {
        public Pizza(
            IPizzaOrder pizzaOrder,
            double cookingTimeMs)
            : base(pizzaOrder, pizzaOrder.Topping)
        {
            this.CookingTime = TimeSpan.FromMilliseconds(cookingTimeMs);
        }

        public TimeSpan CookingTime { get; }

        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch(format)
            {
                case "C":
                    return $"{this.Name} w/ {this.Topping} cooked in {this.CookingTime.TotalMilliseconds}ms";
                case "U":
                    return $"{this.Name} w/ {this.Topping}";
                default:
                    throw new FormatException($"Unrecognized option: {format}. Use C for Cooked, or U for Uncooked.");
            }            
        }
    }
}
