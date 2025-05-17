using System.Windows;
using ExpenseManager.Database;
using ExpenseManager.ViewModels;
using ScottPlot;

namespace ExpenseManager.Views;

public partial class AppWindow : Window
{
    public AppWindow(User user)
    {
        InitializeComponent();
        DataContext = new AppViewModel(user, WpfPlot1);
        
        WpfPlot1.Plot.Add.Palette = new ScottPlot.Palettes.Penumbra();
        
        WpfPlot1.Plot.FigureBackground.Color = Color.FromHex("#16222E");
        WpfPlot1.Plot.DataBackground.Color = Color.FromHex("#16222E");
        
        WpfPlot1.Plot.Add.HorizontalLine(y: 0, color: Color.FromHex("#9D00FF"));
        
        WpfPlot1.Plot.Axes.Bottom.Label.Text = "Days";
        WpfPlot1.Plot.Axes.Left.Label.Text = "Balance";
        WpfPlot1.Plot.Axes.Hairline(true);
        WpfPlot1.Refresh();
    }
}