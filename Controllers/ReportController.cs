using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockReport.Infrastructure;
using StockReport.Model;
using StockReport.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockReport.Controllers
{
    [ApiController]
    [Route("api/v1.0/market/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMemoryReportStroage memoryReportStroage;
        private readonly RabbitMqService rabbitMqService;

        public ReportController(IMemoryReportStroage memoryReportStroage, RabbitMqService rabbitMqService)
        {
            this.memoryReportStroage = memoryReportStroage;
            this.rabbitMqService = rabbitMqService;
        }

        [HttpGet]
        [Route("GetStocks")]
        public IEnumerable<RabbitStockCollection> Get()
        {
            try
            {
                var result = this.rabbitMqService.GetAll();
                return result;
            }
            catch
            {
                return this.memoryReportStroage.Get();
            }
        }
    }
}
