using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
          );
});

builder.Configuration.AddJsonFile("ocelot.json", false, true);

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("CorsPolicy");

await app.UseOcelot();

app.UseHttpsRedirection();

app.Run();
