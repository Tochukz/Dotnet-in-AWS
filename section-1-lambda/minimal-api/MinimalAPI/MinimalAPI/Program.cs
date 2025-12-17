using Amazon.Lambda.AspNetCoreServer.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Lambda hosting for HTTP API or Lambda URL
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Alternatives:
// LambdaEventSource.RestApi (API Gateway REST API)
// LambdaEventSource.ApplicationLoadBalancer

var app = builder.Build();

app.MapGet("/", () => "Welcome to My Minimal API!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





