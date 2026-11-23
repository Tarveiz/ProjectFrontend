using Avalonia;
using Avalonia.Input;
using Avalonia.Threading;

namespace FrontTestTask.App.Infrastructure;

/// <summary>
/// Устанавливает фокус на элемент при его появлении в визуальном дереве.
/// </summary>
public static class FocusBehavior
{
    public static readonly AttachedProperty<bool> IsInitiallyFocusedProperty = AvaloniaProperty.RegisterAttached<InputElement, bool>("IsInitiallyFocused", typeof(FocusBehavior));

    static FocusBehavior()
    {
        IsInitiallyFocusedProperty.Changed.AddClassHandler<InputElement>(OnIsInitiallyFocusedChanged);
    }

    public static bool GetIsInitiallyFocused(InputElement element) => element.GetValue(IsInitiallyFocusedProperty);

    public static void SetIsInitiallyFocused(InputElement element, bool value) => element.SetValue(IsInitiallyFocusedProperty, value);

    private static void OnIsInitiallyFocusedChanged(InputElement element, AvaloniaPropertyChangedEventArgs e)
    {
        element.AttachedToVisualTree -= OnAttachedToVisualTree;

        if (e.NewValue is true)
            element.AttachedToVisualTree += OnAttachedToVisualTree;
    }

    // Фокус откладывается до завершения построения окна, иначе он не применяется.
    private static void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender is InputElement element)
            Dispatcher.UIThread.Post(() => element.Focus());
    }
}
