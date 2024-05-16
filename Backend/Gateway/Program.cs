using Ocelot.Middleware;

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
            policy.WithOrigins("http://localhost:53001").WithMethods("PUT", "GET", "DELETE", "POST").AllowAnyHeader();
        });
});

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

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
