using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using MSLoggerAPI.DAL;
using MSLoggerAPI.Model;
using SharedMessageBroker;
using SharedMessageBroker.Model;
using System.Text;
using System.Text.Json;

namespace MSLoggerAPI.LogServiceHelper
{
    public class LogBGHelper : BackgroundService
    {
        private IServiceScopeFactory serviceScopeFactory;

        public LogBGHelper(IServiceScopeFactory serviceScopeFactory)  {

            this.serviceScopeFactory = serviceScopeFactory;

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            var logRepository = scope
                .ServiceProvider
                .GetRequiredService<IGenericRespository<AppLogger>>();
            var messageReceiver = scope
                .ServiceProvider
                .GetRequiredService<IMessageReceiver>();

            messageReceiver.InitReceivingExchange(QueueType.Logger);
            while (!stoppingToken.IsCancellationRequested)
            {  
               Console.WriteLine("Listening for messages...");
               Task.Delay(5000).Wait();
               LoggingModel? receivedMsg = null;
                var consumer = messageReceiver.GetBasicConsumer();
                consumer.Received += (model, args) =>
                {
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    var body = args.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    receivedMsg = JsonSerializer.Deserialize<LoggingModel>(json);
                                       
                    Console.WriteLine("Received {0}", receivedMsg.Message);
                    if (receivedMsg != null)
                    {
                        var logEntry = new AppLogger { LoggingClass = receivedMsg.LoggingClass, LoggingMethod = receivedMsg.LoggingMethod, LoggingProgram = receivedMsg.LoggingProgram, LogType = receivedMsg.LogType, Message = receivedMsg.Message };
                        logRepository.Add(logEntry);
                    }
                    messageReceiver.SendBasicAck(args);
                };
                //Console.ReadLine();
                messageReceiver.CloseConsumer(consumer, QueueType.Logger);
                //messageReceiver.CloseReceiver();
            }
            messageReceiver.CloseReceiver();
            return Task.CompletedTask;
        }
    }
}
