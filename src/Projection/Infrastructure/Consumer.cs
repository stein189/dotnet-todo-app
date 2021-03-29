using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Projection 
{
    internal abstract class Consumer 
    {
        protected IModel channel;
        protected ConsumerConfig config;

        public Consumer(IModel channel, ConsumerConfig config) 
        {
            this.config = config;
            this.channel = channel;
        }

        public abstract void Start();

        /** @todo this shouldn't be here, remove after basic testing is no longer needed */
        public void Publish() 
        {
            this.Setup();

            string message = JsonSerializer.Serialize(new TodoCreated() { Title = "this is a title" });
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "messages",
                routingKey: "event.todo.created",
                basicProperties: null,
                body: body
            );
            
            Console.WriteLine(" [x] Sent {0}", message);
        }

        public void Consume(EventHandler<BasicDeliverEventArgs> handler) {
            this.Setup();
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += handler;
            
            this.channel.BasicConsume(
                queue: this.config.queue,
                autoAck: false,
                consumer: consumer
            );

            // just for now
            Console.ReadLine();
        }

        public void Setup() {
            this.channel.ExchangeDeclare(exchange: this.config.exchange, type: this.config.exchangeType);
            this.channel.QueueDeclare(
                queue: this.config.queue, 
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            this.channel.QueueBind(exchange: this.config.exchange, queue: this.config.queue, routingKey: this.config.routingKey);
        }
    }
}