using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using echkere_nail2.Data;
using echkere_nail2.ViewModels;

namespace echkere_nail2;

public partial class ItemsView : UserControl
{
    public ItemsView()
    {
        InitializeComponent();
        DataContext = new ItemViewModel();
    }

    private async void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        var selectedItem = MainDataGridItems.SelectedItem as Item;
        if (selectedItem == null) return;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var createAndChangeItemWindow = new CreateandchangeItem(selectedItem);
        await createAndChangeItemWindow.ShowDialog(parent);

        var viewModel = DataContext as ItemViewModel;
        viewModel?.RefreshData();
    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var createAndChangeItemWindow = new CreateandchangeItem();
        await createAndChangeItemWindow.ShowDialog(parent);

        var viewModel = DataContext as ItemViewModel;
        viewModel?.RefreshData();
    }

    private void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var selectedItem = MainDataGridItems.SelectedItem as Item;
        if (selectedItem == null) return;

        App.dbContext.Items.Remove(selectedItem);
        App.dbContext.SaveChanges();

        var viewModel = DataContext as ItemViewModel;
        viewModel?.RefreshData();
    }
}