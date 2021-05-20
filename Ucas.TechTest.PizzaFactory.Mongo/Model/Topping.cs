using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ucas.TechTest.PizzaFactory.Mongo.Model
{
    [BsonIgnoreExtraElements(true)]
    public class Topping
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}