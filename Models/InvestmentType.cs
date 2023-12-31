using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class InvestmentType
{
    public int IdInvestmentType { get; set; }

    public string TypeName { get; set; } = null!;

    public int AnualInterestRate { get; set; }

    public int GatReal { get; set; }

    public int GatNominal { get; set; }

    public virtual ICollection<InvestmentSimulator> InvestmentSimulators { get; set; } = new List<InvestmentSimulator>();
}
