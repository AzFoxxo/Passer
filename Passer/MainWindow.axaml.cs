using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Passer;

public partial class MainWindow : Window
{
    private string? _passwordStorePath;
    private List<Login> _logins = [];
    
    public MainWindow()
    {
        InitializeComponent();
    }

    // Quit the application
    private void ApplicationQuit(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    // Save the logins
    private async void ApplicationSave(object? sender, RoutedEventArgs e)
    {
        // Check if the password store path is set
        if (_passwordStorePath is not null) SavePasswordStore();
        else ApplicationSaveAs(sender, e);
        
    }

    private void SavePasswordStore()
    {
        // Write the passwords to the json file
        var jsonString = System.Text.Json.JsonSerializer.Serialize(_logins);
        System.IO.File.WriteAllText(_passwordStorePath!, jsonString);
    }

    private async void ApplicationSaveAs(object? sender, RoutedEventArgs e)
    {
        var saveDialogue = new SaveFileDialog
        {
            Title = "Save Password Store",
            InitialFileName = "my-password-store.json",
            Filters =
            {
                new FileDialogFilter { Name = "Password Store", Extensions = { "json" } }
            }
        };
        var result = await saveDialogue.ShowAsync(this);
        if (result is null) return;
        _passwordStorePath = result;
        SavePasswordStore();
    }

    private async void ApplicationLoad(object? sender, RoutedEventArgs e)
    {
        // Open file dialogue
        var openDialogue = new OpenFileDialog
        {
            Title = "Open Password Store",
            Filters =
            {
                new FileDialogFilter { Name = "Password Store", Extensions = { "json" } }
            }
        };
        var result = await openDialogue.ShowAsync(this);
        if (result is null || result.Length == 0) return;
        
        // Load the data
        var jsonString = await System.IO.File.ReadAllTextAsync(result[0]);
        _logins = System.Text.Json.JsonSerializer.Deserialize<List<Login>>(jsonString) ?? [];
        _passwordStorePath = result[0];
    }

    // Open the licence
    private void ApplicationLicence(object? sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://github.com/AzFoxxo/Passer/blob/main/LICENSE",
            UseShellExecute = true
        });
    }

    // Show the about dialogue with the version number and info
    private async void ApplicationAbout(object? sender, RoutedEventArgs e)
    {
        var dialogue = new AboutDialogue();
        await dialogue.ShowDialog(this);
    }

    // Open the website
    private void ApplicationHomePage(object? sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "http://github.com/azfoxxo/passer",
            UseShellExecute = true
        });

    }

    // Add a new login
    private async void ApplicationLoginAdd(object? sender, RoutedEventArgs e)
    {
        var dialogue = new AddLoginWindow();
    
        var result = await dialogue.ShowDialog<Login>(this);
        _logins.Add(result);
    }
}