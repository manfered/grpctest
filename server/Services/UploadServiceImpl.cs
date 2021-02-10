using Google.Protobuf;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uploader;
using static Uploader.UploadService;

namespace server.Services
{
    public class UploadServiceImpl : UploadServiceBase
    {
        private class UploadedChunks
        {
            public int number { get; set; }
            public ByteString data { get; set; }
        }

        public override async Task<UploadResponse> Upload(IAsyncStreamReader<UploadRequest> requestStream, ServerCallContext context)
        {
            var receivedListOFData = new List<UploadedChunks>();

            while (await requestStream.MoveNext())
            {
                var newChunk = new UploadedChunks
                {
                    number = requestStream.Current.FileuploadChunks.ChunkNumber,
                    data = requestStream.Current.FileuploadChunks.ChunkData
                };
                receivedListOFData.Add(newChunk);
                Console.WriteLine($"received chunk data number : {requestStream.Current.FileuploadChunks.ChunkNumber}");
            }

            // order by the number of chunks
            receivedListOFData = receivedListOFData.OrderBy(item => item.number).ToList();

            // here the stream is completed we need to generate 
            var byteResult = new List<byte>();
            for (int index = 0; index < receivedListOFData.Count; index++)
            {
                byteResult.AddRange(receivedListOFData[index].data.ToList());
            }

            // convert byte array to text file
            File.WriteAllBytes(@"c:\grpc\received.pdf", byteResult.ToArray());

            return new UploadResponse
            {
                Result = "File received"
            };
        }
    }
}
