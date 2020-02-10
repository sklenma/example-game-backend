namespace Example_Persistance.Model
{
    public class AwardArticle
    {
        public int Id { get; set; }
        public PlayerAward PlayerAward { get; set; }
        public int ArticleId { get; set; }
        public int ArticleAmount { get; set; }
    }
}