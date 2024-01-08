using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;
using ProInvestAPI.Domain;
using ProInvestAPI.Models;

namespace ProInvestAPI.Controllers{
    
    [ApiController]
    [Route("[controller]")]
    public class InvestmentRequestController(IConfiguration config, InvestmentRequestProvider investmentRequest) : ControllerBase
    {
        private IConfiguration config = config;

        private InvestmentRequestProvider _investmentRequest = investmentRequest;


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet("GetInvestmentRequestByUserId/{IdUser}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetInvestmentRequestByUserId(int IdUser)
        {
            try
            {
                (int code, List<InvestmentRequest> investmentRequestList, string report) = _investmentRequest.GetInvestmentRequestByUserId(IdUser);
                if (code == 200)
                {
                    return Ok(
                        investmentRequestList
                    );
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet("GetInvestmentRequestList")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetInvestmentRequestList()
        {
            try
            {
                (int code, List<InvestmentRequest> investmentRequestList, string report) = _investmentRequest.GetInvestmentRequestList();
                if (code == 200)
                {
                    return Ok(
                        investmentRequestList
                    );
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPut("PutInvestmentRequest")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> PutInvestmentRequest([FromBody] InvestmentRequestDomain investmentRequest)
        {
            try{
                InvestmentRequest newInvestment = new(){
                    InvestmentFolio = Utility.Utility.GenerateFolio(investmentRequest.ClientId),
                    Date = investmentRequest.Date,
                    Status = 1,
                    Ipaddress = investmentRequest.Ipaddress,
                    OriginOfFounds = investmentRequest.OriginOfFoundsId,
                    Bank = investmentRequest.BankId,
                    ClientId = investmentRequest.ClientId,
                    InvestmentSimulatorId = investmentRequest.InvestmentSimulatorId
                };
                var result = await _investmentRequest.PostInvestmentRequest(newInvestment);
                if(result == null){
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en PutInvestmentRequest: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}