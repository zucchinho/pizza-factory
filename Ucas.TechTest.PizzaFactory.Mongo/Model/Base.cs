using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Mongo.Model
{
    [BsonIgnoreExtraElements(true)]
    public class Base : IPizzaBase
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonDefaultValue(1)]
        [BsonElement("multiplier")]
        public double Multiplier { get; set; }
    }
}