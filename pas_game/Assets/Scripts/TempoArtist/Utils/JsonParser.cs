using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TempoArtist.Beatmaps;

namespace TempoArtist.Utils
{
    public static class JsonParser
    {
        public static Beatmap JsonToBeatmap(string jsonPath)
        {
            var beatMap = JsonConvert.DeserializeObject<Beatmap>(File.ReadAllText(jsonPath));
            return beatMap;
        }
    }
}