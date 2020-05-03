using System.Threading.Tasks;
using JBWooliesXTest.Core.Model.Request;

namespace JBWooliesXTest.Core.Abstracts
{
    public interface ITrolleyCalculator
    {
        Task<decimal> Calculate(TrolleyTotalRequest trolleyTotalRequest);
    }
}