using Microsoft.AspNetCore.SignalR;

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

        public int InvestmentAmout { get; set; }

        public int InvestmentTerm { get; set;}

        public float StimatedResult { get ; set;}
        public int InvestmentType { get; set; }

        public int DocumentType {get; set;}

    }
}