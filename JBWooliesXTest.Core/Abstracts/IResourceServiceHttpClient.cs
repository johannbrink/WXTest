using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using JBWooliesXTest.Core.Model.Request;

namespace JBWooliesXTest.Core.Abstracts
{
    public interface IResourceServiceHttpClient
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<IEnumerable<ShopperHistoryDto>> GetShopperHistory();
        Task<decimal> TrolleyCalculator(TrolleyTotalRequest trolleyTotalRequest);
    }
}