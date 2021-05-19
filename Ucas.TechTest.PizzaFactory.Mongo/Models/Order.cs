using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Ucas.TechTest.PizzaFactory.Model;

namespace Ucas.TechTest.PizzaFactory.Mongo.Models
{
    public class Order : IOrder
    {
        [BsonId]
        [BsonRepresentation((BsonType.ObjectId))]
        public string Id { get; set; }
        
        [BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<
            IEnumerable<IPizzaOrder>, 
            IEnumerable<PizzaOrder>>)
        )]
        public IEnumerable<IPizzaOrder> Pizzas { get; set; }

        public string OrderNumber => this.Id;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}