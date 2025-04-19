// Program.cs
using SerialWebAPI.Services;
using System;
using System.Windows;



    // Web API Modus
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<SerialService>();
    builder.Services.AddEndpointsApiExplorer();

    var app = builder.Build();
    app.MapControllers();
    await app.RunAsync();
