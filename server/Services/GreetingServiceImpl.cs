using Greet;
using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace server.Services
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GreetingResponse
            {
                Result = $"Hello {request.Greeting.FirstName} {request.Greeting.LastName}"
            });
        }

        public override async Task GreetManyTime(GreetingManyTimesRequest request, IServerStreamWriter<GreetingManyTimesResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Server recieved the request : {request.ToString()}");
            var result = $"hello {request.Greeting.FirstName} {request.Greeting.LastName}";

            foreach (var item in Enumerable.Range(1, 10))
            {
                await responseStream.WriteAsync(new GreetingManyTimesResponse
                {
                    Result = result
                });
            }
        }

        public override async Task<GreetingLongResponse> GreetLong(IAsyncStreamReader<GreetingLongRequest> requestStream, ServerCallContext context)
        {
            Console.WriteLine("Server Long request stream started");
            var result = $"Start\n";
            while (await requestStream.MoveNext())
            {
                result += requestStream.Current.Greeting.FirstName + " " + requestStream.Current.Greeting.LastName + "\n";
            }

            return new GreetingLongResponse
            {
                Result = result
            };
        }
    }
}
