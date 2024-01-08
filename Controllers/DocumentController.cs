using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;

namespace ProInvestAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class DocumentController(IConfiguration config, DocumentProvider login) : ControllerBase
    {

    }
}