using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ucas.TechTest.PizzaFactory.Core.Model;

namespace Ucas.TechTest.PizzaFactory.Mongo.Model
{
    public class Party : IParty
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }

        public int TableNumber { get; set; }
        public int Size { get; set; }
    }
}