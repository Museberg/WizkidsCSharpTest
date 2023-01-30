using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using WordPredictionBackend.Clients;
using WordPredictionBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Setting up CORS
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {

        options.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WebServiceClient>();
builder.Services.AddDbContext<DictionaryContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Configuration.AddJsonFile("appsettings.json");

// Setting base address and berer token for request header
builder.Services.AddHttpClient<WebServiceClient>(client =>
{
    // Setting up base address for HTTPClient - an error message is provided if address is missing from appsettings
    client.BaseAddress = new Uri(builder.Configuration["WebServiceClient:BaseAddress"] ?? "ERR: Base address is missing from appsettings");
    // Adding a bearer token to the HTTPClient - this token is only valid until 2023-02-26, so test my application fast! ;-)
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", builder.Configuration["WebServiceClient:Token"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
