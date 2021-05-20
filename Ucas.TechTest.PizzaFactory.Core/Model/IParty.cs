namespace Ucas.TechTest.PizzaFactory.Core.Model
{
public interface IParty
    {
        string Id { get; }
        int TableNumber { get; }
        int Size { get; }
    }
}