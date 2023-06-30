using System;
using System.Reactive;
using ReactiveUI;

namespace OPCUA_Client_Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private bool _isConnected = OpcClientSingleton.IsConnected;
    private bool _serverError;

    public MainWindowViewModel()
    {
        ConnectCommand = ReactiveCommand.Create(Connect);
    }
    
    public ReactiveCommand<Unit, Unit> ConnectCommand { get; }
    
    private bool IsConnected
    {
        get => _isConnected;
        set => this.RaiseAndSetIfChanged(ref _isConnected, value);
    }
    
    private bool ServerError
    {
        get => _serverError;
        set => this.RaiseAndSetIfChanged(ref _serverError, value);
    }

    private void Connect()
    {
        if (!IsConnected)
        {
            try
            {
                OpcClientSingleton.Instance.Connect();
                IsConnected = true;
                ServerError = false;
            }
            catch (Exception)
            {
                IsConnected = false;
                ServerError = true;
            }
        }
        else
        {
            OpcClientSingleton.Instance.Disconnect();
            IsConnected = false;
            ServerError = false;
        }
    }
}
