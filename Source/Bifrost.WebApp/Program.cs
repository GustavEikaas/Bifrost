using Bifrost.Extensions;
using BifrostConfigurator = Bifrost.Configuration.Configure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(typeof(Bifrost.Execution.IInstancesOf<>), typeof(Bifrost.Execution.InstancesOf<>));
builder.Services.AddTransient(typeof(Bifrost.Views.IView<>), typeof(Bifrost.Views.View<>));
builder.Services.AddTransient(typeof(Bifrost.Collections.IObservableCollection<>), typeof(Bifrost.Collections.ObservableCollection<>));
builder.Services.AddTransient(typeof(Bifrost.Domain.IAggregateRootRepository<>), typeof(Bifrost.Domain.AggregateRootRepository<>));
builder.Services.AddTransient(typeof(Bifrost.Execution.IInstancesOf<>), typeof(Bifrost.Execution.InstancesOf<>));
builder.Services.AddTransient(typeof(Bifrost.Read.IReadModelRepositoryFor<>), typeof(Bifrost.Read.ReadModelRepositoryFor<>));
builder.Services.AddTransient(typeof(Bifrost.Read.IReadModelOf<>), typeof(Bifrost.Read.ReadModelOf<>));

var bifrost = BifrostConfigurator.DiscoverAndConfigure(new LoggerFactory());

//Map all Bifrost.Ninject bindings to the DI container
bifrost.Container.GetBoundServices().ForEach(s =>
{
    if (s.Namespace is not null && !s.Namespace.StartsWith("Bifrost.")) return;
    if (s.IsGenericType) return;

    builder.Services.AddTransient(s, (IServiceProvider _) => bifrost.Container.Get(s, true));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

