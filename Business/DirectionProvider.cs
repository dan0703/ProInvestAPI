using System.Globalization;
using ProInvestAPI.Domain;
using ProInvestAPI.Models;

namespace ProInvestAPI.Business{
    public class DirectionProvider{
        private readonly ProInvestDbContext _connectionModel;

        public DirectionProvider(ProInvestDbContext connectionModel){
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            _connectionModel = connectionModel;
        }

        public (int, DirectionDomain, string) GetDirectionByPostalCode(int PostalCode)
        {
            int code = 200;
            DirectionDomain direction = new();
            string report = "";
            try
            {
                var temp = _connectionModel.PostalCodes.Where(x=> x.IdpostalCode.Equals(PostalCode)).First();
                var MunicipalityName = _connectionModel.Municipalities.Where(x=> x.IdMunicipality.Equals(temp.MunicipalityId))
                .Select(x=> x.Name).First();
                var StateName = _connectionModel.States.Where(x=> x.IdStates.Equals(temp.StateId))
                .Select(x=> x.Name).First();

                direction.StateName = StateName;
                direction.MunicipalityName = MunicipalityName;
                direction.PostalCode = PostalCode;        
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, direction, report);
        }

        public (int, List<Municipality>, string) GetMunicipalityListByStateId(int StateId)
        {
            int code = 200;
            List<Municipality> municipalityList = [];
            string report = "";
            try
            {
                var municipalityListTemp = _connectionModel.Municipalities.Where(x=> x.IdState.Equals(StateId)).ToList();
                 municipalityList.AddRange(municipalityListTemp);         
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, municipalityList, report);
        }

        public (int, List<State>, string) GetStateList()
        {
            int code = 200;
            List<State> stateList = [];
            string report = "";
            try
            {
                var stateListTemp = _connectionModel.States.ToList();
                 stateList.AddRange(stateListTemp);         
            }
            catch (Exception e)
            {
                code = 500;
                report = e.Message;
            }
            return (code, stateList, report);
        }

    }
}