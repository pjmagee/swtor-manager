using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.Infrastructure;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using SwtorHelper.Data;
using SwtorHelper.Domain.Tracker;
using SwtorLogParser.Monitor;

namespace SwtorHelper;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); })
            .ConfigureLifecycleEvents((lifecycleBuilder) =>
            {
                lifecycleBuilder.AddWindows(windowsLifecycleBuilder =>
                {
                    var monitor = MauiWinUIApplication.Current.Services.GetRequiredService<CombatLogsMonitor>();
                    var trackerService = MauiWinUIApplication.Current.Services.GetRequiredService<CharacterTracker>();
                    
                    windowsLifecycleBuilder.OnLaunched((application, args) =>
                    {
                        monitor.Start(CancellationToken.None);
                    });

                    windowsLifecycleBuilder.OnClosed((window, args) =>
                    {
                        monitor.Stop();
                    });
                    
                    windowsLifecycleBuilder.OnVisibilityChanged((window, args) =>
                    {
                        
                    });
                    
                    windowsLifecycleBuilder.OnWindowCreated((window) =>
                    {

                    });
                });
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        
        builder.Services.AddFluentUIComponents();
        builder.Services.AddSingleton<SettingsManager>();
        builder.Services.AddSingleton<SettingsApplier>();
        builder.Services.AddSingleton<CharacterTracker>();
        builder.Services.AddSingleton(CombatLogsMonitor.Instance);
        builder.Services.AddSingleton(CombatLogs.Instance);
        
        

        return builder.Build();
    }
}