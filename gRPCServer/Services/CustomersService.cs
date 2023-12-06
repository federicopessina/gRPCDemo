using Grpc.Core;
using gRPCServer.Protos;

namespace gRPCServer.Services;

public class CustomersService : Customer.CustomerBase
{
    private readonly ILogger<CustomersService> logger;

    public CustomersService(ILogger<CustomersService> logger)
    {
        this.logger = logger;
    }

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
    {
        CustomerModel output = new CustomerModel();

        if (request.UserId == 1)
        {
            output.FirstName = "Jamie";
            output.LastName = "Smith";
        }
        else if (request.UserId == 2)
        {
            output.FirstName = "Jane";
            output.LastName = "Doe";
        }
        else
        {
            output.FirstName = "Greg";
            output.LastName = "Thomas";
        }

        return Task.FromResult(output);
    }

    public override async Task GetNewCustomers(
        NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
    {
        List<CustomerModel> customers = new List<CustomerModel>();

        foreach (var cust in customers)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(cust);
        }
    }
}
