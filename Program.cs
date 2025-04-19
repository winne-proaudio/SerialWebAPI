// Program.cs
using SerialWebAPI.Services;
using System;
using System.Windows;


if (Environment.GetCommandLineArgs().Contains("--webapi"))
{
    // Web API Modus
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<SerialService>();
    builder.Services.AddEndpointsApiExplorer();

    var app = builder.Build();
    app.MapControllers();
    await app.RunAsync();
}
else
{
    // Desktop Modus
    var application = new System.Windows.Application();
    application.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
    application.Run();

}