using System;
using System.Collections.Generic;

namespace echkere_nail2.Data;

public partial class Item
{
    public int IdItem { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
}
