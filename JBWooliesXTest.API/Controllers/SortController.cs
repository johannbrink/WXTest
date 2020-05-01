using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using JBWooliesXTest.Core.Model;
using JBWooliesXTest.Core.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace JBWooliesXTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SortController : ControllerBase
    {
        private readonly IResourceServiceHttpClient _resourceServiceHttpClient;

        public SortController(IResourceServiceHttpClient resourceServiceHttpClient)
        {
            _resourceServiceHttpClient = resourceServiceHttpClient;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResponse>> Get([FromQuery] string sortOption)
        {
            if (!Enum.TryParse<SortOption>(sortOption, out var typedSortOption))
                throw new ArgumentException();

            var products = await _resourceServiceHttpClient.GetProducts();
            switch (typedSortOption)
            {
                case SortOption.Low:
                    return products.Select(ProductResponse.FromDto).OrderBy(x => x.Price);
                case SortOption.High:
                    return products.Select(ProductResponse.FromDto).OrderByDescending(x => x.Price);
                case SortOption.Ascending:
                    return products.Select(ProductResponse.FromDto).OrderBy(x => x.Name);
                case SortOption.Descending:
                    return products.Select(ProductResponse.FromDto).OrderByDescending(x => x.Name);
                case SortOption.Recommended:
                {
                    var shopperHistories = await _resourceServiceHttpClient.GetShopperHistory();
                    var productHistory = new List<ShopperHistoryProductDto>();
                    foreach (var shopperHistory in shopperHistories)
                    {
                        productHistory.AddRange(shopperHistory.Products);
                    }

                    var productPopularity = productHistory
                        .GroupBy(x => x.Name)
                        .Select(g => new ShopperHistoryProductDto()
                        {
                            Name = g.Key,
                            Quantity = g.Sum(x => x.Quantity)
                        });
                    var query =
                        from product in products
                        join popularity in productPopularity on product.Name equals popularity.Name into gj
                        from subPopularity in gj.DefaultIfEmpty()
                        select new ProductResponse()
                        {
                            Name = product.Name,
                            Quantity = product.Quantity,
                            Price = product.Price,
                            Popularity = subPopularity?.Quantity ?? 0
                        };


                    return query.OrderByDescending(x => x.Popularity);

                }
                default:
                    throw new NotImplementedException(); //This line is intentionally not covered in a test as it would result in less maintainable code if default is used to handle a specific case
            }
        }
    }
}