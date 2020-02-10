using System.Collections.Generic;

namespace Example_Persistance.Model
{
    public class Player
    {
        public int PlayerId { get; set; }
        public Storage Storage { get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public int Gems { get; set; }
    }
}