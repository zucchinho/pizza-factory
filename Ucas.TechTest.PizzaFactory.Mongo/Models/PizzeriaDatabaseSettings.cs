namespace Ucas.TechTest.PizzaFactory.Mongo.Models
{
    public class PizzeriaDatabaseSettings : IPizzeriaDatabaseSettings
    {
        public string OrdersCollectionName { get; set; }
        public string PartiesCollectionName { get; set; }
        public string ToppingsCollectionName { get; set; }
        public string BasesCollectionName { get; set; }
        public string ConnectionString { get; set; }    
        public string DatabaseName { get; set; }    
    }
}