using Ocelot.Administration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TV2.Backend.ClassLibrary.DataBrokers;
using TV2.Backend.ClassLibrary.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IMetadataEndpointResolver, EndpointResolver>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://code-maze.com/aspnetcore-api-gateway-with-ocelot/
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration).AddAdministration("/administration", "secret");

var app = builder.Build();
var logger = app.Services.GetService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// if (!app.Environment.IsDevelopment())
// {
//     app.UseHsts();
// }
//app.UseHttpsRedirection();

var conf = new OcelotPipelineConfiguration
{
    PreQueryStringBuilderMiddleware = async (httpContext, next) =>
    {
        if(!httpContext.Request.Path.Equals("/gateway/resolve-test"))
        {
            await next.Invoke();

            return;
        }

        var queryString = httpContext.Request.Query["endpoint"];

        var metadataHostResolver = httpContext.RequestServices.GetRequiredService<IMetadataEndpointResolver>();

        var metadata = metadataHostResolver.Resolve(Guid.Parse(queryString));

        var downstreamRequest = httpContext.Items.DownstreamRequest();

        downstreamRequest.Host = "api.dummy";
        downstreamRequest.Port = 80;
        logger.LogDebug(downstreamRequest.ToString());
                
                
        //downstreamRequest.Host = metadata.Url;
        //downstreamRequest.Port = int.Parse(metadata.Port);

        await next.Invoke();
    }
};

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();



/*
namespace TV2.Gateway;

public class Program
{
    public static void Main(string[] args)
    {
        new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile("ocelot.json")
                    .AddEnvironmentVariables();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                if (hostingContext.HostingEnvironment.IsDevelopment())
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                }
                //add your logging
            })
            .UseIISIntegration()
            .UseStartup<Startup>()
            .Build()
            .Run();
    }
}
*/