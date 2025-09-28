using echkere_nail2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echkere_nail2.ViewModels
{
    public partial class ItemViewModel : ViewModelBase
    {
        public List<Item> Items { get; set; }

        public ItemViewModel() 
        {
            Items = App.dbContext.Items.ToList();

        }

        public void RefreshData()
        {
            Items = App.dbContext.Items.ToList();
            OnPropertyChanged(nameof(Items));
        }
    }
}
