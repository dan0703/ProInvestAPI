using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class PostalCode
{
    public int IdpostalCode { get; set; }

    public int StateId { get; set; }

    public int MunicipalityId { get; set; }

    public virtual Municipality Municipality { get; set; } = null!;

    public virtual ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();

    public virtual State State { get; set; } = null!;
}
