namespace ProInvestAPI.Domain{
    public class InvestmentRequestDomain{
        public int IdInvestmentRequest { get; set; }

        public int ClientId { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }

        public string InvestmentFolio { get; set; } = null!;

        public string Ipaddress { get; set; } = null!;

        public int InvestmentSimulatorId { get; set; }

        public int OriginOfFoundsId { get; set; }

        public int BankId { get; set; }
    }
}