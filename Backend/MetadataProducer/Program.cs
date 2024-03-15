using TV2.Backend.Services.MetadataProducer.DataProviders;
using TV2.Backend.Services.MetadataProducer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddTransient<IDataProvider, DummyDataProvider>();
builder.Services.AddSingleton<IDataProvider, DummyDataProvider>();
builder.Services.AddSingleton<IMessageService, MessageService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();