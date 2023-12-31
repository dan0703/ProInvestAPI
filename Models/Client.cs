using System;
using System.Collections.Generic;

namespace ProInvestAPI.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Rfc { get; set; }

    public string BirthDay { get; set; } = null!;

    public string? AcademicDegree { get; set; }

    public string? Profession { get; set; }

    public string? CompanyName { get; set; }

    public string? PhoneNumber { get; set; }

    public int? AdressId { get; set; }

    public virtual Adress? Adress { get; set; }

    public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; } = new List<InvestmentRequest>();

    public virtual User? User { get; set; }
}
