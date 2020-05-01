using System.Collections.Generic;

namespace JBWooliesXTest.Core.HttpClientModel.Resource
{
    public class ShopperHistoryDto
    {
        public int CustomerId { get; set; }
        public List<ShopperHistoryProductDto> Products { get; set; }
    }
}
