using Microsoft.EntityFrameworkCore;
using patikaodev.CustomMiddlewares;
using patikaodev.DBOperations;
using patikaodev.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<patikaodev.DBOperations.ProductsDbContext>(options => options.UseInMemoryDatabase(databaseName: "ProductsDB"));
builder.Services.AddSingleton<ILoggerService,ConsoleLogger>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.Run();
