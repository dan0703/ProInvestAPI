using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class InvestmentSimulator
{
    public int IdInvestmentSimulator { get; set; }

    public int InvestmentType { get; set; }

    public int InvestmentTerm { get; set; }

    public int InvestmentAmount { get; set; }

    public int EstimatedResult { get; set; }

    public string SimulationDate { get; set; } = null!;

    public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; } = new List<InvestmentRequest>();

    public virtual InvestmentType InvestmentTypeNavigation { get; set; } = null!;
}
