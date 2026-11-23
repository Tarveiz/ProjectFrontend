namespace FrontTestTask.Common.ViewModels;

internal static class ValidationPatterns
{
    // Регулярка для email, формата:
    // имя@домен.зона: латиница, цифры, в имени также _%+-, в домене дефис; точка только как разделитель частей.
    public const string Email = @"^[A-Za-z0-9_%+-]+(\.[A-Za-z0-9_%+-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.[A-Za-z]{2,}$";
}
