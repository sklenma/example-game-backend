namespace Example_Service.ValueObjects.Responses
{
    public class AddPlayerResponseVO
    {
        public int PlayerId { get; set; }
        public int StorageCapacity{ get; set; }
        public int Level { get; set; }
        public int Gold { get; set; }
        public int Gems { get; set; }
    }
}