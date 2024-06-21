using GreenNewJobs.Infrastructure.Messaging;
using GreenNewJobs.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text;
using GreenNewJobs.Domain.Interfaces;
using GreenNewJobs.Infrastructure.Context;
using GreenNewJobs.Infrastructure.Repositories;
using GreenNewJobs.Application.Validators;
using GreenNewJobs.Domain.Events;
using GreenNewJobs.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using GreenNewJobs.Application.UseCases.GetAllRentalPlans;
using GreenNewJobs.Application.UseCases.RentalPlanUseCases.CreateRentalPlan;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.CreateMotorcycle;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.UpdateMotorcycle;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.DeleteMotocycle;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotocycleById;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetMotorcycleByPlate;
using GreenNewJobs.Application.UseCases.MotorcyclesUseCases.GetAllMotorcycle;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.CreateDeliveryPerson;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetDeliveryPersonById;
using GreenNewJobs.Application.UseCases.OrdersUseCases.CreateOrder;
using GreenNewJobs.Application.UseCases.RentalsUseCases.CreateRental;
using GreenNewJobs.Application.UseCases.OrdersUseCases.AcceptOrder;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.DeleteDeliveryPerson;
using GreenNewJobs.Application.UseCases.OrdersUseCases.DeliverOrder;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.GetAllDeliveryPersons;
using GreenNewJobs.Application.UseCases.OrdersUseCases.GetAllOrders;
using GreenNewJobs.Application.UseCases.OrdersUseCases.GetOrderById;
using GreenNewJobs.Application.UseCases.RentalsUseCases.GetRentalById;
using GreenNewJobs.Application.UseCases.DeliveryPersonUseCases.UpdateDeliveryPerson;
using GreenNewJobs.Application.UseCases.OrdersUseCases.UpdateOrder;
using GreenNewJobs.Application.UseCases.RentalsUseCases.UpdateRental;
using GreenNewJobs.Application.UseCases.RentalsUseCases.GetAllRentals;
using GreenNewJobs.Application.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<CreateRentalPlanUseCase>();
builder.Services.AddScoped<GetAllRentalPlansUseCase>();


// Adicionar serviços ao contêiner
builder.Services.AddControllers();

builder.Services.AddScoped<NotificationService>();

// Registrar Validators
builder.Services.AddTransient<IValidator<CreateMotorcycleGreenInput>, CreateMotorcycleGreenInputValidator>();
builder.Services.AddTransient<IValidator<UpdateMotorcycleGreenInput>, UpdateMotorcycleGreenInputValidator>();
builder.Services.AddTransient<IValidator<DeleteMotorcycleGreenInput>, DeleteMotorcycleGreenInputValidator>();
builder.Services.AddTransient<IValidator<GetMotorcycleGreenByIdInput>, GetMotorcycleGreenByIdInputValidator>();
builder.Services.AddTransient<IValidator<GetMotorcycleGreenByPlateInput>, GetMotorcycleGreenByPlateInputValidator>();
builder.Services.AddTransient<IValidator<CreateDeliveryPersonInput>, CreateDeliveryPersonInputValidator>();
builder.Services.AddTransient<IValidator<CreateRentalInput>, CreateRentalInputValidator>();
builder.Services.AddTransient<IValidator<CreateOrderInput>, CreateOrderInputValidator>();

// Registrar Event Dispatcher
builder.Services.AddSingleton<IEventDispatcher, RabbitMqEventDispatcher>();
builder.Services.AddSingleton<IEventRepository, EventRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GreenNewJobs API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira o JWT com Bearer no campo",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Configuração do JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

var key = Encoding.ASCII.GetBytes(jwtKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("DelivererPolicy", policy => policy.RequireRole("Deliverer"));
});

// Configuração do MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddSingleton<MongoDbContext>();

// Registrar os repositórios
builder.Services.AddScoped<IMotorcycleGreenRepository, MotorcycleGreenRepository>();
builder.Services.AddScoped<IDeliveryPersonRepository, DeliveryPersonRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderNotificationRepository, OrderNotificationRepository>();
builder.Services.AddScoped<IRentalPlanRepository, RentalPlanRepository>();

// Registrar os handlers
builder.Services.AddScoped<CreateMotorcycleGreenUseCase>();
builder.Services.AddScoped<GetMotorcycleGreenByIdUseCase>();
builder.Services.AddScoped<GetAllMotorcycleGreenUseCase>();
builder.Services.AddScoped<GetMotorcycleGreenByPlateUseCase>();
builder.Services.AddScoped<UpdateMotorcycleGreenUseCase>();
builder.Services.AddScoped<DeleteMotorcycleGreenUseCase>();
builder.Services.AddScoped<CreateDeliveryPersonUseCase>();
builder.Services.AddScoped<GetDeliveryPersonByIdUseCase>();
builder.Services.AddScoped<GetAllDeliveryPersonsUseCase>();
builder.Services.AddScoped<UpdateDeliveryPersonUseCase>();
builder.Services.AddScoped<DeleteDeliveryPersonUseCase>();
builder.Services.AddScoped<CreateRentalUseCase>();
builder.Services.AddScoped<GetRentalByIdUseCase>();
builder.Services.AddScoped<GetAllRentalsUseCase>();
builder.Services.AddScoped<UpdateRentalUseCase>();
builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<GetOrderByIdUseCase>();
builder.Services.AddScoped<GetAllOrdersUseCase>();
builder.Services.AddScoped<UpdateOrderUseCase>();
builder.Services.AddScoped<GetAllRentalPlansUseCase>();
builder.Services.AddScoped<UpdateRentalReturnDateUseCase>();
builder.Services.AddScoped<AcceptOrderUseCase>();
builder.Services.AddScoped<DeliverOrderUseCase>();



// Configuração do RabbitMQ
builder.Services.AddSingleton<IConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var factory = new ConnectionFactory
    {
        HostName = configuration["RabbitMQ:HostName"],
        UserName = configuration["RabbitMQ:UserName"],
        Password = configuration["RabbitMQ:Password"],
        AutomaticRecoveryEnabled = true, // Habilita a recuperação automática
        NetworkRecoveryInterval = TimeSpan.FromSeconds(10) // Intervalo de recuperação
    };

    int retries = 5;
    while (true)
    {
        try
        {
            return factory.CreateConnection();
        }
        catch
        {
            if (--retries == 0)
                throw;
            Console.WriteLine("Waiting for RabbitMQ to be available...");
            Thread.Sleep(5000);
        }
    }
});

builder.Services.AddSingleton<IEventDispatcher, RabbitMqEventDispatcher>();


builder.Services.AddHostedService<OrderNotificationConsumer>();


builder.Services.AddSingleton<OrderNotificationPublisher>();
builder.Services.AddHostedService<OrderNotificationConsumer>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<DeliveryPersonService>();
builder.Services.AddScoped<RentalPlanService>();

// Registrar o inicializador de planos de aluguel
builder.Services.AddHostedService<RentalPlanInitializer>();

var app = builder.Build();

// Configurar o pipeline de solicitações HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreenNewJobs API v1"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
