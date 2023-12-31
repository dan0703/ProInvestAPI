using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class RequestStatus
{
    public int IdRequestStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; } = new List<InvestmentRequest>();
}
