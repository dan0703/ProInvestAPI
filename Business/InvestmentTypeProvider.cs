using System.Globalization;
using ProInvestAPI.Models;

namespace ProInvestAPI.Business{
    public class InvestmentTypeProvider{
        private readonly ProInvestDbContext _connectionModel;

        public InvestmentTypeProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

        public (int, List<InvestmentType>, string) GetInvestmentTypes()
        {
            int code = 200;
            List<InvestmentType> investmentTypeList = [];
            string report = "";
            try
            {
                var listTemp = _connectionModel.InvestmentTypes.ToList();
                investmentTypeList.AddRange(listTemp);
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, investmentTypeList, report);
        }

    }
}