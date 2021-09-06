using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockReport.Model
{
    public class RabbitMqStockReportDbSettings : IRabbitMqStockReportDbSettings
    {
        public RabbitMqStockReportDbSettings()
        {
        }

        public string RabbitCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
