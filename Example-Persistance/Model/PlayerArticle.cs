namespace Example_Persistance.Model
{
    public class PlayerArticle
    {
        public int PlayerArticleId { get; set; }
        public Storage Storage { get; set; }
        public int ItemId { get; set; }
        public int ArticleAmount { get; set; }

    }
}