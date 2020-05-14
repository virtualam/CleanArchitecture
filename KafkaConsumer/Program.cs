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

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "mylogs-consumer",
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = false
            };

            string topic = "mylogs";

            var kafkaConsumer = new MyKafkaConsumer(config, topic, cancellationTokenSource.Token);
            kafkaConsumer.Consume(Console.WriteLine);
        }
    }
}
