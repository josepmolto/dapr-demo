using Producer.Extensions;
using Producer.Sender;
using Microsoft.AspNetCore.Mvc;
using Common.Dto.Derbyzone;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCloudEvents();

app.MapControllers();


app.MapPost("get_offers", async (
    [FromBody] Offer offer,
    [FromServices] IQueueSender queueSender,
    CancellationToken cancellationToken) =>
        await queueSender.SendAsync(offer, cancellationToken).ConfigureAwait(false));

app.Run();