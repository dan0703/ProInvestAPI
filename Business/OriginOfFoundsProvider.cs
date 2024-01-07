using System.Globalization;
using ProInvestAPI.Models;

namespace ProInvestAPI.Business{
    public class OriginOfFoundsProvider{
        private readonly ProInvestDbContext _connectionModel;

        public OriginOfFoundsProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

        public (int, List<OriginOfFound>, string) GetOriginsOfFounds()
        {
            int code = 200;
            List<OriginOfFound> originOfFoundsList = [];
            string report = "";
            try
            {
                var listTemp = _connectionModel.OriginOfFounds.ToList();
                originOfFoundsList.AddRange(listTemp);
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, originOfFoundsList, report);
        }

    }
}