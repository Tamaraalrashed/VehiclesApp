var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddHttpClient();


var app = builder.Build();


app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

