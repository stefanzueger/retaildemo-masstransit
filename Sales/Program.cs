using System;
using System.Threading.Tasks;
using MassTransit;

namespace Sales
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Sales";

            var busControl = ConfigureBus();

            await busControl.StartAsync();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await busControl.StopAsync();
        }

        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "RetailDemo.MassTransit.Sales", e =>
                {
                    e.Consumer<PlaceOrderHandler>();
                });
            });
        }
    }
}
