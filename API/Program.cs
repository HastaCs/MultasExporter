
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Diagnostics;
using System.Globalization;

namespace API
{
  public class Program
  {
    public static void Main(string[] args)
    {
     
      var builder = WebApplication.CreateBuilder(args);

     
      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }
      app.Use(async (context, next) =>
      {
        var maxAllowedSize = 30 * 1024 * 1024; // 20 MB
        context.Request.EnableBuffering(); // Permitir leer el cuerpo de la solicitud varias veces
        if (context.Request.ContentLength > maxAllowedSize)
        {
          context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
         return; // Detener el pipeline
        }

        await next.Invoke();
      });
      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

    
      app.Run();
    }
  }
}
