namespace FrontTestTask.Common.ViewModels.Interfaces;

/// <summary>
/// Раздел главного окна. Порядок в меню через Order.
/// </summary>
public interface ISectionViewModel
{
    public string Title { get; }

    public int Order { get; }

    public bool IsSelected { get; set; }
}
