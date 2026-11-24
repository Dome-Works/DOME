using FastEndpoints;
using FastEndpoints.Swagger;
using Dome.Socket.Api.Docker;
using Dome.Socket.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.AddSocketSecurity();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDomeSocket(builder.Configuration);

var app = builder.Build();

app.UseSocketSecurity();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
app.UseSwaggerGen();

app.Run();
