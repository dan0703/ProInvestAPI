namespace ProInvestAPI.Controllers{
    using Domain;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.AspNetCore.Mvc;
    using ProInvestAPI.Business;
    using System.Net;
    using System.Security.Claims;
    using System.Text;

    [ApiController]
    [Route("[controller]")]
    public class UserController(IConfiguration config, UserProvider login) : ControllerBase
    {
        private IConfiguration config = config;

        private readonly UserProvider _user = login;

        [HttpGet("GetUsers")]
        public ActionResult GetUsertest()
        {
            (int code, List<UserDomain> userList, string report) = _user.GetUsers();
            if (code == 200)
            {
                return Ok(
                    userList
                );
            }
            else
            {
                return NotFound();
            }
        }



        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost("LoginUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> ValidateLoginUser([FromBody] LoginDomain loginCredentials)
        {
            try
            {
                UserDomain user = await _user.Login(loginCredentials);
                string jwtToken = "";
                if (user == null)
                {
                    return NotFound();
                } else{
                     jwtToken = GenerateToken(user);
                }
                return Ok(new JsonResult(new {jwtToken, user.IdUser}));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

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

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GenerateToken(UserDomain userDomain)
        {
            string token = "";
            try
            {
                var claims = new[]
            {
            new Claim(ClaimTypes.Name, userDomain.Username),
            new Claim(ClaimTypes.Email, userDomain.Email)

        };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var securityToken = new JwtSecurityToken(
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(60),
                                    signingCredentials: creds
                    );
                token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al generar el token: {e.Message}");
                throw;
            }
            return token;
        }
    }
}