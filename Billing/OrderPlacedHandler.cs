using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using Messages.Events;

namespace Billing
{
    public class OrderPlacedHandler : IConsumer<OrderPlaced>
    {
        static ILog log = Logger.Get<OrderPlacedHandler>();

        public Task Consume(ConsumeContext<OrderPlaced> context)
        {
            log.Info($"Received OrderPlaced, OrderId = {context.Message.OrderId} - Charging credit card...");

            return context.Publish(new OrderBilled
            {
                OrderId = context.Message.OrderId
            });
        }
    }
}