/// <summary>
/// Interface to be inherited on any script containing saveable data.
/// </summary>
public interface ISaveable
{
    object CaptureState(); // Save function
    void RestoreState(object state); // Load method
}