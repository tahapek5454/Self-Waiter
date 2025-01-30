var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecksUI(settings =>
{
    string? dealerEndpoint = $"{builder.Configuration["BaseURL"] ?? string.Empty}{builder.Configuration["ServiceURLS:DealerService"] ?? string.Empty}";
    if(!string.IsNullOrEmpty(dealerEndpoint))
    {
        settings.AddHealthCheckEndpoint("Dealer Service", dealerEndpoint);
    }

    settings.SetEvaluationTimeInSeconds(60);
    settings.SetApiMaxActiveRequests(2);

}).AddSqlServerStorage(builder.Configuration.GetConnectionString("MSSQL") ?? string.Empty);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    //options.AddCustomStylesheet("health-check-ui.css");
});

app.Run();
