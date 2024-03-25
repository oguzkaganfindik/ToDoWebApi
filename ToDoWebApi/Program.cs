using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// MVC projesi olsa viewler olaca�� i�in AddControllerWithViews eklenirdi. Fakat Web Api katman�nda view d�n�lmeyecek o nedenle sadece Controllers yeterli.

builder.Services.AddEndpointsApiExplorer(); // Api i�in gerekli

builder.Services.AddDbContext<ToDoContext>(options => options.UseInMemoryDatabase("ToDoDb"));

builder.Services.AddSwaggerGen(); // Swagger kullan�m� i�in.

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();