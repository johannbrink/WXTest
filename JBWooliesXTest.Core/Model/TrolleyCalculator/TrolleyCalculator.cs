using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JBWooliesXTest.Core.Abstracts;
using JBWooliesXTest.Core.Model.TrolleyTotal;

namespace JBWooliesXTest.Core.Model.TrolleyCalculator
{
    public class TrolleyCalculator : ITrolleyCalculator
    {
        public Task<decimal> Calculate(TrolleyTotalRequest trolleyTotalRequest)
        {
            throw new NotImplementedException();
        }
    }
}
