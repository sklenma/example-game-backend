using System.Collections.Generic;

namespace Example_Service.ValueObjects
{
    public class PlayerAwardVO
    {
        public int ForLevel { get; set; }
        public List<ArticleVO> AwardedArticles { get; set; }
        public bool Collected { get; set; }
    }
}