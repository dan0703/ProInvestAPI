using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class InvestmentType
{
    public int IdInvestmentType { get; set; }

    public string TypeName { get; set; } = null!;

    public float AnualInterestRate { get; set; }

    public float GatReal { get; set; }

    public float GatNominal { get; set; }

    public virtual ICollection<InvestmentSimulator> InvestmentSimulators { get; set; } = new List<InvestmentSimulator>();
}
