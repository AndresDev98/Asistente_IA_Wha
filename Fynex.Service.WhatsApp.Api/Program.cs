using Fynex.Service.WhatsApp.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fynex WhatsApp API",
        Version = "v1",
        Description = "API para gestionar mensajes de WhatsApp con IA"
    });
});

builder.Services.AddSingleton<OpenAiService>();
builder.Services.AddSingleton<TwilioService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();