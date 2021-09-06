using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockReport.Model
{
    public class RabbitStockCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RabbitId { get; set; }
        public string StockId { get; set; }
        public string CompanyID { get; set; }
        public decimal StockPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DbName { get; set; }
        public string TableName { get; set; }
        public string TransactionType { get; set; }
    }
}
