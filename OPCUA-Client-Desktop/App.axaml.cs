using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OPCUA_Client_Desktop.ViewModels;
using OPCUA_Client_Desktop.Views;

namespace OPCUA_Client_Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
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
        if (OpcClientSingleton.IsConnected) OpcClientSingleton.Instance.Disconnect();
    } 
}