using System;
using System.Threading.Tasks;
using Example_Service;
using Example_Service.Exceptions;
using Example_Service.ValueObjects;
using Example_Service.ValueObjects.Responses;
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

        [HttpPost("CompleteLevel/{playerId}")]
        public async Task<ActionResult<PlayerAwardVO>> CompleteLevel(int playerId)
        {
            try
            {
                return await _levelService.CompleteLevel(playerId);
            }
            catch (PlayerNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound("Player not found");
            }
        }
    }
}