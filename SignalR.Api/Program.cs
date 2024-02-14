using SignalR.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//SignalR servisini ekledik.
builder.Services.AddSignalR();

//Cors politikasýný ekledik.
//https://localhost:5003 adresinden gelen isteklere izin verdik.
//Bu adresi isteðe göre deðiþtirebilirsiniz.
//https://localhost:5003 adresi, SignalR.Client projesinin adresidir.
//Bu adresten gelen isteklere izin vermemiz gerekiyor.
//Bu adresten gelen isteklere izin vermezsek, SignalR.Client projesi ile SignalR.Api projesi arasýnda iletiþim kuramayýz.
//Bu yüzden bu adrese izin vermemiz gerekiyor.
//AllowCredentials() metodu ile de istemci tarafýndan gelen isteklerde kimlik doðrulamasý yapýlmasýna izin veriyoruz.
//Bu metodu kullanmazsak, istemci tarafýndan gelen isteklerde kimlik doðrulamasý yapýlamaz.
//Bu yüzden bu metodu kullanmamýz gerekiyor.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:5003").AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//SignalR'ý kullanabilmek için bu kodu eklememiz gerekiyor.
//https://localhost:5001/myhub adresine istek atýldýðýnda MyHub sýnýfý ile eþleþir.
app.MapHub<MyHub>("/MyhHub");

//app.UseEndpoints(endpoints =>
//{
//    app.MapControllers();
//    app.MapHub<MyHub>("/myhub");
//});


app.Run();
