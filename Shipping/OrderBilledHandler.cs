using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using Messages.Events;

namespace Shipping
{
    public class OrderBilledHandler : IConsumer<OrderBilled>
    {
        static ILog log = Logger.Get<OrderBilledHandler>();

        public Task Consume(ConsumeContext<OrderBilled> context)
        {
            log.Info($"Received OrderPlaced, OrderId = {context.Message.OrderId} - should I ship now?");

            return Task.CompletedTask;
        }
    }
}