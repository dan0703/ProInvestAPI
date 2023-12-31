using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class Municipality
{
    public int IdMunicipality { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PostalCode> PostalCodes { get; set; } = new List<PostalCode>();
}
