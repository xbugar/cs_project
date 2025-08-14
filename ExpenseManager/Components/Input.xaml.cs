using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.Components;

public partial class Input : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(Input),
            new PropertyMetadata(string.Empty));
    
    public static readonly DependencyProperty TextBindingProperty =
        DependencyProperty.Register(nameof(TextBinding), typeof(string), typeof(Input), 
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string TextBinding
    {
        get => (string)GetValue(TextBindingProperty);
        set => SetValue(TextBindingProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public Input()
    {
        InitializeComponent();
    }
}