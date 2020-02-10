using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Example_Persistance;
using Example_Persistance.Model;
using Example_Service.Constants;
using Example_Service.Definitions;
using Example_Service.Enums;
using Example_Service.Exceptions;
using Example_Service.Mappers;
using Example_Service.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Example_Service
{
    public interface IAwardService
    {
        Task<List<PlayerAward>> GetPlayerAwards(int playerId);
        Task<PlayerAwardVO> AddPlayerLvlUpAward(Player player);
        Task<List<ArticleVO>> CollectAward(int playerId, int level);
    }

    public class AwardService : IAwardService
    {
        private readonly ExampleDbContext _context;
        private readonly IAwardMapper _awardMapper;
        private readonly ICurrencyService currencyService;

        public AwardService(ExampleDbContext context, IAwardMapper awardMapper)
        {
            _context = context;
            currencyService = new CurrencyService();
            _awardMapper = awardMapper;
        }

        public async Task<List<PlayerAward>> GetPlayerAwards(int playerId)
        {
            //var awards
            return await _context.PlayerAwards.Where(x => x.Player.PlayerId == playerId).ToListAsync();
        }

        public async Task<PlayerAwardVO> AddPlayerLvlUpAward(Player player)
        {
            PlayerAward award = null;
            var levelDefinition = DefinitionLoader.GameDefinitions.AwardDefinitions
                .FirstOrDefault(x => x.Level == player.Level);

            List<AwardArticle> addedPlayerAwards = new List<AwardArticle>();


            if (levelDefinition != null && levelDefinition.Rewards.Count > 0)
            {
                award = new PlayerAward()
                {
                    Collected = false,
                    ForLevel = levelDefinition.Level,
                    Player = player,
                };

                foreach (var definitionAward in levelDefinition.Rewards)
                {
                    var awardArticle = new AwardArticle()
                    {
                        PlayerAward = award,
                        ArticleAmount = definitionAward.Amount,
                        ArticleId = definitionAward.ID,
                    };

                    addedPlayerAwards.Add(awardArticle);
                    await _context.AwardArticles.AddAsync(awardArticle);
                }

                award.AwardArticles = addedPlayerAwards;

                await _context.PlayerAwards.AddAsync(award);
                await _context.SaveChangesAsync();
            }

            return _awardMapper.MapAwardsToPlayerAwardVO(award);
        }

        private async Task<PlayerAward> GetNotCollectedPlayerAwardForLevel(int playerId, int level)
        {
            return await _context.PlayerAwards
                .Where(x => x.Player.PlayerId == playerId && x.ForLevel == level && !x.Collected).Include(x => x.AwardArticles)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ArticleVO>> CollectAward(int playerId, int level)
        {
            var award = await GetNotCollectedPlayerAwardForLevel(playerId, level);

            if (award != null)
            {
                var storage = await _context.Storages.FirstOrDefaultAsync(x => x.Player.PlayerId == playerId);
                return await AddAwardToStorage(award, playerId, storage);
            }
            throw new AwardNotFoundException();
        }

        public async Task<List<ArticleVO>> AddAwardToStorage(PlayerAward award, int playerId, Storage storage = null)
        {
            var player = await _context.Players.Where(x => x.PlayerId == playerId).FirstOrDefaultAsync();
            List<ArticleVO> awardArticleVos = new List<ArticleVO>();
            foreach (var playerAward in award.AwardArticles)
            {
                var article = new ArticleVO(playerAward.ArticleId, playerAward.ArticleAmount);
                var awardVO = await AddArticle(article, player, storage);
                awardArticleVos.Add(awardVO);
                award.Collected = true;
            }

            await _context.SaveChangesAsync();

            return awardArticleVos;
        }

        private async Task<ArticleVO> AddArticle(ArticleVO article, Player player, Storage storage = null)
        {
            if (article == null)
                return null;

            if (article.ID <= Consts.MINIMAL_CURRENCY_ID)
            {
                throw new ArgumentOutOfRangeException($"Value must be more than {Consts.MINIMAL_CURRENCY_ID}");
            }
            else if (article.ID < Consts.MINIMAL_ELIXIR_ID)
            {
                return AddCurrency(article, player);
            }
            else if (article.ID < Consts.MINIMAL_SUPPLY_ID)
            {
                throw new NotImplementedException();
            }
            else if (article.ID < Consts.MAXIMAL_ARTICLE_ID)
            {
                if (storage != null)
                    return await AddSupply(article, storage);
                return null;
            }
            return null;
        }

        private ArticleVO AddCurrency(ArticleVO article, Player player)
        {
            return currencyService.AdjustCurrency(article.ID, article.Amount, player);
        }

        private async Task<ArticleVO> AddSupply(ArticleVO article, Storage storage)
        {
            var item = storage.Items?.FirstOrDefault(x => x.ItemId == article.ID);
            if (storage.ItemsCount + article.Amount > storage.Capacity)
            {

            }
            if (item == null)
            {
                item = new PlayerArticle() { ArticleAmount = article.Amount, ItemId = article.ID};
                await _context.PlayerArticles.AddAsync(item);
            }
            else
            {
                item.ItemId += article.ID;
                item.ArticleAmount += article.Amount;
                _context.PlayerArticles.Update(item);

            }

            return new ArticleVO(item.ItemId, item.ArticleAmount);
        }
    }
}