using Counter;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Counter.CounterService;

namespace server.Services
{
    public class CounterServiceImpl : CounterServiceBase
    {
        public override async Task counterUntil(CounterRequest request, IServerStreamWriter<CounterResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Server received the request : {request.ToString()}");

            for (int index = 1; index <= request.Number; index++)
            {
                await responseStream.WriteAsync(new CounterResponse
                {
                    Result = index
                });
            }
        }

    }
}
