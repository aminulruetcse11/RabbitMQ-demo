# RabbitMQ-demo
Rabbit MQ Publisher and receiver demo project
# Docker image for RabbitMQ
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management
# Package need to install
NuGet\Install-Package RabbitMQ.Client -Version 6.4.0
