using System;
using System.Collections.Generic;

namespace echkere_nail2.Data;

public partial class Basket
{
    public int IdBasket { get; set; }

    public int? UserId { get; set; }

    public int? ItemId { get; set; }

    public int? Count { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? User { get; set; }
}
