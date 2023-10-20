using IPStack.API.DbContexts;
using IPStack.API.Models;
using IPStack.API.Services;
using IPStackLibrary.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

//builder.Services.AddHttpClient<IPInfoProvider>();

builder.Services.AddDbContext<IPStackContext>(
    dbContextOptions => dbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:IPDetailsDBConnectionString"]));

builder.Services.AddScoped<IIPDetailsRepository, IPDetailsRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<LibrarySettings>(builder.Configuration.GetSection("LibrarySettings"));
var IpInfoSettings = builder.Configuration.GetSection(nameof(LibrarySettings)).Get<LibrarySettings>();
builder.Services.AddTransient(sp => new IPInfoProvider(new HttpClient(), IpInfoSettings.HostName, IpInfoSettings.ApiKey) );
//{ hostName = IpInfoSettings.HostName, apiKey = IpInfoSettings.ApiKey }

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
