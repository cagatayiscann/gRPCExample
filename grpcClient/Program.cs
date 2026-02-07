using System;
using Grpc.Net.Client;
using grpcMessageClient;
using grpcServer;

namespace grpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5296");
            var messageClient = new Message.MessageClient(channel);

            // Unary
            // MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest
            // {
            //     Message = "Çağatay efendinin hürmetleri",
            //     Name = "Çağatay'ın habercisi"
            // });
            // System.Console.WriteLine(response.Message);

            // Server streaming
            // var response = messageClient.SendMessage(new MessageRequest
            // {    
            //     Message = "Çağatay efendinin hürmetleri",
            //     Name = "Çağatay'ın habercisi"
            // });

            // CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            // while (await response.ResponseStream.MoveNext(cancellationTokenSource.Token))
            // {
            //     System.Console.WriteLine(response.ResponseStream.Current.Message);
            // }

            // Client Streaming
            var request = messageClient.SendMessage();
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await request.RequestStream.WriteAsync(new MessageRequest
                {
                    Name = "Çağatay",
                    Message = "Virüs yükleniyor %" + (i+1)*10
                });
            }

            await request.RequestStream.CompleteAsync();

            System.Console.WriteLine((await request.ResponseAsync).Message);

            // var greetClient = new Greeter.GreeterClient(channel);

            // HelloReply result = await greetClient.SayHelloAsync(new HelloRequest{
            //     Name = "Çağatay'dan selamlar"
            // });
        }
    }
}