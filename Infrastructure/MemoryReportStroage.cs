using StockReport.Model;
using System.Collections.Generic;

namespace StockReport.Infrastructure
{
    public class MemoryReportStroage : IMemoryReportStroage
    {
        private readonly IList<RabbitStockCollection> stockList = new List<RabbitStockCollection>();

        public void Add(RabbitStockCollection stockDetails)
        {
            stockList.Add(stockDetails);
        }

        public IEnumerable<RabbitStockCollection> Get()
        {
            return stockList;
        }
    }
}
