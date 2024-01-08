using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ProInvestAPI.Models;

namespace ProInvestAPI.Business{
    public class InvestmentSimulatorProvider{
        private readonly ProInvestDbContext _connectionModel;

        public InvestmentSimulatorProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

        public (int, InvestmentSimulator, string) GetInvestmentSimulatorById(int IdInvestmentSimulator)
        {
            int code = 200;
            InvestmentSimulator investmentSimulator = new();
            string report = "";
            try
            {
                var investmentTemp = _connectionModel.InvestmentSimulators.Where(x=> x.IdInvestmentSimulator.Equals(IdInvestmentSimulator)).FirstOrDefault();
                investmentSimulator = investmentTemp;         
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, investmentSimulator, report);
        }
        
        public (int, List<InvestmentSimulator>, string) GetInvestmentSimulatorList()
        {
            int code = 200;
            List<InvestmentSimulator> investmentSimulatorList = [];
            string report = "";
            try
            {
                var listTemp = _connectionModel.InvestmentSimulators.ToList();
                investmentSimulatorList.AddRange(listTemp);
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, investmentSimulatorList, report);
        }

        public async Task<string> PostInvestmentSimulator(InvestmentSimulator investmentSimulator)
        {
            using var transaction = await _connectionModel.Database.BeginTransactionAsync();
            try
            {
                bool canConnect = await _connectionModel.Database.CanConnectAsync();

                if (!canConnect)
                {
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                var lastInvestment = _connectionModel.InvestmentSimulators
                    .OrderBy(x => x.IdInvestmentSimulator)
                    .LastOrDefault();

                int lastId = (lastInvestment != null) ? lastInvestment.IdInvestmentSimulator : 0;
                investmentSimulator.IdInvestmentSimulator = lastId + 1;
                _connectionModel.InvestmentSimulators.Add(investmentSimulator);

                int changes = await _connectionModel.SaveChangesAsync();

                if (changes > 0)
                {
                    var investmentRequestAux = await _connectionModel.InvestmentSimulators
                        .Where(x => x.IdInvestmentSimulator == investmentSimulator.IdInvestmentSimulator)
                        .FirstOrDefaultAsync();

                    if (investmentRequestAux != null)
                    {
                        await transaction.CommitAsync();
                        return investmentRequestAux.IdInvestmentSimulator.ToString();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return null;
                    }
                }
                else
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error en PostInvestmentRequest: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw new Exception("Error durante la transacción de registro de investmentRequest.", ex);
            }
        }
    }
}