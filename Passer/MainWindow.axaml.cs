using System;
using System.Collections.Generic;
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

    private void ApplicationLicence(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ApplicationAbout(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ApplicationHomePage(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ApplicationLoginAdd(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ApplicationLoginEdit(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ApplicationLoginDelete(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}