namespace ProInvestAPI{
    public class InvestmentSimulatorDomain{
        public int IdInvestmentSimulator { get; set; }

        public int InvestmentType { get; set; }

        public int InvestmentTerm { get; set; }

        public int InvestmentAmount { get; set; }

        public int EstimatedResult { get; set; }

        public string SimulationDate { get; set; } = null!;
    }
}