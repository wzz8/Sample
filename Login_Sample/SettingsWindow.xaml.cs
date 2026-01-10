using Login_Sample.ViewModels;
using System.Windows;

namespace Login_Sample;

/// <summary>
/// Interaction logic for SettingsWindow.xaml
/// </summary>
public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        this.DataContext = new SettingsViewModel(this);
        InitializeComponent();
    }
}