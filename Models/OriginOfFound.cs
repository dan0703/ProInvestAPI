using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class OriginOfFound
{
    public int IdOriginOfFounds { get; set; }

    public string NameOfOrigin { get; set; } = null!;

    public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; } = new List<InvestmentRequest>();
}
