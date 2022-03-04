
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

// var logger = new LoggerConfiguration()
//     .MinimumLevel.Debug()
//     .WriteTo.Console()
//     .WriteTo.Http("http://localhost:5201")
//     .Enrich.FromLogContext()
//     .CreateLogger();

// // Configure serilog
// Log.Logger = logger;

// builder.Logging.ClearProviders();
// builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Http("http://localhost:5201")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
