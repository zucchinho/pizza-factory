using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Ucas.TechTest.PizzaFactory.Core.Model;
using Ucas.TechTest.PizzaFactory.DataAccess.Model;
using Ucas.TechTest.PizzaFactory.Mongo.Model;
using Ucas.TechTest.PizzaFactory.Restaurant;

namespace Ucas.TechTest.PizzaFactory.Mongo
{
    public class MongoDBService : IOrderWriter, IOrderUpdater, IOrdersReader, IOrderReader, IPartyWriter, IMenuReader
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Party> _parties;
        private readonly IMongoCollection<Topping> _toppings;
        private readonly IMongoCollection<Base> _bases;

        public MongoDBService(IPizzeriaDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this._orders = database.GetCollection<Order>(settings.OrdersCollectionName);
            this._parties = database.GetCollection<Party>(settings.PartiesCollectionName);
            this._toppings = database.GetCollection<Topping>(settings.ToppingsCollectionName);
            this._bases = database.GetCollection<Base>(settings.BasesCollectionName);
        }
        
        public Task<IOrder> CreateOrderAsync(IEnumerable<IPizzaOrder> orderItems)
        {
            return this.CreateOrderAsync(orderItems, CancellationToken.None);
        }

        public async Task<IOrder> CreateOrderAsync(IEnumerable<IPizzaOrder> orderItems, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                Pizzas = orderItems
            };
            
            await this._orders.InsertOneAsync(order, new InsertOneOptions(), cancellationToken);

            return order;
        }

        public IOrder CreateOrder(IEnumerable<IPizzaOrder> orderItems)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IOrder>> GetOrdersAsync(OrderStatus orderStatus)
        {
            return this.GetOrdersAsync(orderStatus, CancellationToken.None);
        }

        public async Task<IEnumerable<IOrder>> GetOrdersAsync(OrderStatus orderStatus, CancellationToken cancellationToken)
        {
            var cursor = await this._orders.FindAsync(
                o => o.Status == orderStatus, 
                new FindOptions<Order>(),
                cancellationToken);

            return await cursor.ToListAsync(cancellationToken);
        }

        public IEnumerable<IOrder> GetOrders(OrderStatus orderStatus)
        {
            var fluent = this._orders.Find(
                o => o.Status == orderStatus);

            return fluent.ToList();
        }

        public  Task<IEnumerable<IOrder>> GetOrdersAsync()
        {
            return this.GetOrdersAsync(CancellationToken.None);
        }

        public async Task<IEnumerable<IOrder>> GetOrdersAsync(CancellationToken cancellationToken)
        {
            var cursor = await this._orders.FindAsync(
                o => true,
                new FindOptions<Order>(),
                cancellationToken);

            return await cursor.ToListAsync(cancellationToken);
        }

        public IEnumerable<IOrder> GetOrders()
        {
            var fluent = this._orders.Find(
                o => true);

            return fluent.ToList();
        }

        public Task<IOrder> GetOrderAsync(string orderNumber)
        {
            return this.GetOrderAsync(orderNumber, CancellationToken.None);
        }

        public async Task<IOrder> GetOrderAsync(string orderNumber, CancellationToken cancellationToken)
        {
            var cursor = await this._orders.FindAsync(
                o => o.Id == orderNumber,
                new FindOptions<Order>(),
                cancellationToken);

            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public IOrder GetOrder(string orderNumber)
        {
            var fluent = this._orders.Find(o => o.Id == orderNumber);

            return fluent.FirstOrDefault();
        }

        public Task<IParty> CreatePartyAsync(int tableNumber, int partySize)
        {
            return this.CreatePartyAsync(tableNumber, partySize, CancellationToken.None);
        }

        public async Task<IParty> CreatePartyAsync(int tableNumber, int partySize, CancellationToken cancellationToken)
        {
            var party = new Party()
            {
                TableNumber = tableNumber,
                Size = partySize
            };

            await this._parties.InsertOneAsync(party, new InsertOneOptions(), cancellationToken);

            return party;
        }

        public IParty CreateParty(int tableNumber, int partySize)
        {
            throw new NotImplementedException();
        }

        public Task<IPizzaMenu> GetMenuAsync()
        {
            return this.GetMenuAsync(CancellationToken.None);
        }

        public async Task<IPizzaMenu> GetMenuAsync(CancellationToken cancellationToken)
        {
            var toppingsTask = this._toppings.FindAsync(
                t => true,
                new FindOptions<Topping>(),
                cancellationToken);
            var basesTask = this._bases.FindAsync(
                b => true,
                new FindOptions<Base>(),
                cancellationToken);
            
            var toppings = await (await toppingsTask).ToListAsync(cancellationToken);
            var bases = await (await basesTask).ToListAsync(cancellationToken);
            
            return new PizzaMenu()
            {
                Toppings = toppings.Select(t => t.Name).ToList(),
                PizzaBases = bases
            };
        }

        public IPizzaMenu GetMenu()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderAsync(string orderNumber, OrderStatus orderStatus)
        {
            return this.UpdateOrderAsync(orderNumber, orderStatus, CancellationToken.None);
        }

        public async Task<bool> UpdateOrderAsync(string orderNumber, OrderStatus orderStatus, CancellationToken cancellationToken)
        {
            var result = await this._orders.UpdateOneAsync(
                o => o.Id == orderNumber, 
                Builders<Order>.Update.Set(o => o.Status, orderStatus), 
                new UpdateOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public bool UpdateOrder(string orderNumber, OrderStatus orderStatus)
        {
            throw new NotImplementedException();
        }
    }
}