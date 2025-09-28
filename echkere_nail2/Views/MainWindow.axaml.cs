using Avalonia.Controls;
using echkere_nail2.Data;
using echkere_nail2.ViewModels;

namespace echkere_nail2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BtnUsers_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MainContent.Content = new UsersView();
        }

        private void BtnItems_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MainContent.Content = new ItemsView();
        }

        private void BtnBaskets_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MainContent.Content = new BasketView();
        }
    }
}