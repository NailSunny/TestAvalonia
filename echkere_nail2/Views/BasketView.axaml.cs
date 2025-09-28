using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using echkere_nail2.Models;
using echkere_nail2.ViewModels;
using System.Linq;

namespace echkere_nail2;

public partial class BasketView : UserControl
{
    public BasketView()
    {
        InitializeComponent();
        DataContext = new BasketViewModel();
    }

    private async void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        var selectedBasketDTO = MainDataGridBaskets.SelectedItem as BasketDTO;
        if (selectedBasketDTO == null) return;

        var basket = App.dbContext.Baskets.FirstOrDefault(b => b.IdBasket == selectedBasketDTO.Id);
        if (basket == null) return;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var createAndChangeBasketWindow = new CreateandChangeBasket(basket);
        await createAndChangeBasketWindow.ShowDialog(parent);

        var viewModel = DataContext as BasketViewModel;
        viewModel?.RefreshData();
    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var createAndChangeBasketWindow = new CreateandChangeBasket();
        await createAndChangeBasketWindow.ShowDialog(parent);

        var viewModel = DataContext as BasketViewModel;
        viewModel?.RefreshData();
    }

    private void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var selectedBasket = MainDataGridBaskets.SelectedItem as BasketDTO;
        if (selectedBasket == null) return;

        var basketDb = App.dbContext.Baskets.FirstOrDefault(b => b.IdBasket == selectedBasket.Id);
        if (basketDb == null) return;

        App.dbContext.Baskets.Remove(basketDb);
        App.dbContext.SaveChanges();

        var viewModel = DataContext as BasketViewModel;
        viewModel?.RefreshData();
    }
}