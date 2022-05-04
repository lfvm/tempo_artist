namespace TempoArtist.Objects
{
    public interface IHittable : IInteractable
    {
        bool IsHitAttempted { get; }
    }
}