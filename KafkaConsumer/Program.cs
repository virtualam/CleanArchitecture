using Confluent.Kafka;
using System;
using System.Threading;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cancellationTokenSource.Cancel();
            };

            var consumerConfig_config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "mylogs-consumer",
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = false
            };

            var kafkaConsumer = new MyKafkaConsumer(consumerConfig_config, "mylogs", cancellationTokenSource.Token);
            kafkaConsumer.Listen(Console.WriteLine);
        }
    }
}
