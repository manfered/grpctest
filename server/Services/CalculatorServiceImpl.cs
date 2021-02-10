using Calculator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calculator.CalculatorService;

namespace server.Services
{
    public class CalculatorServiceImpl : CalculatorServiceBase
    {
        public override async Task<CalculatorResponse> Calculate(CalculatorRequest request, ServerCallContext context)
        {
            return new CalculatorResponse
            {
                Result = request.A + request.B
            };
        }
    }
}
