using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using echkere_nail2.Data;
using echkere_nail2.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace echkere_nail2;

public partial class CreateandChangeUser : Window
{
    User thisSelectedUser;
    Login thisSelectedLogin;
    public CreateandChangeUser(User user)
    {
        InitializeComponent();
        FullNameText.Text = user.Name;
        PhoneNumberText.Text = user.PhoneNumber;
        DescriptionText.Text = user.Description;
        RolesCombo.SelectedItem = user.Role;
        thisSelectedUser = user;
        thisSelectedLogin = App.dbContext.Logins.FirstOrDefault(l => l.UserId == user.IdUser);
        if (thisSelectedLogin != null)
        {
            LoginText.Text = thisSelectedLogin.Login1;
            PasswordText.Text = thisSelectedLogin.Password;
        }
        DataContext = new MainWindowViewModel();
    }

    public CreateandChangeUser()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();

    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FullNameText.Text))
        {
            ShowError("���� '���' ����������� ��� ����������.");
            return;
        }
        if (!Regex.IsMatch(FullNameText.Text, @"^[A-Za-z�-��-���\s]+$"))
        {
            ShowError("���� '���' ����� ��������� ������ ����� � �������.");
            return;
        }

        if (string.IsNullOrWhiteSpace(PhoneNumberText.Text))
        {
            ShowError("���� '����� ��������' ����������� ��� ����������.");
            return;
        }
        if (!Regex.IsMatch(PhoneNumberText.Text, @"^[0-9]+$"))
        {
            ShowError("���� '����� ��������' ����� ��������� ������ �����.");
            return;
        }

        if (PhoneNumberText.Text.Length != 12)
        {
            ShowError("����� �������� ������ ��������� ����� 12 ����.");
            return;
        }

        if (string.IsNullOrWhiteSpace(LoginText.Text))
        {
            ShowError("���� '�����' ����������� ��� ����������.");
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordText.Text) || PasswordText.Text.Length < 4)
        {
            ShowError("������ ������ ���� �� ������ 4 ��������.");
            return;
        }

        if (RolesCombo.SelectedItem == null)
        {
            ShowError("���������� ������� ���� ������������.");
            return;
        }

        if (thisSelectedUser == null)
        {
            var newUser = new User()
            {
                PhoneNumber = PhoneNumberText.Text,
                Name = FullNameText.Text,
                Description = DescriptionText.Text,
                //IdUser = thisSelectedUser != null ? thisSelectedUser.IdUser : 0,
                Role = RolesCombo.SelectedItem as Role
            };
            App.dbContext.Users.Add(newUser);
            App.dbContext.SaveChanges();

            var newLogin = new Login
            {
                Login1 = LoginText.Text,
                Password = PasswordText.Text,
                UserId = newUser.IdUser
            };
            App.dbContext.Logins.Add(newLogin);
        }
        else
        {
            var userDb = App.dbContext.Users.FirstOrDefault(x => x.IdUser == thisSelectedUser.IdUser);
            if (userDb != null)
            {
                userDb.PhoneNumber = PhoneNumberText.Text;
                userDb.Name = FullNameText.Text;
                userDb.Description = DescriptionText.Text;
                userDb.Role = RolesCombo.SelectedItem as Role;
            }

            var loginDb = App.dbContext.Logins.FirstOrDefault(l => l.UserId == thisSelectedUser.IdUser);
            if (loginDb != null)
            {
                loginDb.Login1 = LoginText.Text;
                loginDb.Password = PasswordText.Text;
            }
            else
            {
                var newLogin = new Login
                {
                    Login1 = LoginText.Text,
                    Password = PasswordText.Text,
                    UserId = thisSelectedUser.IdUser
                };
                App.dbContext.Logins.Add(newLogin);
            }
        }

        App.dbContext.SaveChanges();
        this.Close();
    }
    private async void ShowError(string message)
    {
        var dlg = new Window
        {
            Title = "������",
            Content = new TextBlock { Text = message, Margin = new Thickness(20) },
            Width = 300,
            Height = 150
        };
        await dlg.ShowDialog(this);
    }
}
