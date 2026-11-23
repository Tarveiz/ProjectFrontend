using Avalonia.Controls;

namespace FrontTestTask.App.Infrastructure;

public sealed record ViewMapping(Type ViewModelType, Func<Control> CreateView);
