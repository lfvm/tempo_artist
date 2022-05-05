using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TempoArtist.Beatmaps;

namespace TempoArtist.Utils
{
    public static class JsonParser
    {
        public static Beatmap JsonToBeatmap(string json)
        {
            return JsonConvert.DeserializeObject<Beatmap>(json);
        }

        public static string BeatmapToJson(Beatmap beatmap)
        {
            var beatmapStr = JsonConvert.SerializeObject(beatmap);
            return beatmapStr;
        }
    }
}