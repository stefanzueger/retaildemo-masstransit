using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using Messages.Commands;

namespace Shipping
{
    class ShipOrderHandler : IConsumer<ShipOrder>
    {
        static ILog log = Logger.Get<ShipOrderHandler>();

        public Task Consume(ConsumeContext<ShipOrder> context)
        {
            log.Info($"Order [{context.Message.OrderId}] - Succesfully shipped.");
            return Task.CompletedTask;
        }
    }
}