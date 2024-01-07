using System.Globalization;
using ProInvestAPI.Models;

namespace ProInvestAPI.Business{
    public class BankProvider{
        private readonly ProInvestDbContext _connectionModel;

        public BankProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

        public (int, List<Bank>, string) GetBankList()
        {
            int code = 200;
            List<Bank> bankList = [];
            string report = "";
            try
            {
                var listTemp = _connectionModel.Banks.ToList();
                bankList.AddRange(listTemp);
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, bankList, report);
        }

    }
}