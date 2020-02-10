using System;
using System.Threading.Tasks;
using Example_Service;
using Microsoft.AspNetCore.Mvc;

namespace Example_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("AddPlayer")]
        public async Task<ActionResult> AddPlayer()
        {
            var player = await _playerService.AddPlayer();
            return Ok(player);
        }

    }
}