using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProInvestAPI.Business;
using ProInvestAPI.Domain;
using ProInvestAPI.Models;

namespace ProInvestAPI.Controllers{
    
    [ApiController]
    [Route("[controller]")]
    public class DirectionsController(IConfiguration config, DirectionProvider direction) : ControllerBase
    {
        private IConfiguration config = config;

        private DirectionProvider _direction = direction;

        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpGet("GetDirectionByPostalCode/{IdPostalCode}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetDirectionByPostalCode(int IdPostalCode)
        {
            try
            {
                (int code, DirectionDomain direction, string report) = _direction.GetDirectionByPostalCode(IdPostalCode);
                if (code == 200)
                {
                    return Ok(
                        direction
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
        [HttpGet("GetMunicipalityByStateId/{IdState}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetMunicipalityByStateId(int IdState)
        {
            try
            {
                (int code, List<Municipality> municipalityList, string report) = _direction.GetMunicipalityListByStateId(IdState);
                if (code == 200)
                {
                    return Ok(
                        municipalityList
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
        [HttpGet("GetStateList")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetStateList()
        {
            try
            {
                (int code, List<State> stateList, string report) = _direction.GetStateList();
                if (code == 200)
                {
                    return Ok(
                        stateList
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