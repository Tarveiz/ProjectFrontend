using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace FrontTestTask.App.Infrastructure;

/// <summary>
/// Подбирает View для ViewModel по зарегистрированным ViewMapping.
/// </summary>
public sealed class ViewLocator(IEnumerable<ViewMapping> mappings) : IDataTemplate
{
    private readonly Dictionary<Type, Func<Control>> _views = mappings.ToDictionary(mapping => mapping.ViewModelType, mapping => mapping.CreateView);

    public Control? Build(object? param) => param is not null && _views.TryGetValue(param.GetType(), out var createView) ? createView() : null;

    public bool Match(object? data) => data is not null && _views.ContainsKey(data.GetType());
}
