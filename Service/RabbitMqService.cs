using MongoDB.Bson;
using MongoDB.Driver;
using StockReport.Model;
using System.Collections.Generic;
using System.Linq;

namespace StockReport.Service
{
    public class RabbitMqService
    {
        private readonly IMongoCollection<RabbitStockCollection> rabbitMongoCollection;

        public RabbitMqService(IRabbitMqStockReportDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            rabbitMongoCollection = database.GetCollection<RabbitStockCollection>(settings.RabbitCollectionName);
        }

        public List<RabbitStockCollection> GetAll()
        {
            var rabbit = rabbitMongoCollection.Find(new BsonDocument()).ToList();   
            return rabbit;
        }
            
        public RabbitStockCollection Create(RabbitStockCollection collection)
        {
            rabbitMongoCollection.InsertOne(collection);            
            return collection;
        }

    }
}