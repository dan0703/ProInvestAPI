namespace ProInvestAPI.Domain;

public class UserDomain{
     public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreateTime { get; set; }

    public int IdUser { get; set; }
}