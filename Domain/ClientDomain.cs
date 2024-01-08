namespace ProInvestAPI.Domain;

public class ClientDomain{
     public int IdClient { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Rfc { get; set; }

    public string BirthDay { get; set; } = null!;

    public string AcademicDegree { get; set; }

    public string Profession { get; set; }

    public string CompanyName { get; set; }

    public string PhoneNumber { get; set; }

}