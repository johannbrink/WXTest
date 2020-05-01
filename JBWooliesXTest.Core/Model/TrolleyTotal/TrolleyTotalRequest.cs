using System.Collections.Generic;
namespace JBWooliesXTest.Core.Model.TrolleyTotal
{
    public class TrolleyTotalRequest
    {
        public List<TrolleyTotalRequestProduct> Products { get; set; }

        public List<TrolleyTotalRequestSpecial> Specials { get; set; }

        public List<TrolleyTotalRequestQuantity> Quantities { get; set; }
    }
}
