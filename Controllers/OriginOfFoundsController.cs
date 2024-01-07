using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;
using ProInvestAPI.Models;

namespace ProInvestAPI.Controllers{
    
    [ApiController]
    [Route("[controller]")]
    public class OriginOfFoundsController(IConfiguration config, OriginOfFoundsProvider originOfFounds) : ControllerBase
    {
        private IConfiguration config = config;
        private OriginOfFoundsProvider _originOfFounds = originOfFounds;

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet("GetOriginsOfFounds")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetOriginsOfFounds()
        {
            try
            {
                (int code, List<OriginOfFound> originsOfFounds, string report) = _originOfFounds.GetOriginsOfFounds();
                if (code == 200)
                {
                    return Ok(
                        originsOfFounds
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