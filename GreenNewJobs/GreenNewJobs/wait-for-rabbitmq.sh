#!/bin/sh

# Função para verificar se RabbitMQ está acessível
check_rabbitmq() {
  nc -z rabbitmq 5672
}

# Verifica se RabbitMQ está acessível
until check_rabbitmq; do
  echo "Waiting for RabbitMQ..."
  sleep 2
done

echo "RabbitMQ is up and running!"
exec "$@"
