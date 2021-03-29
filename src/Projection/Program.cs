using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Projection
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionProvider = new RabbitMQConnectionProvider();
            var channel = connectionProvider.CreateChannel();
            var eventHandler = new TodoEventHandler();
            var consumer = new TodoConsumer(
                eventHandler,
                channel, 
                new ConsumerConfig() { 
                    queue = "todo-events",
                    routingKey = "event.todo.*",
                    exchange = "messages",
                    exchangeType = "fanout",
                }
            );

            consumer.Start();
        }
    }
}
