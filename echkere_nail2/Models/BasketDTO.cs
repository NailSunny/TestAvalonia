using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echkere_nail2.Models
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public int? Count { get; set; }
    }
}
