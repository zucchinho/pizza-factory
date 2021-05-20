using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ucas.TechTest.PizzaFactory.Mongo.Model
{
    public class Topping
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}