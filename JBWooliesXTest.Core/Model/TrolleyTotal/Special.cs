using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JBWooliesXTest.Core.Model.TrolleyTotal
{
    public class Special
    {
        [JsonPropertyName("Quantities")]
        public List<SpecialInventory>  Inventory { get; set; }
        public decimal Total { get; set; }
    }
}
