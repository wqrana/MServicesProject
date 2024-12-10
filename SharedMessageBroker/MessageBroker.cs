using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SharedMessageBroker
{
   public enum QueueType
    {
        Logger,
        Product,
        Customer
    }
    public interface IMessageSender
    {
        void InitSendingExchange(QueueType queueType);
        bool SendMessage<T>(T message, QueueType queueType);
        void CloseSender(); 
    }
    public interface IMessageReceiver
    {
        void InitReceivingExchange(QueueType queueType);
        EventingBasicConsumer GetBasicConsumer();
        void SendBasicAck(dynamic args);
        void CloseConsumer(EventingBasicConsumer consumer,QueueType queueType);
        void CloseReceiver();
    }
    public class MessageBroker: IMessageSender, IMessageReceiver
    {
        private string connectionString;
        private IConfigurationSection queueTypes;
        private IConfigurationSection routingKeys;
        private string exchangeName;
        //private string queueName;
        //private string routingKey;
        private string clientType;
        private IConnection connection;
        private IModel channel;

        public MessageBroker(IConfigurationRoot configurationRoot, string clientType)
        {
            connectionString = configurationRoot.GetSection("MessageBroker:ConnectionString").Value;
            exchangeName = configurationRoot.GetSection("MessageBroker:ExchangeName").Value;
            queueTypes = configurationRoot.GetSection("MessageBroker:QueueTypes");
            routingKeys = configurationRoot.GetSection("MessageBroker:RoutingKeys");
            this.clientType = clientType;

            //setupBroker();
        }
        private void setupBroker()
        {
            ConnectionFactory connectionFactory = new();
            connectionFactory.Uri = new Uri(connectionString);
            connectionFactory.ClientProvidedName = clientType;
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);          
           
        }
        public void InitSendingExchange(QueueType queueType)
        {
            setupBroker();
            var queueName = queueTypes.GetSection(queueType.ToString()).Value;
            var routingKey = routingKeys.GetSection(queueType.ToString()).Value;
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            
        }

        public void InitReceivingExchange(QueueType queueType)
        {
            setupBroker();
            var queueName = queueTypes.GetSection(queueType.ToString()).Value;
            var routingKey = routingKeys.GetSection(queueType.ToString()).Value;
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
        }

        public bool SendMessage<T>(T message, QueueType queueType) {
            //setupBroker();
            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            var routingKey = routingKeys.GetSection(queueType.ToString()).Value;
            channel.BasicPublish(exchangeName, routingKey, null, body);            
            return true;
        }
       
        public void CloseSender()
        {
            channel.Close();
            connection.Close();
        }
        
        public EventingBasicConsumer GetBasicConsumer()
        {
            channel.BasicQos(0, 1, false);           
            return new EventingBasicConsumer(channel);
        }
        public void SendBasicAck(dynamic args)
        {
            channel.BasicAck(args.DeliveryTag, false);
        }

        public void CloseConsumer(EventingBasicConsumer consumer, QueueType queueType)
        {
            var queueName = queueTypes.GetSection(queueType.ToString()).Value;
            string customerTag = channel.BasicConsume(queueName, false, consumer);
            channel.BasicCancel(customerTag);
        }
       
        public void CloseReceiver()
        {
            channel.Close();
            connection.Close();
        }
    }

}
