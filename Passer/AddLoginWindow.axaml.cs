using System;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Passer;

public partial class AddLoginWindow : Window
{
    public AddLoginWindow()
    {
        InitializeComponent();
    }

    // Add the login
    private void Add(object? sender, RoutedEventArgs e)
    {
        // Create the login
        var login = new Login
        {
            WebsiteUrl = UrlField.Text,
            Username = EmailUsernameField.Text,
            Password = PasswordField.Text,
            Tags = TagsField.Text?
                .Split(',')
                .Select(tag => tag.Trim())
                .Where(tag => !string.IsNullOrWhiteSpace(tag))
                .ToList()
        };
        
        Close(login);

    }

    // Cancel adding a login
    private void Cancel(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}