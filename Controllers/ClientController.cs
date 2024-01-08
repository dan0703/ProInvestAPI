using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;

namespace ProInvestAPI{
    [ApiController]
    [Route("[controller]")]
    public class ClientController(IConfiguration config, ClientProvider direction) : ControllerBase{
        
    }
}