using echkere_nail2.Data;
using echkere_nail2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echkere_nail2.ViewModels
{
    public partial class BasketViewModel : ViewModelBase
    {
        public List<BasketDTO> Baskets { get; set; }
        public List<User> Users { get; set; }
        public List<Item> Items { get; set; }
        public BasketViewModel()
        {
            LoadData();
            Users = App.dbContext.Users.ToList();
            Items = App.dbContext.Items.ToList();
            RefreshData();
        }

        public void LoadData()
        {
            Baskets = (from b in App.dbContext.Baskets
                       join u in App.dbContext.Users on b.UserId equals u.IdUser
                       join i in App.dbContext.Items on b.ItemId equals i.IdItem
                       select new BasketDTO
                       {
                           Id = b.IdBasket,
                           UserName = u.Name,
                           ItemName = i.Name,
                           Count = b.Count
                       }).ToList();

            OnPropertyChanged(nameof(Baskets));
        }
        public void RefreshData()
        {
            LoadData();

            Users = App.dbContext.Users.ToList();
            OnPropertyChanged(nameof(Users));

            Items = App.dbContext.Items.ToList();
            OnPropertyChanged(nameof(Items));
        }
    }
}
