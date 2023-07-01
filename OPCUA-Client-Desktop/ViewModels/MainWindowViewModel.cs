using System;
using System.Reactive;
using ReactiveUI;
using OPCUA_Client_Desktop.Services;

// Disables warning pertaining to a problem in the Avalonia 11 RC design previewer.
#pragma warning disable CS8618

namespace OPCUA_Client_Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private IOpcClientService _opcClientService;
    private bool _isConnected;
    private bool _serverError;
    
    public MainWindowViewModel()
    {
        ConnectCommand = ReactiveCommand.Create(Connect);
        if (!Avalonia.Controls.Design.IsDesignMode) _opcClientService = new OpcClientService();
    }

    public MainWindowViewModel(IOpcClientService opcClientService) : this()
    {
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
