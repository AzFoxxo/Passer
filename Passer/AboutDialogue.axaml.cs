using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Passer;

public partial class AboutDialogue : Window
{
    public AboutDialogue()
    {
        InitializeComponent();

        AppName.Text = "Passer";
        VersionText.Text = $"Version: {GetAppVersion()}";
    }

    private static string GetAppVersion()
    {
        var version = Assembly.GetEntryAssembly()?.GetName().Version;
        return version?.ToString() ?? "Unknown";
    }
}