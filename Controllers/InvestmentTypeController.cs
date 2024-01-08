using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;
using ProInvestAPI.Models;

namespace ProInvestAPI.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class InvestmentTypeController(IConfiguration config, InvestmentTypeProvider investment) : ControllerBase
    {
        private IConfiguration config = config;
        private InvestmentTypeProvider _investmentType = investment;

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet("GetInvestmentTypes")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetInvestmentTypes()
        {
            try
            {
                (int code, List<InvestmentType> investmentTypes, string report) = _investmentType.GetInvestmentTypes();
                if (code == 200)
                {
                    return Ok(
                        investmentTypes
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
    }
}