using System.Collections.Generic;
using System.Runtime.Serialization;
using Example_Service.ValueObjects;

namespace Example_Service.Definitions
{
    public class AwardDefinition
    {
        public int Level { get; set; }
        public int XP { get; set; }
        public bool IsActive { get; set; }
        public string Reward { get; set; }
        public List<ArticleVO> Rewards = new List<ArticleVO>();

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (!string.IsNullOrEmpty(Reward))
            {
                string[] splitArticles = Reward.Replace("(", "").Replace(")", "").Split(';');

                foreach (string s in splitArticles)
                {
                    string[] sArt = s.Split(',');
                    Rewards.Add(new ArticleVO(int.Parse(sArt[0]), int.Parse(sArt[1])));
                }

            }
        }
    }
}