using MassTransit;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.StateMachine.StateDbContext;
using SelfWaiter.StateMachine.StateInstances;
using SelfWaiter.StateMachine.StateMachines;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(configure =>
{
    configure.AddSagaStateMachine<DealerImageFileChangedStateMachine, DealerImageFileChangedStateInstance>()
    .EntityFrameworkRepository(options =>
    {
        options.AddDbContext<DbContext, SelfWaiterDbContext>((provider, _builder) =>
        {
            _builder.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")); 
        });
    });

    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        configurator.ReceiveEndpoint(RabbitMQSettings.StateMachine_DealerImageFileChangedQueue, e =>
        {
            e.ConfigureSaga<DealerImageFileChangedStateInstance>(context);
            e.DiscardSkippedMessages();
        });
    });

});


var app = builder.Build();



ApplyPendigMigration();
app.Run();

void ApplyPendigMigration()
{
    var _db = builder.Services.BuildServiceProvider().GetRequiredService<DbContext>();

    if (_db.Database.GetPendingMigrations().Count() > 0)
        _db.Database.Migrate();
}
