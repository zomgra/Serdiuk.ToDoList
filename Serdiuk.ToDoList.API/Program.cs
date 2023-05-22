using Serdiuk.ToDoList.API.Extensions;
using Serdiuk.ToDoList.Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureIdentity(logger);
builder.Services.ConfigureEntityFramework(logger);
builder.Services.ConfigureSwagger(logger);
builder.Services.ConfigureRepository(logger);
builder.Services.ConfigureJWTBearer(logger, builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TodoExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
