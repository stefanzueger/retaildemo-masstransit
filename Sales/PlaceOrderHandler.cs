using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using Messages.Commands;
using Messages.Events;

namespace Sales
{
    public class PlaceOrderHandler : IConsumer<PlaceOrder>
    {
        private static readonly ILog Log = Logger.Get<PlaceOrderHandler>();

        static Random random = new Random();

        public Task Consume(ConsumeContext<PlaceOrder> context)
        {
            Log.Info($"Received PlaceOrder, OrderId = {context.Message.OrderId}");

            if (random.Next(0, 5) == 0)
            {
                throw new Exception("Oops");
            }

            var orderPlaced = new OrderPlaced
            {
                OrderId = context.Message.OrderId
            };
            return context.Publish(orderPlaced);
        }
    }
}