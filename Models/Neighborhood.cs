using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class Neighborhood
{
    public int IdNeighborhood { get; set; }

    public string Name { get; set; } = null!;

    public int PostalCodeId { get; set; }

    public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();

    public virtual PostalCode PostalCode { get; set; } = null!;
}
