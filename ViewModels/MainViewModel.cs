using Avalonia.Threading;
using Microsoft.AspNetCore.Builder;
using ReactiveUI;
using System.Windows.Input;
using System.Reactive;

public class MainViewModel : ReactiveObject
{
    private bool _isServerRunning;
    private WebApplication _app;
    private CancellationTokenSource _cancellationTokenSource;

    public bool IsServerRunning
    {
        get => _isServerRunning;
        private set => this.RaiseAndSetIfChanged(ref _isServerRunning, value);
    }

    public string ServerStatusText => IsServerRunning ? "Server stoppen" : "Server starten";

    public ReactiveCommand<Unit, Unit> ToggleServerCommand { get; }

    public MainViewModel()
    {
        ToggleServerCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (!IsServerRunning)
            {
                await StartServer();
            }
            else
            {
                await StopServer();
            }
        });
    }

    private async Task StartServer()
    {
        try
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<SerialService>();
            builder.Services.AddEndpointsApiExplorer();

            _app = builder.Build();
            _app.MapControllers();

            await Task.Run(async () =>
            {
                try
                {
                    await _app.RunAsync(_cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    // Normales Beenden - keine Aktion erforderlich
                }
            });

            await Dispatcher.UIThread.InvokeAsync(() => 
            {
                IsServerRunning = true;
            });
        }
        catch (Exception ex)
        {
            await ShowErrorDialog("Fehler beim Starten des Servers", ex.Message);
        }
    }

    private async Task StopServer()
    {
        try
        {
            if (_app != null)
            {
                _cancellationTokenSource?.Cancel();
                await _app.StopAsync();
                _app = null;
            }
            IsServerRunning = false;
        }
        catch (Exception ex)
        {
            await ShowErrorDialog("Fehler beim Stoppen des Servers", ex.Message);
        }
    }

    private async Task ShowErrorDialog(string title, string message)
    {
        // Implementieren Sie hier Ihre eigene Avalonia-Dialoglogik
        // Zum Beispiel mit MessageBox.Avalonia NuGet-Paket:
        await MessageBox.Avalonia
            .MessageBoxManager
            .GetMessageBoxStandardWindow(title, message)
            .ShowAsync();
    }
}