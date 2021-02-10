using Conversation;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Conversation.ConversationService;

namespace server.Services
{
    public class ConversationServiceImpl : ConversationServiceBase
    {
        public override async Task HavingConversation(IAsyncStreamReader<ConversationRequest> requestStream, IServerStreamWriter<ConversationResponse> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                Console.WriteLine($"Request received : {requestStream.Current.TextMessage}");

                await responseStream.WriteAsync(new ConversationResponse
                {
                    TextResult = $"Hey you said : {requestStream.Current.TextMessage}"
                });
            }
        }
    }
}
