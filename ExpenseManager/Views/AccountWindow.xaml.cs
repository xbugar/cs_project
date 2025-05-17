using System.Windows;
using ExpenseManager.Database;
using ExpenseManager.ViewModels;
using ScottPlot;

namespace ExpenseManager.Views;

public partial class AccountWindow : Window
{
    public AccountWindow(Account account)
    {
        InitializeComponent();
        DataContext = new AccountViewModel(account, WpfPlot2);
        
        WpfPlot2.Plot.Add.Palette = new ScottPlot.Palettes.Penumbra();
        WpfPlot2.Plot.FigureBackground.Color = Color.FromHex("#16222E");
        WpfPlot2.Plot.DataBackground.Color = Color.FromHex("#16222E");
        
        WpfPlot2.Plot.Add.HorizontalLine(y: 0, color: Color.FromHex("#9D00FF"));
        
        WpfPlot2.Plot.Axes.Bottom.Label.Text = "Days";
        WpfPlot2.Plot.Axes.Left.Label.Text = "Balance";
        
        WpfPlot2.Plot.Axes.Hairline(true);
        WpfPlot2.Refresh();
    }
}