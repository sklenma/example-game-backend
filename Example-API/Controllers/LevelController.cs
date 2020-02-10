using System;
using System.Threading.Tasks;
using Example_API.Requests;
using Example_Service;
using Example_Service.Exceptions;
using Example_Service.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Example_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpPost("CompleteLevel")]
        public async Task<ActionResult<PlayerAwardVO>> CompleteLevel([FromBody]CompleteLevelRequest request)
        {
            try
            {
                return await _levelService.CompleteLevel(request.PlayerId);
            }
            catch (PlayerNotFoundException e)
            {
                return NotFound("Player not found");
            }
        }
    }
}