using System;
using System.Threading.Tasks;
using Example_Persistance;
using Example_Service;
using Example_Service.Exceptions;
using Example_Service.Mappers;
using Example_Service.ValueObjects.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Example_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly IAwardService _rewardService;
        private readonly IAwardMapper _rewardMapper;

        public AwardController(IAwardService rewardService, IAwardMapper rewardMapper)
        {
            _rewardService = rewardService;
            _rewardMapper = rewardMapper;
        }

        [HttpPost("collectAward/{playerId}/{level}")]
        public async Task<ActionResult<AwardResponseVO>> CollectAward(int playerId, int level)
        {
            try
            {
                var rewardArticles = await _rewardService.CollectAward(playerId, level);

                if (rewardArticles != null && rewardArticles.Count > 0)
                {
                    return Ok(_rewardMapper.MapAwardResponseVo(rewardArticles, true, level));
                }

                return BadRequest("FAIL - REWARDS ALREADY COLLECTED");
            }
            catch (AwardNotFoundException e)
            {
                return NotFound("Award does not exist");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}