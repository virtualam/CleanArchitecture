using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KafkaConsumer
{
    public class MyKafkaConsumer
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly string _topic;
        private readonly CancellationToken _cancellationToken;

        public MyKafkaConsumer(ConsumerConfig consumerConfig, string topic, CancellationToken cancellationToken)
        {
            _consumerConfig = consumerConfig;
            _topic = topic;
            _cancellationToken = cancellationToken;
        }

        public void Listen(Action<string> messageConsumerFtn)
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                consumer.Subscribe(_topic);

                try
                {
                    messageConsumerFtn($"Listening to messages on {_consumerConfig.BootstrapServers}...");
                    while (true)
                    {
                        var cr = consumer.Consume(_cancellationToken);
                        messageConsumerFtn($"{cr.Message.Value}");
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                    messageConsumerFtn("Ctrl-C was pressed.");
                }
                finally
                {
                    messageConsumerFtn("Closing...");
                    consumer.Close();
                }
            }
        }
    }
}
