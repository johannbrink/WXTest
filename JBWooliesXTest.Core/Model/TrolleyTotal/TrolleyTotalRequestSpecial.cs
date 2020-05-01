using System.Collections.Generic;

namespace JBWooliesXTest.Core.Model.TrolleyTotal
{
    public class TrolleyTotalRequestSpecial
    {
        public List<TrolleyTotalRequestSpecialQuantity>  Quantities { get; set; }
        public double Total { get; set; }
    }
}
