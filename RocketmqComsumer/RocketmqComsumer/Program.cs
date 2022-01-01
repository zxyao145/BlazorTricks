using RocketmqComsumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<EventDispathcer>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


var consumer = app.Services.GetRequiredService<EventDispathcer>();
//consumer.Start();

app.Run();
