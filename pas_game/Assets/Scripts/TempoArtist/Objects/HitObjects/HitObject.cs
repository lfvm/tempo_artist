namespace TempoArtist.Objects.HitObjects
{
    public abstract class HitObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Time { get; set; }
        public int QueueID { get; set; }
    }
}