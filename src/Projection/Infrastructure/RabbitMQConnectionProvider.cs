using System;
using RabbitMQ.Client;

namespace Projection
{
    public class RabbitMQConnectionProvider
    {
        private IConnection connection;

        public IModel CreateChannel()
        {
            if (this.connection == null)
            {
                this.connection = this.Connect();
            }

            Console.WriteLine("Creating new channel!");

            return this.connection.CreateModel();
        }

        private IConnection Connect()
        {
            Console.WriteLine("Conneting to RabbitMQ...");

            /* @todo pass config in constructor */
            ConnectionFactory factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "rabbitmq.service.dev"
            };

            IConnection connection = factory.CreateConnection();

            Console.WriteLine("Connetion established!");

            return connection;
        }
    }
}