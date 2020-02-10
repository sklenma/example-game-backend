using System.Collections.Generic;

namespace Example_Persistance.Model
{
    public class PlayerAward
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public int ForLevel { get; set; }
        public List<AwardArticle> AwardArticles { get; set; }
        public bool Collected { get; set; }
    }
}