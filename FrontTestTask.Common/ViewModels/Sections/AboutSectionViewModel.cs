namespace FrontTestTask.Common.ViewModels.Sections;

public sealed class AboutSectionViewModel : SectionViewModelBase
{
    public override string Title => "О разработчике";

    public override int Order => 1;

    public string FullName => "Крючков Михаил";

    public string Position => "C#/.NET-разработчик";

    public string Goal => "Рассматриваю позицию .NET Frontend Developer (Avalonia UI / WPF)";

    public string Experience => "Коммерческий опыт разработки на .NET: 3 года 8 месяцев";

    public string Summary => "Готов работать и развиваться как frontend-разработчик на Avalonia UI и WPF. "
        + "Есть опыт разработки desktop-приложений на WPF. Это приложение на Avalonia UI с MVVM (CommunityToolkit.Mvvm) выполнено как тестовое задание. "
        + "Основной коммерческий опыт связан с серверной разработкой: ASP.NET Core, REST API, PostgreSQL, Redis. "
        + "Понимание серверной стороны помогает проектировать клиентские приложения и их работу с API. "
        + "Занимаюсь оптимизацией производительности: на самых нагруженных API поднял пропускную способность с 16 до 100 RPS.";

    public IReadOnlyList<string> Skills { get; } =
    [
        "C#", ".NET", "Avalonia UI", "WPF", "MVVM", "CommunityToolkit.Mvvm", "REST API", "ASP.NET Core",
        "PostgreSQL", "Redis", "Elasticsearch", "Docker", "Git", "GitLab", "CI/CD", "SOLID"
    ];

    public IReadOnlyList<string> Languages { get; } =
    [
        "Русский: родной",
        "Английский: B2 (Upper-Intermediate)"
    ];

    public IReadOnlyList<string> Contacts { get; } =
    [
        "Telegram: @Tarveiz",
        "Email: misha28.95@mail.ru",
        "GitHub: github.com/Tarveiz"
    ];
}
