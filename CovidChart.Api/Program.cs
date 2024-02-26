using CovidChart.Api.Hubs;
using CovidChart.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});

//SignalR servisini ekledik.
builder.Services.AddSignalR();
builder.Services.AddScoped<CovidService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7065").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.UseCors("CorsPolicy");


//SignalR'ý kullanabilmek için bu kodu eklememiz gerekiyor.
//https://localhost:5001/myhub adresine istek atýldýðýnda MyHub sýnýfý ile eþleþir.
app.MapHub<CovidHub>("/CovidHub");

app.Run();
