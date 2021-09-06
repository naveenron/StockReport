using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockReport.Model
{
    public interface IRabbitMqStockReportDbSettings
    {
        string RabbitCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
