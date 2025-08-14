using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Components;

public partial class Password : UserControl
{
    public static readonly DependencyProperty IsVisiblePropertyControl =
        DependencyProperty.Register(nameof(IsVisibleControl), typeof(bool), typeof(Password),
            new PropertyMetadata(false));

    public bool IsVisibleControl
    {
        get => (bool)GetValue(IsVisiblePropertyControl);
        set => SetValue(IsVisiblePropertyControl, value);
    }
    
    public string Secret => SecretBox.Password;

    public Password()
    {
        InitializeComponent();
    }
}