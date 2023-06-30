using System;
using ReactiveUI;

namespace OPCUA_Client_Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string? _name;

    public MainWindowViewModel()
    {
        this.WhenAnyValue(vm => vm.Name)
            .Subscribe(_ => this.RaisePropertyChanged(nameof(Greeting)));
    }

    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Greeting => $"Hello, {(string.IsNullOrEmpty(Name) ? "world!" : Name)}";

}
