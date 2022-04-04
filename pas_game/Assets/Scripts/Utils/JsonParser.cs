using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BeatmapConverter.Utils
{
    public class JsonParser
    {
        public Beatmap JsonToBeatmap(string jsonPath)
        {
            var beatMap = JsonConvert.DeserializeObject<Beatmap>(File.ReadAllText(jsonPath));
            return beatMap;
        }
    }
}