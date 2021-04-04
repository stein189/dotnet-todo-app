using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Todo.Projection
{
    internal class TodoConsumer : Consumer
    {
        private TodoEventHandler eventHandler;

        public TodoConsumer(TodoEventHandler eventHandler, IModel channel, ConsumerConfig config) : base(channel, config) 
        {
            this.eventHandler = eventHandler;
        }

        protected override void HandleMessage(Object model, BasicDeliverEventArgs args) 
        {
            var body = args.Body.ToArray();
            string jsonString = Encoding.UTF8.GetString(body);

            Console.WriteLine(" [x] Received {0}", jsonString);

            try {
                TodoCreated todoCreatedEvent = JsonSerializer.Deserialize<TodoCreated>(jsonString);

                this.eventHandler.Handle(todoCreatedEvent);

                this.channel.BasicAck(args.DeliveryTag, false);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());

                this.channel.BasicReject(args.DeliveryTag, true);
            }
        }
    }
}