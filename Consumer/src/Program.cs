using Consumer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppServices(builder.Configuration);

builder
    .Services
    .AddControllers()
    .AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCloudEvents(); //Cloud Events es el middleware que desencapsula las solicitudes en formato CloudEvents
// https://github.com/cloudevents/spec/tree/v1.0
// Configure the HTTP request pipeline.

app.MapControllers();

app.MapSubscribeHandler();

app.Run();