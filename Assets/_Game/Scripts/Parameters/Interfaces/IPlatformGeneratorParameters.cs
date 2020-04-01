
namespace Assets._Game.Scripts.Parameters.Interfaces
{
    public interface IPlatformGeneratorParameters
    {
        float probabilityChangeDirection { get; }
        int stepThresholdBack { get; }
        int stepThresholdForward { get; }
        int initialStepsOneDirection { get; }
        int initialAreaSize { get; }
    }
}
