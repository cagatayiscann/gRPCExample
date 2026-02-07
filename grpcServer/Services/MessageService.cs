using Grpc.Core;
using grpcServer;
using messageServer;

namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{
    public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    {
        System.Console.WriteLine($"Message: {request.Message} | Name: {request.Name}");

        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(200);
            await responseStream.WriteAsync(new MessageResponse
            {
                Message = "Clock is ticking " + i
            });
        }
    }
}
