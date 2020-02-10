using System.Collections.Generic;

namespace Example_Service.ValueObjects.Responses
{
    public class AwardResponseVO
    {
        public int ForLevel { get; set; }
        public List<ArticleVO> ChangedArticles { get; set; }
        public bool Collected { get; set; }
    }
}