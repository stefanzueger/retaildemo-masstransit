using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Configuration;
using MassTransit.Logging;
using Messages.Commands;

namespace ClientUI
{
    class Program
    {
        static ILog log = Logger.Get<Program>();

        static async Task Main()
        {
            Console.Title = "ClientUI";

            var busControl = ConfigureBus();
            
            await busControl.StartAsync();

            await RunLoop(busControl);

            await busControl.StopAsync();
        }

        static IBusControl ConfigureBus()
        {
            EndpointConvention.Map<PlaceOrder>(new Uri("rabbitmq://localhost/RetailDemo.MassTransit.Sales"));

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }

        static async Task RunLoop(IBusControl busControl)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");

                        await busControl.Send(command)
                            .ConfigureAwait(false);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
