using System.Reflection;
using Ninject;
using Con = Bifrost.Configuration.Configure;
var builder = WebApplication.CreateBuilder(args);

var kernel = new StandardKernel();
kernel.Load(new List<Assembly>() { Assembly.GetExecutingAssembly() });
builder.Services.AddSingleton(kernel);
/* kernel.Bind<IServiceProvider>().ToConstant(kernel); */
/* builder.Host.UseServiceProviderFactory(new NinjectServiceProviderFactory()); */

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();
Con.DiscoverAndConfigure(new LoggerFactory());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

