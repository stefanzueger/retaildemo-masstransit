using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using Messages.Events;

namespace Shipping
{
    public class OrderPlacedHandler : IConsumer<OrderPlaced>
    {
        static ILog log = Logger.Get<OrderPlacedHandler>();

        public Task Consume(ConsumeContext<OrderPlaced> context)
        {
            log.Info($"Received OrderPlaced, OrderId = {context.Message.OrderId} - Should i ship now?");

            return Task.CompletedTask;
        }
    }
}