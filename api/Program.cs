
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Http("http://localhost:5201")
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "apiCorsPolicy",
                      builder =>
                      {
                          builder.WithOrigins("*", "https://localhost:7292", "https://localhost:7280", "http://localhost:5201")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                            //.WithMethods("OPTIONS", "GET");
                      });
});

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

app.UseCors("apiCorsPolicy"); 

app.UseSerilogRequestLogging();

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
