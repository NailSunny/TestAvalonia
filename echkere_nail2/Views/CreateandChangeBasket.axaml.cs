using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using echkere_nail2.Data;
using echkere_nail2.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace echkere_nail2;

public partial class CreateandChangeBasket : Window
{
    private Basket thisSelectedBasket;

    public CreateandChangeBasket(Basket basket)
    {
        InitializeComponent();
        thisSelectedBasket = basket;

        var vm = new BasketViewModel();
        DataContext = vm;

        UsersCombo.SelectedItem = vm.Users.FirstOrDefault(u => u.IdUser == basket.UserId);
        ItemsCombo.SelectedItem = vm.Items.FirstOrDefault(i => i.IdItem == basket.ItemId);
        CountText.Text = basket.Count?.ToString();
    }

    public CreateandChangeBasket()
    {
        InitializeComponent();
        DataContext = new BasketViewModel();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        if (UsersCombo.SelectedItem is not User selectedUser)
        {
            ShowError("Выберите пользователя.");
            return;
        }

        if (ItemsCombo.SelectedItem is not Item selectedItem)
        {
            ShowError("Выберите товар.");
            return;
        }

        if (!int.TryParse(CountText.Text, out int count) || count <= 0)
        {
            ShowError("Введите корректное количество (>0).");
            return;
        }

        if (!Regex.IsMatch(CountText.Text, @"^[0-9]+$"))
        {
            ShowError("Поле 'Количество' может содержать только цифры.");
            return;
        }

        if (thisSelectedBasket == null)
        {
            var newBasket = new Basket
            {
                UserId = selectedUser.IdUser,
                ItemId = selectedItem.IdItem,
                Count = count
            };
            App.dbContext.Baskets.Add(newBasket);
        }
        else
        {
            var basketDb = App.dbContext.Baskets.FirstOrDefault(b => b.IdBasket == thisSelectedBasket.IdBasket);
            if (basketDb != null)
            {
                basketDb.UserId = selectedUser.IdUser;
                basketDb.ItemId = selectedItem.IdItem;
                basketDb.Count = count;
            }
        }

        App.dbContext.SaveChanges();
        this.Close();
    }

    private async void ShowError(string message)
    {
        var dlg = new Window
        {
            Title = "Ошибка",
            Content = new TextBlock { Text = message, Margin = new Thickness(20) },
            Width = 300,
            Height = 150
        };
        await dlg.ShowDialog(this);
    }
}