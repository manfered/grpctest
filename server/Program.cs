using Calculator;
using Conversation;
using Counter;
using Greet;
using Grpc.Core;
using server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uploader;

namespace server
{
    class Program
    {
        const int Port = 50052;
        static async Task Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server()
                {
                    Services = { GreetingService.BindService(new GreetingServiceImpl()),
                                 CalculatorService.BindService(new CalculatorServiceImpl()),
                                 CounterService.BindService(new CounterServiceImpl()),
                                 UploadService.BindService(new UploadServiceImpl()),
                                 ConversationService.BindService(new ConversationServiceImpl())},
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine($"The server is listening on the port : {Port}");
                Console.ReadKey();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"The server failed to start : {ex.Message}");
                throw;
            }
            finally
            {
                if (server != null)
                    await server.ShutdownAsync();
            }

        }
    }
}
