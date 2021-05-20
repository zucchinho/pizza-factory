using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Mongo.Model
{
    public class Base : IPizzaBase
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }

        public string Name { get; set; }
        
        public double Multiplier { get; set; }
    }
}