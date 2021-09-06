using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockReport.Model
{
    public class StockDetails
    {
        public string StockId { get; set; }
        public string CompanyID { get; set; }
        public decimal StockPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
