using System.Threading.Tasks;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.Model.Request;
using JBWooliesXTest.Core.Model.TrolleyTotal;
using Microsoft.AspNetCore.Mvc;

namespace JBWooliesXTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly IResourceServiceHttpClient _resourceServiceHttpClient;

        public TrolleyTotalController(IResourceServiceHttpClient resourceServiceHttpClient)
        {
            _resourceServiceHttpClient = resourceServiceHttpClient;
        }

        [HttpPost]
        public async Task<decimal> Post([FromBody] TrolleyTotalRequest trolleyTotalRequest)
        {
            return await _resourceServiceHttpClient.TrolleyCalculator(trolleyTotalRequest);
        }

    }
}