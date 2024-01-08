using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;
using ProInvestAPI.Domain;
using ProInvestAPI.Models;

namespace ProInvestAPI.Controllers{
    
    [ApiController]
    [Route("[controller]")]
    public class InvestmentSimulatorController(IConfiguration config, InvestmentSimulatorProvider investmentSimulator) : ControllerBase
    {
        private IConfiguration config = config;

        private InvestmentSimulatorProvider _investmentSimulator = investmentSimulator;


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet("GetInvestmentSimulatorById/{IdInvestmentSimulator}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetInvestmentSimulatorById(int IdInvestmentSimulator)
        {
            try
            {
                (int code, InvestmentSimulator investmentSimulator, string report) = _investmentSimulator.GetInvestmentSimulatorById(IdInvestmentSimulator);
                if (code == 200)
                {
                    return Ok(
                        investmentSimulator
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
        [HttpGet("GetInvestmentSimulatorList")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetInvestmentSimulatorList()
        {
            try
            {
                (int code, List<InvestmentSimulator> investmentSimulatorList, string report) = _investmentSimulator.GetInvestmentSimulatorList();
                if (code == 200)
                {
                    return Ok(
                        investmentSimulatorList
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
        [HttpPut("PutInvestmentSimulator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> PutInvestmentRequest([FromBody] InvestmentSimulatorDomain investmentSimulator)
        {
            try{
                InvestmentSimulator newInvestment = new(){
                    InvestmentType = investmentSimulator.InvestmentType,
                    InvestmentTerm = investmentSimulator.InvestmentTerm,
                    InvestmentAmount = investmentSimulator.InvestmentAmount,
                    EstimatedResult = investmentSimulator.EstimatedResult,
                    SimulationDate = DateTime.Now
                };
                var result = await _investmentSimulator.PostInvestmentSimulator(newInvestment);
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