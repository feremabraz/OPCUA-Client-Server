using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using OPCUA_Client_Desktop.Services;
using Opc.UaFx;

// Disables warning pertaining to a problem in the Avalonia 11 RC design previewer.
#pragma warning disable CS8618

namespace OPCUA_Client_Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IOpcClientService _opcClientService;
    private bool _isConnected;
    private bool _serverError;
    private int _selectedIndex;
    private ObservableCollection<OpcNodeData> _fetchResults;
    private string _nodeValue;
    
    public ReactiveCommand<Unit, Unit> ConnectCommand { get; }
    public ReactiveCommand<Unit, Unit> FetchCommand { get; }
    public ReactiveCommand<Unit, Unit> FetchNodeValueCommand { get; }
    
    public MainWindowViewModel()
    {
        ConnectCommand = ReactiveCommand.Create(Connect);
        FetchCommand = ReactiveCommand.Create(Fetch);
        FetchNodeValueCommand = ReactiveCommand.Create(FetchNodeValue);
        this.WhenAnyValue(x => x.SelectedIndex)
            .Subscribe(_ =>
            {
                this.RaisePropertyChanged(nameof(NodeId));
                this.RaisePropertyChanged(nameof(NodeLevel));
            });
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

    public int SelectedIndex
    {
        get => _selectedIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }

    public ObservableCollection<OpcNodeData> FetchResults
    {
        get => _fetchResults;
        set => this.RaiseAndSetIfChanged(ref _fetchResults, value);
    }

    public string NodeId => FetchResults.ElementAtOrDefault(SelectedIndex)?.NodeId ?? string.Empty;
    public int NodeLevel => FetchResults.ElementAtOrDefault(SelectedIndex)?.Level ?? 0;
    public string NodeValue
    {
        get => _nodeValue;
        set => this.RaiseAndSetIfChanged(ref _nodeValue, value);
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

    private void Fetch()
    {
        if (!_opcClientService.IsConnected) return;
        FetchResults = new ObservableCollection<OpcNodeData>(_opcClientService.Fetch());
    }

    private void FetchNodeValue()
    {
        string? nodeId = FetchResults.ElementAtOrDefault(SelectedIndex)?.NodeId;
        if (string.IsNullOrEmpty(nodeId)) return;
        OpcReadNode nodeToRead = new OpcReadNode(nodeId);
        NodeValue = _opcClientService.FetchSingle(nodeId);
    }

}
