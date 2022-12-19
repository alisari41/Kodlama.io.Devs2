using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs2.Application;
using Kodlama.io.Devs2.Persistence;

var builder = WebApplication.CreateBuilder(args);


#region Her projenin kendi servisleri olacak bunlarý bir bütün olarak sýnýflarý sadece buraya ekliyoruz

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
//builder.Services.AddSecurityServices();
builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddInfrastructureServices();
//builder.Services.AddHttpContextAccessor();

#endregion







builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region MiddleWare'i ekleyerek hata mesajýný düzeltiyorüz

if (app.Environment.IsProduction()) // Bunu açýklama satýrý yapýp çalýþtýrýrsak sadece hata mesajýnýn sade halini alýrýz ama bu þekilde çalýþtýrýrsak detaytlý bir þekilde hata mesajýný alýrýz
    app.ConfigureCustomExceptionMiddleware();

#endregion






//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
