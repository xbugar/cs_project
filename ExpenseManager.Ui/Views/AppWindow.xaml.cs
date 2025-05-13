using System.Windows;
using ScottPlot;

namespace ExpenseManager.Ui.Views;

public partial class AppWindow : Window
{
    public AppWindow()
    {
        InitializeComponent();

        WpfPlot1.Plot.Add.Palette = new ScottPlot.Palettes.Penumbra();

        double[] dataX = [1, 2, 3, 4, 5];
        double[] dataY1 = [1, 4, 9, 16, 25];
        var color = Color.FromHex("#FF5733");
        WpfPlot1.Plot.Add.Scatter(dataX, dataY1, color);

        color = Color.FromHex("#33FF57");
        double[] dataY2 = [7, 8, 9, 10];
        WpfPlot1.Plot.Add.Scatter(dataX, dataY2, color);

        color = Color.FromHex("#0000ff");
        double[] dataY3 = [40, 30, 50, 60, 70];
        WpfPlot1.Plot.Add.Scatter(dataX, dataY3, color);

        WpfPlot1.Refresh();

        WpfPlot1.Plot.FigureBackground.Color = Color.FromHex("#16222E");
        WpfPlot1.Plot.DataBackground.Color = Color.FromHex("#16222E");


        WpfPlot1.Plot.Axes.Hairline(true);
        WpfPlot1.Refresh();
    }
}