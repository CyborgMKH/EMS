using System;
using System.Collections.Generic;

namespace EMS.Models;

public partial class UserTable
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; }
}
