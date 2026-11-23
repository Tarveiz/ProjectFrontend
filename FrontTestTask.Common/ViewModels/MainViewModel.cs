using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FrontTestTask.Common.ViewModels.Interfaces;

namespace FrontTestTask.Common.ViewModels;

public sealed partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ISectionViewModel? _currentSection;

    public MainViewModel(IEnumerable<ISectionViewModel> sections)
    {
        Sections = [.. sections.OrderBy(section => section.Order)];
        CurrentSection = Sections.FirstOrDefault();
    }

    public string Title => "FrontTestTask";

    public IReadOnlyList<ISectionViewModel> Sections { get; }

    [RelayCommand]
    private void Navigate(ISectionViewModel section) => CurrentSection = section;

    partial void OnCurrentSectionChanged(ISectionViewModel? oldValue, ISectionViewModel? newValue)
    {
        if (oldValue is not null)
            oldValue.IsSelected = false;

        if (newValue is not null)
            newValue.IsSelected = true;
    }
}
