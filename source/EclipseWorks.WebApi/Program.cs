using EclipseWorks.Application;
using EclipseWorks.DbAdapter;
using EclipseWorks.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//registering DI
builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddDbAdapter(builder.Configuration);

var app = builder.Build();

//Add Configuration File
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json");

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
