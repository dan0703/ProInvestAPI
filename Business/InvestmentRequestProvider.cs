using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ProInvestAPI.Models;

namespace ProInvestAPI.Business{
    public class InvestmentRequestProvider{
        private readonly ProInvestDbContext _connectionModel;

        public InvestmentRequestProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

        public (int, List<InvestmentRequest>, string) GetInvestmentRequestByUserId(int IdUser)
        {
            int code = 200;
            List<InvestmentRequest> investmentRequestList = [];
            string report = "";
            try
            {
                var listTemp = _connectionModel.InvestmentRequests.Where(x=> x.ClientId.Equals(IdUser)).ToList();
                investmentRequestList.AddRange(listTemp);
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, investmentRequestList, report);
        }
        
        public (int, List<InvestmentRequest>, string) GetInvestmentRequestList()
        {
            int code = 200;
            List<InvestmentRequest> investmentRequestList = [];
            string report = "";
            try
            {
                var listTemp = _connectionModel.InvestmentRequests.ToList();
                investmentRequestList.AddRange(listTemp);
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, investmentRequestList, report);
        }

        public async Task<string> PostInvestmentRequest(InvestmentRequest investmentRequest)
        {
            using var transaction = await _connectionModel.Database.BeginTransactionAsync();
            try
            {
                bool canConnect = await _connectionModel.Database.CanConnectAsync();

                if (!canConnect)
                {
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                var lastInvestment = _connectionModel.InvestmentRequests
                    .OrderBy(x => x.IdInvestmentRequest)
                    .LastOrDefault();

                int lastId = (lastInvestment != null) ? lastInvestment.IdInvestmentRequest : 0;
                investmentRequest.IdInvestmentRequest = lastId + 1;
                _connectionModel.InvestmentRequests.Add(investmentRequest);

                int changes = await _connectionModel.SaveChangesAsync();

                if (changes > 0)
                {
                    var investmentRequestAux = await _connectionModel.InvestmentRequests
                        .Where(x => x.ClientId == investmentRequest.ClientId)
                        .FirstOrDefaultAsync();

                    if (investmentRequestAux != null)
                    {
                        await transaction.CommitAsync();
                        return investmentRequestAux.IdInvestmentRequest.ToString();
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