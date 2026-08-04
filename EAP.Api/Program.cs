
using EAP.Api.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<EAP.Core.Services.ISimulationEngine, EAP.Core.Services.SimulationEngine>();

builder.Services.AddTransient<EAP.Core.Services.ITransientService, EAP.Core.Services.TransientService>();
builder.Services.AddScoped<EAP.Core.Services.IScopedService, EAP.Core.Services.ScopedService>();
builder.Services.AddSingleton<EAP.Core.Services.ISingletonService, EAP.Core.Services.SingletonService>();
builder.Services.AddDbContext<EAP.Core.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EAP")));
builder.Services.AddScoped<EAP.Core.Data.DeviceRepository>();

builder.Services.AddScoped<EAP.Core.Data.DeviceRepository>();
builder.Services.AddScoped<EAP.Core.Services.IDeviceService, EAP.Core.Services.DeviceService>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionMiddleware>();

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
