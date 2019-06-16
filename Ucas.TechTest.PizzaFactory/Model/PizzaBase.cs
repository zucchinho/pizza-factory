namespace Ucas.TechTest.PizzaFactory.Model
{
    public class PizzaBase : IPizzaBase
    {
        public PizzaBase(
            string name, 
            double multiplier)
        {
            this.Name = name;
            this.Multiplier = multiplier;
        }

        public string Name { get; }

        public double Multiplier { get; }
    }
}