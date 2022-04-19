using System;

namespace TempoArtist.Objects
{
    public interface IInteractable
    {
        int InteractionID { get; set; }
        int AccuracyLaybackMs { get; set; }
        
        double PerfectInteractionTimeInMs { get; set; }
        double InteractionBoundsStartTimeInMs { get; set; }
        double InteractionBoundsEndTimeInMs { get; set; }

        void TryInteract();

        bool IsInInteractionBound(double time);

        event EventHandler OnInteract;
    }
}