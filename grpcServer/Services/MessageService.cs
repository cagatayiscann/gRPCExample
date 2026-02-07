using Grpc.Core;
using grpcServer;
using messageServer;

namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{
    // Unary
    // public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
    // {
    //     System.Console.WriteLine($"Message: {request.Message} | Name: {request.Name}");

    //     return new MessageResponse
    //     {
    //         Message = "Mesaj başarıyla alındı."
    //     };
    // }

    // Server streaming
    // public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    // {
    //     System.Console.WriteLine($"Message: {request.Message} | Name: {request.Name}");

    //     for (int i = 0; i < 10; i++)
    //     {
    //         await Task.Delay(200);
    //         await responseStream.WriteAsync(new MessageResponse
    //         {
    //             Message = "Clock is ticking " + i
    //         });
    //     }
    // }

    // Client streaming
    public override async Task<MessageResponse> SendMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext(context.CancellationToken))
        {
            System.Console.WriteLine($"Message: {requestStream.Current.Message} | Name: {requestStream.Current.Name}");
        }

        return new MessageResponse
        {
            Message = "Mesaj alındı."
        };
    }
}
