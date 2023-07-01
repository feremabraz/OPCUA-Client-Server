using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Splat;
using OPCUA_Client_Desktop.Services;
using OPCUA_Client_Desktop.ViewModels;
using OPCUA_Client_Desktop.Views;

namespace OPCUA_Client_Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Locator.CurrentMutable.RegisterLazySingleton(() => new OpcClientService(), typeof(IOpcClientService));
    }

    public override void OnFrameworkInitializationCompleted()
    {        
        var opcClientService = Locator.Current.GetService<IOpcClientService>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(opcClientService)
            };
            desktop.Startup += OnStartup;
            desktop.Exit += OnExit;
        }
        base.OnFrameworkInitializationCompleted();
    }
    
    private void OnStartup(object? s, ControlledApplicationLifetimeStartupEventArgs e)
    {
        
    }

    private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        var opcClientService = Locator.Current.GetService<IOpcClientService>();
        if (opcClientService.IsConnected) opcClientService.Disconnect();
    } 
}
