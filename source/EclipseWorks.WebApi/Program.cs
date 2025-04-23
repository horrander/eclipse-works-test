using EclipseWorks.Application;
using EclipseWorks.DbAdapter;
using EclipseWorks.Domain;
using EclipseWorks.WebApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilters>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Eclipse Works - Projects Manager",
        Version = "v1",
    });

    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EclipseWorks.WebApi.xml");
    options.IncludeXmlComments(filePath);
});

builder.Services.AddLogging(builder => builder.ClearProviders().AddConsole());
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
