using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;
using ProInvestAPI.Models;

namespace ProInvestAPI.Controllers{
    
    [ApiController]
    [Route("[controller]")]
    public class BankController(IConfiguration config, BankProvider bank) : ControllerBase
    {
        private IConfiguration config = config;

        private BankProvider _bank = bank;


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet("GetBankList")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetBankList()
        {
            try
            {
                (int code, List<Bank> bankList, string report) = _bank.GetBankList();
                if (code == 200)
                {
                    return Ok(
                        bankList
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