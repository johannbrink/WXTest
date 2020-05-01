using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JBWooliesXTest.Core.HttpClientModel.Resource;
using JBWooliesXTest.Core.Model.TrolleyTotal;

namespace JBWooliesXTest.Core.Abstracts
{
    public interface ITrolleyCalculator
    {
        Task<decimal> Calculate(TrolleyTotalRequest trolleyTotalRequest);
    }
}