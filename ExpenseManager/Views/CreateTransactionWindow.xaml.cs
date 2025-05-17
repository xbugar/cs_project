using System.Collections.ObjectModel;
using System.Windows;
using ExpenseManager.Database;
using ExpenseManager.ViewModels;

namespace ExpenseManager.Views;

public partial class CreateTransactionWindow : Window
{
    public CreateTransactionWindow(Account account, ObservableCollection<Transaction> transactions)
    {
        InitializeComponent();
        DataContext = new CreateTransactionViewModel(account, transactions);
    }


    private void Border_DragOver(object sender, DragEventArgs e)
    {
        e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        e.Handled = true;
    }

    private void Border_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])(e.Data.GetData(DataFormats.FileDrop) ?? Array.Empty<string>());
            if (files.Length > 0 && DataContext is CreateTransactionViewModel viewModel)
            {
                viewModel.ProcessDroppedFile(files);
            }
        }
    }
}