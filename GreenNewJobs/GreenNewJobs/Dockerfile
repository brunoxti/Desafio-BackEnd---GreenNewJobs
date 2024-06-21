# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Set environment variables for MongoDB and RabbitMQ
ENV MONGODB_CONNECTION_STRING=mongodb://mongo:27017/GreenNewJobs
ENV RABBITMQ_HOST=rabbitmq
ENV RABBITMQ_USERNAME=guest
ENV RABBITMQ_PASSWORD=guest

# Expose the port your app runs on
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "GreenNewJobs.dll"]
