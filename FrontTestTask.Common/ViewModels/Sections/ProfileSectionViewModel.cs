using CommunityToolkit.Mvvm.ComponentModel;
using FrontTestTask.Service.Models.Dto;
using FrontTestTask.Service.Services.Interfaces;

namespace FrontTestTask.Common.ViewModels.Sections;

public sealed partial class ProfileSectionViewModel : SectionViewModelBase
{
    private readonly ISessionService _session;
    private readonly IImageLoader _imageLoader;
    private CancellationTokenSource? _avatarCancellation;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAuthenticated), nameof(Initials))]
    private UserDto? _user;

    [ObservableProperty]
    private byte[]? _avatar;

    public ProfileSectionViewModel(ISessionService session, IImageLoader imageLoader)
    {
        _session = session;
        _imageLoader = imageLoader;

        _session.SessionChanged += OnSessionChanged;
        UpdateUser();
    }

    public override string Title => "Профиль";

    public override int Order => 0;

    public bool IsAuthenticated => User is not null;

    public string Initials => GetInitials(User?.Name);

    private void OnSessionChanged(object? sender, EventArgs e) => UpdateUser();

    private void UpdateUser()
    {
        _avatarCancellation?.Cancel();
        _avatarCancellation?.Dispose();
        _avatarCancellation = null;

        User = _session.CurrentUser;
        Avatar = null;

        var avatarUrl = User?.Avatar;
        if (string.IsNullOrEmpty(avatarUrl))
            return;

        _avatarCancellation = new CancellationTokenSource();
        _ = LoadAvatarAsync(avatarUrl, _avatarCancellation.Token);
    }

    private async Task LoadAvatarAsync(string url, CancellationToken cancellationToken)
    {
        var avatar = await _imageLoader.LoadAsync(url, cancellationToken);
        if (!cancellationToken.IsCancellationRequested)
            Avatar = avatar;
    }

    private static string GetInitials(string? name)
        => string.IsNullOrWhiteSpace(name) ? string.Empty : string.Concat(name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2).Select(part => char.ToUpperInvariant(part[0])));
}
