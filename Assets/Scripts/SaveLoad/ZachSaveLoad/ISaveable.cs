/// <summary>
/// Interface to be used with any script containing save data.
/// </summary>
public interface ISaveable
{
    object CaptureState(); // Save function
    void RestoreState(object state); // Load method
}