using Microsoft.EntityFrameworkCore;
using SignalR.Api.Hubs;
using SignalR.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Cors politikas�n� ekledik.
//https://localhost:5003 adresinden gelen isteklere izin verdik.
//Bu adresi iste�e g�re de�i�tirebilirsiniz.
//https://localhost:5003 adresi, SignalR.Client projesinin adresidir.
//Bu adresten gelen isteklere izin vermemiz gerekiyor.
//Bu adresten gelen isteklere izin vermezsek, SignalR.Client projesi ile SignalR.Api projesi aras�nda ileti�im kuramay�z.
//Bu y�zden bu adrese izin vermemiz gerekiyor.
//AllowCredentials() metodu ile de istemci taraf�ndan gelen isteklerde kimlik do�rulamas� yap�lmas�na izin veriyoruz.
//Bu metodu kullanmazsak, istemci taraf�ndan gelen isteklerde kimlik do�rulamas� yap�lamaz.
//Bu y�zden bu metodu kullanmam�z gerekiyor.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7065").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

//SignalR servisini ekledik.
builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();



//SignalR'� kullanabilmek i�in bu kodu eklememiz gerekiyor.
//https://localhost:5001/myhub adresine istek at�ld���nda MyHub s�n�f� ile e�le�ir.
app.MapHub<MyHub>("/MyHub");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();

//    ///http://localhost:4400/MyHub
//    endpoints.MapHub<MyHub>("/MyHub");
//});

app.Run();
