using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using echkere_nail2.Data;
using echkere_nail2.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace echkere_nail2;

public partial class CreateandchangeItem : Window
{
    Item thisSelectedItem;
    public CreateandchangeItem(Item item)
    {
        InitializeComponent();
        NameText.Text = item.Name;
        DescriptionText.Text = item.Description;
        PriceText.Text = item.Price.ToString();
        thisSelectedItem = item;
        DataContext = new ItemViewModel();
    }

    public CreateandchangeItem()
    {
        InitializeComponent();       
        DataContext = new ItemViewModel();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!Regex.IsMatch(PriceText.Text, @"^[0-9]+$"))
        {
            ShowError("Поле 'Цена' может содержать только цифры.");
            return;
        }

        if (thisSelectedItem == null)
        {
            var newItem = new Item()
            {
                Name = NameText.Text,
                Description = DescriptionText.Text,
                Price = int.Parse(PriceText.Text),
            };
            App.dbContext.Items.Add(newItem);
            App.dbContext.SaveChanges();
        }
        else
        {
            var itemDb = App.dbContext.Items.FirstOrDefault(x => x.IdItem == thisSelectedItem.IdItem);
            if (itemDb != null)
            {
                itemDb.Name = NameText.Text;
                itemDb.Description = DescriptionText.Text;
                itemDb.Price = int.Parse(PriceText.Text);
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