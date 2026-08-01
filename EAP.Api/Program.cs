
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<EAP.Core.Services.ISimulationEngine, EAP.Core.Services.SimulationEngine>();

builder.Services.AddTransient<EAP.Core.Services.ITransientService, EAP.Core.Services.TransientService>();
builder.Services.AddScoped<EAP.Core.Services.IScopedService, EAP.Core.Services.ScopedService>();
builder.Services.AddSingleton<EAP.Core.Services.ISingletonService, EAP.Core.Services.SingletonService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
