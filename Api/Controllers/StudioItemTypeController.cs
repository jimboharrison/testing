using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioItemTypes;

namespace PeoplesPartnership.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class StudioItemTypesController : ControllerBase
    {
        private readonly IGetAllStudioItemTypesHandler _getAllStudioItemTypesHandler;

        public StudioItemTypesController(IGetAllStudioItemTypesHandler getAllStudioItemTypesHandler)
        {
            _getAllStudioItemTypesHandler = getAllStudioItemTypesHandler;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            return Ok(await _getAllStudioItemTypesHandler.GetAllStudioItemTypes());
        }
        
        //Not in scope but would add a POST / PUT / DELETE in here.
        //Studio Item POST and PUT endpoints should not be responsible for updating this data
    }
}