# FrontTestTask

Тестовое задание: desktop-приложение на Avalonia UI (.NET 10) по паттерну MVVM с авторизацией и регистрацией через [fakeapifordevs](https://fakeapifordevs.vercel.app/docs).

## Запуск

Нужен .NET 10 SDK (версия закреплена в `global.json`).

```bash
dotnet run --project FrontTestTask.App
```

Или открыть `FrontTestTask.slnx` в Visual Studio 2026.

## Что умеет

- Вход, выход и регистрация в отдельном окне с автоматическим входом после неё.
- Раздел «Профиль» с данными пользователя и аватаром, раздел «О разработчике».
- Валидация полей, индикатор загрузки и сообщения об ошибках.

## Архитектура

Решение разбито на три проекта:

- **FrontTestTask.Service** — работа с API и сервисы (сессия, авторизация). Ничего не знает про UI.
- **FrontTestTask.Common** — ViewModels. Не зависит от Avalonia.
- **FrontTestTask.App** — разметка, стили и регистрация зависимостей.

Ссылки идут в одну сторону: App → Common → Service.

Соответствие названиям из задания: MainView — `MainWindow`, View_1 — раздел «Профиль», View_2 — раздел «О разработчике».

## MVVM

- Свойства, команды и валидация сделаны на CommunityToolkit.Mvvm.
- Навигация между разделами идёт через команду в `MainViewModel`, нужная View подбирается по типу ViewModel. Чтобы добавить раздел, достаточно создать ViewModel, View и зарегистрировать их одной строкой.
- Окно регистрации открывается через `IDialogService`, ViewModel не знает о View.
- В code-behind только `InitializeComponent()`, привязки компилируемые.

## Работа с API

- Все запросы асинхронные и поддерживают отмену.
- Ошибки обрабатываются в одном месте: сетевые сбои, таймауты и ответы сервера с ошибкой превращаются в понятное сообщение для пользователя.
- Токен подставляется в запросы автоматически и хранится только в памяти.

## NuGet-пакеты

- Avalonia, Avalonia.Desktop, Avalonia.Themes.Fluent: UI-фреймворк и базовая тема.
- Avalonia.Fonts.Inter: одинаковый шрифт на всех ОС.
- AvaloniaUI.DiagnosticsSupport: инструменты отладки UI, только в Debug.
- CommunityToolkit.Mvvm: MVVM-фреймворк.
- Microsoft.Extensions.DependencyInjection: внедрение зависимостей.
- Microsoft.Extensions.Http: HttpClientFactory, управление HTTP-соединениями и подстановка токена в запросы.
- Microsoft.Extensions.Logging.Abstractions, Microsoft.Extensions.Logging.Debug: логирование.

## Особенности тестового API

- При входе всегда возвращается один и тот же пользователь (Hayden Zaman), пароль должен быть не короче 6 символов.
- Регистрация не сохраняет пользователя и не возвращает токен, поэтому после неё выполняется отдельный вход.
- Тексты ошибок сервер присылает на английском, они показываются как есть.
