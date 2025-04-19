using SerialWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<SerialService>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapControllers();
app.Run();