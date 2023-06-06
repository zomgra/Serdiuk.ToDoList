using Serdiuk.ToDoList.API.Extensions;
using Serdiuk.ToDoList.Application.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(b =>
{
    b.AddDefaultPolicy(p =>
    {
        p.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000")
        .AllowCredentials();
    });
});

builder.Services.ConfigureEntityFramework(logger);
builder.Services.ConfigureIdentity(logger);
builder.Services.ConfigureJWTBearer(logger, builder.Configuration);
builder.Services.ConfigureSwagger(logger, builder.Configuration);
builder.Services.ConfigureRepository(logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi");
        options.DocumentTitle = "TodoApi";
        options.DocExpansion(DocExpansion.List);
    });
}

app.UseMiddleware<TodoExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();
