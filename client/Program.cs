using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dummy;
using Greet;
using Calculator;
using System.Net.NetworkInformation;
using Counter;
using Uploader;
using System.IO;
using Google.Protobuf;
using Conversation;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50052";
        const int defaultDataChunkSize = 50000;
        static async Task Main(string[] args)
        {
            //await Test1();
            //await Test2();
            //await Test3();
            //await Test4();
            //await Test5();
            //await Test6();

            await Test7();
        }

        private static async Task Test7()
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Connected to server successfully");
            });

            var client = new ConversationService.ConversationServiceClient(channel);

            var stream = client.HavingConversation();

            var listOfConversationRequests = new List<ConversationRequest>
            {
                new ConversationRequest
                {
                    TextMessage = "Hello"
                },
                new ConversationRequest
                {
                    TextMessage = "How are you doing?"
                },
                new ConversationRequest
                {
                    TextMessage = "Nice to meet you"
                }
            };

            foreach (var item in listOfConversationRequests)
            {
                await stream.RequestStream.WriteAsync(item);
            }

            var responseTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine($"received something from the server : {stream.ResponseStream.Current.TextResult}");
                }
            });          

            await stream.RequestStream.CompleteAsync();
            await responseTask;

            await channel.ShutdownAsync();
            Console.WriteLine("We are done here");
            Console.ReadKey();
        }

        private static async Task Test6()
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Connection extablished");
            });

            var client = new UploadService.UploadServiceClient(channel);

            var stream = client.Upload();

            var meantToBeUploadedBytes = System.IO.File.ReadAllBytes(@"c:\grpc\sending.pdf");
            var numberOfChunks = meantToBeUploadedBytes.Length / defaultDataChunkSize + 1;

            for (int chunkIndex = 0; chunkIndex < numberOfChunks; chunkIndex++)
            {
                try
                {
                    if (chunkIndex == numberOfChunks - 1)
                    {
                        await stream.RequestStream.WriteAsync(new UploadRequest
                        {
                            FileuploadChunks = new FileuploadChunks
                            {
                                ChunkNumber = chunkIndex,
                                ChunkData = ByteString.CopyFrom(meantToBeUploadedBytes, chunkIndex * defaultDataChunkSize, meantToBeUploadedBytes.Length - (chunkIndex * defaultDataChunkSize))
                            }
                        });
                    }
                    else
                    {
                        await stream.RequestStream.WriteAsync(new UploadRequest
                        {
                            FileuploadChunks = new FileuploadChunks
                            {
                                ChunkNumber = chunkIndex,
                                ChunkData = ByteString.CopyFrom(meantToBeUploadedBytes, chunkIndex * defaultDataChunkSize, defaultDataChunkSize)
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

            // define when the stream is completed
            await stream.RequestStream.CompleteAsync();

            // retureive the responde from the server
            var response = await stream.ResponseAsync;

            // inform everuthing is done
            Console.WriteLine("seems like the file is uploaded");

            // shut down channel
            await channel.ShutdownAsync();

            // keep ui open
            Console.ReadKey();
        }

        private static async Task Test5()
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Connected to the server");
            });

            var client = new GreetingService.GreetingServiceClient(channel);

            var stream = client.GreetLong();

            for (int index = 0; index < 10; index++)
            {
                await stream.RequestStream.WriteAsync(new GreetingLongRequest
                {
                    Greeting = new Greeting
                    {
                        FirstName = $"Fred {index}",
                        LastName = $"Seifi {index}"
                    }
                });
            }

            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;

            Console.WriteLine($"here is the response : {response.Result}");
            await channel.ShutdownAsync();

            Console.ReadKey();
        }

        private static async Task Test4()
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Conected to the server successfully");
            });

            var client = new CounterService.CounterServiceClient(channel);

            var response = client.counterUntil(new CounterRequest
            {
                Number = 15
            });

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(1000);
            }

            await channel.ShutdownAsync();
            Console.ReadKey();
        }

        private static async Task Test3()
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync();
            Console.WriteLine($"Conncted properly");

            var client = new CalculatorService.CalculatorServiceClient(channel);

            var response = await client.CalculateAsync(new CalculatorRequest
            {
                A = 12,
                B = 3
            });

            Console.WriteLine($"Here is the response : {response.Result}");

            await channel.ShutdownAsync();
            Console.WriteLine("Connection was finished");
            Console.ReadKey();
        }

        private static async Task Test2()
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Connected to the server");
            });

            var client = new GreetingService.GreetingServiceClient(channel);

            var response = client.GreetManyTime(new GreetingManyTimesRequest
            {
                Greeting = new Greeting
                {
                    FirstName = "Fred",
                    LastName = "Seifi"
                }
            });

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(1000);
            }

            await channel.ShutdownAsync();
            Console.ReadKey();
        }

        private static async Task Test1()
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync();
            Console.WriteLine("The Client Connected Syccessfully");

            //var client = new DummyService.DummyServiceClient(channel);
            var client = new GreetingService.GreetingServiceClient(channel);

            var response = await client.GreetAsync(new GreetingRequest
            {
                Greeting = new Greeting
                {
                    FirstName = "Fred",
                    LastName = "Vog"
                }
            });

            Console.WriteLine($"The response was : {response.Result}");

            await channel.ShutdownAsync();
            Console.ReadKey();
        }
    }
}
