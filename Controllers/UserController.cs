namespace ProInvestAPI.Controllers{
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using ProInvestAPI.Business;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class UserController(IConfiguration config, UserProvider login) : ControllerBase
    {
        private IConfiguration config = config;

        private readonly UserProvider _user = login;

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost("RegisterUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> RegisterUser([FromBody] UserDomain userInformation){
            try{


                var result = await _user.Register(userInformation);
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

