using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class Adress
{
    public int IdAdress { get; set; }

    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string InteriorNumber { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public int NeighborhoodId { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual Neighborhood Neighborhood { get; set; } = null!;
}
