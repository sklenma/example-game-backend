using System.Collections.Generic;
using System.Linq;
using Example_Persistance.Model;
using Example_Service.Helpers;
using Example_Service.ValueObjects;
using Example_Service.ValueObjects.Responses;

namespace Example_Service.Mappers
{
    public interface IAwardMapper
    {
        PlayerAwardVO MapAwardsToPlayerAwardVO(PlayerAward award);
        AwardResponseVO MapAwardResponseVo(List<ArticleVO> changedArticles, bool collected, int forLevel);
    }

    public class AwardMapper : IAwardMapper
    {
        public IArticleHelper ArticleHelper { get; set; }

        public AwardMapper(IArticleHelper helper)
        {
            ArticleHelper = helper;
        }

        public PlayerAwardVO MapAwardsToPlayerAwardVO(PlayerAward award)
        {
            int forLevel = 0;
            bool collected = false;
            List<ArticleVO> awardedArticles = null;
            if (award != null && award.AwardArticles.Any())
            {
                collected = award.Collected;
                forLevel = award.ForLevel;
                awardedArticles = award.AwardArticles
                    .Select(x => new ArticleVO()
                    {
                        Amount = x.ArticleAmount,
                        ID = x.ArticleId,
                        Category = ArticleHelper.GetArticleCategoryById(x.ArticleId)
                    })
                    .ToList();
            }

            return new PlayerAwardVO()
            {
                AwardedArticles = awardedArticles,
                Collected = collected,
                ForLevel = forLevel
            };
        }

        public AwardResponseVO MapAwardResponseVo(List<ArticleVO> changedArticles, bool collected, int forLevel)
        {

            return new AwardResponseVO()
            {
                Collected = collected,
                ForLevel = forLevel,
                ChangedArticles = changedArticles,
            };
        }

    }
}
