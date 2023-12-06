
// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using gRPCServer;
using gRPCServer.Protos;

Console.WriteLine("Hello, gRPC!");

//var input = new HelloRequest { Name = "Tim" };
//var channel = GrpcChannel.ForAddress("http://localhost:5146");

//// NOTE Add using but not the reference.
//// Acts like a local call.
//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);

//Console.WriteLine(reply.Message);

var channel = GrpcChannel.ForAddress("http://localhost:5146");
var customerClient = new Customer.CustomerClient(channel);
var clientRequested = new CustomerLookupModel { UserId = 2 };
var customer = await customerClient.GetCustomerInfoAsync(clientRequested);
Console.WriteLine($"{ customer.FirstName } { customer.LastName }");


Console.WriteLine("New Customer List");
using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;

        Console.WriteLine($"{ currentCustomer.FirstName } { currentCustomer.LastName }: { currentCustomer.EmailAddress }");
    }
}


Console.ReadLine();


