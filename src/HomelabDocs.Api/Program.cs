using FastEndpoints;
using FastEndpoints.Swagger;
using HomelabDocs.Business;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddHomelabDocsBusiness();

var app = builder.Build();

app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();
