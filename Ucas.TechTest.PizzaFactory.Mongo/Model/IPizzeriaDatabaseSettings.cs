namespace Ucas.TechTest.PizzaFactory.Mongo.Model
{
    public interface IPizzeriaDatabaseSettings
    {
        string OrdersCollectionName { get; set; }
        string PartiesCollectionName { get; set; }
        string ToppingsCollectionName { get; set; }
        string BasesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}