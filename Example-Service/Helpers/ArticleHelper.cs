using System;
using Example_Persistance;
using Example_Service.Constants;
using Example_Service.Enums;

namespace Example_Service.Helpers
{
    public interface IArticleHelper
    {
        ArticleCategory GetArticleCategoryById(int articleId);
    }

    public class ArticleHelper : IArticleHelper
    {
        public ArticleHelper()
        {
        }

        public ArticleCategory GetArticleCategoryById(int articleId)
        {
            if (articleId <= Consts.MINIMAL_CURRENCY_ID)
            {
                throw new ArgumentOutOfRangeException($"Value must be more than {Consts.MINIMAL_CURRENCY_ID}");
            }
            else if (articleId < Consts.MINIMAL_ELIXIR_ID)
            {
                return ArticleCategory.Currency;
            }
            else if (articleId < Consts.MAXIMAL_ARTICLE_ID)
            {
                return ArticleCategory.Supply;
            }

            throw new ArgumentOutOfRangeException("Value is more than maximal article ID");
        }
    }
}
