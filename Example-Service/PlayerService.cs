using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Example_Persistance;
using Example_Persistance.Model;
using Example_Service.ValueObjects;
using Example_Service.ValueObjects.Responses;

namespace Example_Service
{
    public interface IPlayerService
    {
        Task<AddPlayerResponseVO> AddPlayer();
    }

    public class PlayerService : IPlayerService
    {
        private ExampleDbContext _context;

        public PlayerService(ExampleDbContext context)
        {
            _context = context;
        }


        public async Task<AddPlayerResponseVO> AddPlayer()
        {
            var player = await _context.Players.AddAsync(new Player()
            {
                Gems = 0,
                Gold = 50,
                Level = 1,
            });

            var storage = await _context.Storages.AddAsync(new Storage()
            {
                Items = new List<PlayerArticle>(),
                ItemsCount = 0,
                Capacity = 3,
                Player = player.Entity
            });

            player.Entity.Storage = storage.Entity;


            await _context.SaveChangesAsync();

            return new AddPlayerResponseVO()
            {
                PlayerId = player.Entity.PlayerId,
                StorageCapacity = player.Entity.Storage.Capacity,
                Gold = player.Entity.Gold,
                Gems = player.Entity.Gems
            };
        }

    }
}