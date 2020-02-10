using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Example_Service.Definitions
{
    public class Definitions
    {
        public string ResourcePath;
        public AwardDefinition[] AwardDefinitions;
        public void LoadDefinitions()
        {
            AwardDefinitions = LoadJson<AwardDefinition[]>("AwardDefinition");
        }

        T LoadJson<T>(string fileName)
        {
            using (StreamReader r = new StreamReader(Path.Combine(ResourcePath, fileName + ".json")))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}