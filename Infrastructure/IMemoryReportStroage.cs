using StockReport.Model;
using System.Collections.Generic;

namespace StockReport.Infrastructure
{
    public interface IMemoryReportStroage
    {
        void Add(RabbitStockCollection stockDetails);
        IEnumerable<RabbitStockCollection> Get();
    }
}