using System.Collections.Generic;

namespace Example_Persistance.Model
{
    public class Storage
    {
        public int StorageId { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public List<PlayerArticle> Items { get; set; }
        public int Capacity { get; set; }
        public int ItemsCount { get; set; }
    }
}