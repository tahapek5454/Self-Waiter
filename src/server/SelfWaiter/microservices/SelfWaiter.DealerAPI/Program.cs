using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SelfWaiter.DealerAPI;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
           );
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDealerService(builder.Configuration);

builder.Services.AddDealerCache(builder.Configuration, enableRedis:false);
builder.Host.AddDealerLoggerService(enableElasticsearch:false);
builder.Services.AddDealerHealthChecks(builder.Configuration, enableMSSQL:true, enableRedis: false, enableElasticsearch:false);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(optinos =>
    {
        // auth opeartion will be added
        optinos
        .WithTitle("Self Waiter Dealer")
        .WithTheme(ScalarTheme.Mars)
        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
    });
}

app.UseCors("CorsPolicy");

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

ApplyPendigMigration();

app.Run();


void ApplyPendigMigration()
{
    using var scope = app.Services.CreateScope();

    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (_db.Database.GetPendingMigrations().Count() > 0)
        _db.Database.Migrate();
}
