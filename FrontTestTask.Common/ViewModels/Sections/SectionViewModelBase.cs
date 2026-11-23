using CommunityToolkit.Mvvm.ComponentModel;
using FrontTestTask.Common.ViewModels.Interfaces;

namespace FrontTestTask.Common.ViewModels.Sections;

public abstract partial class SectionViewModelBase : ViewModelBase, ISectionViewModel
{
    public abstract string Title { get; }

    public abstract int Order { get; }

    [ObservableProperty]
    private bool _isSelected;
}
