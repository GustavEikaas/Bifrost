using BifrostConfigurator = Bifrost.Configuration.Configure;

var builder = WebApplication.CreateBuilder(args);

BifrostConfigurator.DiscoverAndConfigure(builder.Services, new LoggerFactory());

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

