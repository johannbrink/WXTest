using System.Text.Json.Serialization;
using JBWooliesXTest.Core.HttpClientModel.Resource;

namespace JBWooliesXTest.Core.Model.Response
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        [JsonIgnore]
        public double Popularity { get; set; }

        public static ProductResponse FromDto(ProductDto dto)
        {
            return new ProductResponse()
            {
                Price = dto.Price,
                Quantity = dto.Quantity,
                Name = dto.Name
            };
        }
    }
}
