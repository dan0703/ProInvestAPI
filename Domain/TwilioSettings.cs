namespace ProInvestAPI.Domain
{
    public class TwilioSettings
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string TwilioPhoneNumber { get; set; }
        public string VerifyServiceSid { get; internal set; }
    }
}