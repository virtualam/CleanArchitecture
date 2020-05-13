For windows

To start zookeeper
C:\kafka-2.12>.\bin\windows\zookeeper-server-start.bat .\config\server.properties

To start Kafka Broker
C:\kafka-2.12>.\bin\windows\kafka-server-start.bat .\config\server.properties

To Create topic
C:\kafka-2.12\bin\windows>kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic test

Start Producer
C:\kafka-2.12\bin\windows>kafka-console-producer.bat --broker-list localhost:9092 --topic test

To create consumer
C:\kafka-2.12\bin\windows>kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic test --from-beginning

Topic creation With multi partition with replication factor 1
C:\kafka-2.12\bin\windows>kafka-topics --zookeeper localhost:2181 --topic first_topic --create --partitions 3 --replication-factor 1

To get the list of topic created in system
C:\kafka-2.12\bin\windows>kafka-topics --zookeeper localhost:2181 --list