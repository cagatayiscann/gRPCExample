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
            // var greetClient = new Greeter.GreeterClient(channel);

            // HelloReply result = await greetClient.SayHelloAsync(new HelloRequest{
            //     Name = "Çağatay'dan selamlar"
            // });

            var messageClient = new Message.MessageClient(channel);

            MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest
            {
                Message = "Çağatay efendinin hürmetleri",
                Name = "Çağatay'ın habercisi"
            });

            System.Console.WriteLine(response.Message);
        }
    }
}