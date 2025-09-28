using echkere_nail2.Data;
using System.Collections.Generic;
using System.Linq;

namespace echkere_nail2.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
        public MainWindowViewModel()
        {
            Users = App.dbContext.Users.ToList();
            Roles = App.dbContext.Roles.ToList();
            RefreshData();
        }

        public void RefreshData()
        {
            Users = App.dbContext.Users.ToList();
            OnPropertyChanged(nameof(Users));

            Roles = App.dbContext.Roles.ToList();
            OnPropertyChanged(nameof(Roles));
        }
    }
}
