namespace Example_Service.Definitions
{
    public class DefinitionLoader
    {
        private static Definitions _gd;

        public static Definitions GameDefinitions
            {
                get
                {
                    if (_gd == null)
                    {
                        _gd = new Definitions();
                    }

                    return _gd;
                }
        }
    }
}