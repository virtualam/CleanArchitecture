using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaProducer
{
    public class MyKafkaProducer
    {
        private readonly ProducerConfig _producerConfig;
        private readonly string _topic;
        private readonly CancellationToken _cancellationToken;

        public MyKafkaProducer(ProducerConfig producerConfig, string topic, CancellationToken cancellationToken)
        {
            _producerConfig = producerConfig;
            _topic = topic;
            _cancellationToken = cancellationToken;
        }

        public async Task<DeliveryResult<Null, string>> Produce(string message)
        {
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                //producer.Produce(_topic, new Message<Null, string> { Value = message }, handler);
                //await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                var t = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                return t;
                //await t.ContinueWith(task =>
                //{
                //    if (task.IsFaulted)
                //    {

                //    }
                //    else
                //    {
                //        messageProducerFtn($"Wrote to offset: {task.Result.Offset}");
                //    }
                //});
            }
        }
    }
}
