using System.Threading.Tasks;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace JBWooliesXTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly ITrolleyCalculator _trolleyCalculator;

        public TrolleyTotalController(ITrolleyCalculator trolleyCalculator)
        {
            _trolleyCalculator = trolleyCalculator;
        }

        [HttpPost]
        public async Task<decimal> Post([FromBody] TrolleyTotalRequest trolleyTotalRequest)
        {
            return await _trolleyCalculator.Calculate(trolleyTotalRequest);
        }

    }
}