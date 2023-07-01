using System;
using System.Collections.ObjectModel;
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
    private bool _canFetch;
    
    public ReactiveCommand<Unit, Unit> ConnectCommand { get; }
    public ReactiveCommand<Unit, Unit> FetchCommand { get; }
    
    public MainWindowViewModel()
    {
        ConnectCommand = ReactiveCommand.Create(Connect);
        FetchCommand = ReactiveCommand.Create(Fetch);
        if (!Avalonia.Controls.Design.IsDesignMode) _opcClientService = new OpcClientService();
    }

    public MainWindowViewModel(IOpcClientService opcClientService) : this()
    {
        _opcClientService = opcClientService;
    }
    
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
    
    private bool CanFetch
    {
        get => _canFetch;
        set => this.RaiseAndSetIfChanged(ref _canFetch, value);
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
                CanFetch = true;
            }
            catch (Exception)
            {
                IsConnected = false;
                ServerError = true;
                CanFetch = false;
            }
        }
        else
        {
            _opcClientService.Disconnect();
            IsConnected = false;
            ServerError = false;
            CanFetch = false;
        }
    }

    private void Fetch()
    {
        FetchResults = new ObservableCollection<string>(_opcClientService.Fetch());
    }

    private ObservableCollection<string> _fetchResults;
    public ObservableCollection<string> FetchResults
    {
        get => _fetchResults;
        set => this.RaiseAndSetIfChanged(ref _fetchResults, value);
    }
}
