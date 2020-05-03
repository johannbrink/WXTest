using System.Collections.Generic;
using System.Text.Json.Serialization;
using JBWooliesXTest.Core.Model.TrolleyCalculator;

namespace JBWooliesXTest.Core.Model.Request
{
    public class TrolleyTotalRequest
    {
        public List<Product> Products { get; set; }

        public List<Special> Specials { get; set; }

        [JsonPropertyName("Quantities")]
        public List<RequestedItem> RequestedItems { get; set; }

    }
}
