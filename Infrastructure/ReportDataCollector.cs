using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using StockReport.Model;
using StockReport.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockReport.Infrastructure
{
    public class ReportDataCollector : IHostedService
    {
        private readonly ISubscriber subscriber;
        private readonly IMemoryReportStroage memoryReportStroage;
        private readonly RabbitMqService rabbitMqService;

        public ReportDataCollector(ISubscriber subscriber, IMemoryReportStroage memoryReportStroage, RabbitMqService rabbitMqService)
        {
            this.subscriber = subscriber;
            this.memoryReportStroage = memoryReportStroage;
            this.rabbitMqService = rabbitMqService;           
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriber.Subscribe(ProcessMessage);
            return Task.CompletedTask;
        }

        private bool ProcessMessage(string message, IDictionary<string, object> keyValues)
        {
            var stocks = JsonConvert.DeserializeObject<StockDetails>(message);            
            var collection = new RabbitStockCollection
            {
                //RabbitId = Guid.NewGuid().ToString(),
                CompanyID = stocks.CompanyID,
                StockPrice = stocks.StockPrice,
                StockId = stocks.StockId,
                DbName = "StocksDB",
                TableName = "StocksCollection",
                CreatedDate = stocks.CreatedDate,
                TransactionType = "Insert"
            };

            memoryReportStroage.Add(collection);
            rabbitMqService.Create(collection);
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
