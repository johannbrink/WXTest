using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using JBWooliesXTest.Core.Model.TrolleyTotal;
using Microsoft.Extensions.Options;

namespace JBWooliesXTest.API.Services
{
    public class ResourceServiceHttpHttpClient : IResourceServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ResourceServiceHttpClientOptions _options;

        public ResourceServiceHttpHttpClient(HttpClient httpClient, IOptions<ResourceServiceHttpClientOptions> options)
        {
            
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var httpResponseMessage =
                await _httpClient.GetAsync($"{_options.ProductPath}?token={_options.Token}");
            httpResponseMessage.EnsureSuccessStatusCode();
            var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var products = await JsonSerializer.DeserializeAsync<IEnumerable<ProductDto>>(stream, options);

            return products;
        }

        public async Task<IEnumerable<ShopperHistoryDto>> GetShopperHistory()
        {
            var httpResponseMessage =
                await _httpClient.GetAsync($"{_options.ShopperHistoryPath}?token={_options.Token}");
            httpResponseMessage.EnsureSuccessStatusCode();
            var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var shopperHistories =
                await JsonSerializer.DeserializeAsync<IEnumerable<ShopperHistoryDto>>(stream, options);

            return shopperHistories;
        }

        public async Task<decimal> TrolleyCalculator(TrolleyTotalRequest trolleyTotalRequest)
        {
            var jsonString = JsonSerializer.Serialize(trolleyTotalRequest);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_options.TrolleyCalculatorPath}?token={_options.Token}")
            {
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var total = await JsonSerializer.DeserializeAsync<decimal>(responseStream);

            return total;
        }
    }
}