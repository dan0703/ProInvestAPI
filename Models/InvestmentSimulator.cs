using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class InvestmentSimulator
{
    public int IdInvestmentSimulator { get; set; }

    public int InvestmentType { get; set; }

    public int InvestmentTerm { get; set; }

    public int InvestmentAmount { get; set; }

    public float EstimatedResult { get; set; }

    public DateTime SimulationDate { get; set; }

    public string? InvestmentSimulatorcol { get; set; }

    public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; } = new List<InvestmentRequest>();

    public virtual InvestmentType InvestmentTypeNavigation { get; set; } = null!;
}
