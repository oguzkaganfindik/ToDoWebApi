using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// MVC projesi olsa viewler olacaðý için AddControllerWithViews eklenirdi. Fakat Web Api katmanýnda view dönülmeyecek o nedenle sadece Controllers yeterli.

builder.Services.AddEndpointsApiExplorer(); // Api için gerekli

builder.Services.AddDbContext<ToDoContext>(options => options.UseInMemoryDatabase("ToDoDb"));

builder.Services.AddSwaggerGen(); // Swagger kullanýmý için.

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