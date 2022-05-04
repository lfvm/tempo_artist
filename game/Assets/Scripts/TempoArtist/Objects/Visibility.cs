namespace TempoArtist.Objects
{
    public class Visibility
    {
        public double VisibleStartOffsetMs {get; set; }
        public double VisibleEndOffsetMs { get; set; }
        public double VisibleStartStartTimeInMs { get; set; }
        public double VisibleEndStartTimeInMs {get; set;}
        public bool fadeInTriggered { get; set; }
        public bool fadeOutTriggered { get; set; }
    }
}