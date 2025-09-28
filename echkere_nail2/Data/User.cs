using System;
using System.Collections.Generic;

namespace echkere_nail2.Data;

public partial class User
{
    public int IdUser { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Description { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual Role? Role { get; set; }
}
