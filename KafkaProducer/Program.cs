using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaProducer
{
    class Program
    {
        public static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
            };

            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "mylogs-producer"
            };

            string topic = "mylogs";

            var kafkaProducer = new MyKafkaProducer(config, topic, cancellationTokenSource.Token);

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Console.WriteLine("Enter a message");
                var input = Console.ReadLine();
                if (input == null)
                    break;
                var result = kafkaProducer.Produce(input).GetAwaiter().GetResult();
                Console.WriteLine($"Message '{result.Message.Value}' {result.Status}.");
            }
        }
    }
}
