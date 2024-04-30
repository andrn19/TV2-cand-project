using System.Net;
using Ocelot.Administration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TV2.Backend.ClassLibrary.DataBrokers;
using TV2.Backend.ClassLibrary.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var  corsPolicy = "_corsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy  =>
        {
            policy
                .WithOrigins("https://www.google.com",
                                "http://localhost:*")
                .WithMethods("PUT", "GET", "DELETE", "POST");
        });
});


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

app.UseCors(corsPolicy);

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();