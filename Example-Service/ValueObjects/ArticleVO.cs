using System;
using Example_Service.Enums;

namespace Example_Service.ValueObjects
{
    public class ArticleVO
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public ArticleCategory Category { get; set; }

        public ArticleVO()
        {
        }

        public ArticleVO(int id, int amount)
        {
            ID = id;
            Amount = amount;
            Category = GetArticleCategory(id);
        }

        public ArticleCategory GetArticleCategory(int articleId)
        {
            if (articleId <= 1000)
                throw new ArgumentOutOfRangeException("Value cannot be less then 101000");
            else if (articleId < 2000)
            {
                return ArticleCategory.Currency;
            }
            else if (articleId < 3000)
            {
                throw new NotImplementedException();
            }
            else if (articleId < 4000)
            {
                return ArticleCategory.Supply;
            }
            else if (articleId < 5000)
            {
                throw new NotImplementedException();
            }

            throw new Exception("Id of article is not valid");
        }
    }
}