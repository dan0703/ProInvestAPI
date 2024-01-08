using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;
using ProInvestAPI.Domain;

namespace ProInvestAPI{
    [ApiController]
    [Route("[controller]")]
    public class ClientController(IConfiguration config, ClientProvider client) : ControllerBase
    {
         private IConfiguration config = config;

        private readonly ClientProvider _client = client;


        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost("RegisterClient")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> RegisterClient([FromBody] ClientDomain clientInformation){
            try{
                var result = await _client.RegisterClient(clientInformation);
                if(result == null){
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
                return Ok(result);
            }
            catch(Exception ex){
                Console.WriteLine(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}