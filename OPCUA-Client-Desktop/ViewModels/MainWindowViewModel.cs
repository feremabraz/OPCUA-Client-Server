using System;
using System.Reactive;
using ReactiveUI;
using OPCUA_Client_Desktop.Services;

namespace OPCUA_Client_Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private IOpcClientService _opcClientService;
    private bool _isConnected;
    private bool _serverError;

    public MainWindowViewModel(IOpcClientService opcClientService)
    {
        ConnectCommand = ReactiveCommand.Create(Connect);
        _opcClientService = opcClientService;
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
        if (!_opcClientService.IsConnected)
        {
            try
            {
                _opcClientService.Connect();
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
            _opcClientService.Disconnect();
            IsConnected = false;
            ServerError = false;
        }
    }
}
