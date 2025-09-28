using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using echkere_nail2.Data;
using echkere_nail2.ViewModels;
using System.Linq;

namespace echkere_nail2;

public partial class UsersView : UserControl
{
    public UsersView()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var createAndChangeUserWindow = new CreateandChangeUser();
        await createAndChangeUserWindow.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel?.RefreshData();
    }

    private async void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        var selectedUser = MainDataGridUsers.SelectedItem as User;
        if (selectedUser == null) return;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var createAndChangeUserWindow = new CreateandChangeUser(selectedUser);
        await createAndChangeUserWindow.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel?.RefreshData();
    }

    private void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var selectedUser = MainDataGridUsers.SelectedItem as User;
        if (selectedUser == null) return;

        var logins = App.dbContext.Logins.Where(l => l.UserId == selectedUser.IdUser).ToList();
        if (logins.Any())
        {
            App.dbContext.Logins.RemoveRange(logins);
        }

        App.dbContext.Users.Remove(selectedUser);
        App.dbContext.SaveChanges();

        var viewModel = DataContext as MainWindowViewModel;
        viewModel?.RefreshData();
    }
}