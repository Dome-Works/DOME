using FastEndpoints;
using FastEndpoints.Swagger;
using Dome.Socket.Api.Docker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDomeSocket(builder.Configuration);

var app = builder.Build();

app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
app.UseSwaggerGen();

app.Run();
