using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? CreateTime { get; set; }

    public int IdUser { get; set; }

    public virtual Client IdUserNavigation { get; set; } = null!;
}
