﻿using System;
using System.Collections.Generic;

namespace echkere_nail2.Data;

public partial class Role
{
    public int IdRole { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
