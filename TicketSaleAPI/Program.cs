using Microsoft.EntityFrameworkCore;
using Serilog;
using TicketSaleAPI.Data;
using TicketSaleAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lantisi
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b=> b.MigrationsAssembly("TicketSaleAPI.Data")));



// RabbitMqService
builder.Services.AddSingleton<RabbitMqService>();

// Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()      // Konsol log yaz
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)  // Dosyaya log yaz
    .CreateLogger();

// Serilog
builder.Logging.ClearProviders();  
builder.Logging.AddSerilog();  



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// RabbitMqConsumer 
var consumer = new RabbitMqConsumer();
Task.Run(() => consumer.StartConsuming());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
