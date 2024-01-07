using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class InvestmentRequest
{
    public int IdInvestmentRequest { get; set; }

    public int ClientId { get; set; }

    public DateTime Date { get; set; }

    public int Status { get; set; }

    public string InvestmentFolio { get; set; } = null!;

    public string Ipaddress { get; set; } = null!;

    public int InvestmentSimulatorId { get; set; }

    public int OriginOfFounds { get; set; }

    public int Bank { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual InvestmentSimulator InvestmentSimulator { get; set; } = null!;

    public virtual OriginOfFound OriginOfFoundsNavigation { get; set; } = null!;

    public virtual RequestStatus StatusNavigation { get; set; } = null!;
}
