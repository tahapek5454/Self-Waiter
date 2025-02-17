using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SelfWaiter.AuthAPI;
using SelfWaiter.AuthAPI.Core.Domain.Entities;
using SelfWaiter.AuthAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

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

builder.Services.AddAuthService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(optinos =>
    {
        // auth opeartion will be added
        optinos
        .WithTitle("Self Waiter Auth")
        .WithTheme(ScalarTheme.Mars)
        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
    });
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapIdentityApi<AppUser>();

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
