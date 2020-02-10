using System.Linq;
using System.Threading.Tasks;
using Example_Persistance;
using Example_Persistance.Model;
using Example_Service.Exceptions;
using Example_Service.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Example_Service
{
    public interface ILevelService
    {
        Task<PlayerAwardVO> CompleteLevel(int playerId);
    }

    public class LevelService : ILevelService
    {
        private ExampleDbContext _context;
        private IAwardService _awardService;

        public LevelService(ExampleDbContext context, IAwardService awardService)
        {
            _context = context;
            _awardService = awardService;
        }

        public async Task<PlayerAwardVO> CompleteLevel(int playerId)
        {
            var player = await _context.Players.Where(x => x.PlayerId == playerId).FirstOrDefaultAsync();
            if (player != null)
            {
                player.Level++;
                return await _awardService.AddPlayerLvlUpAward(player);
            }
            throw new PlayerNotFoundException();
        }

    }
}