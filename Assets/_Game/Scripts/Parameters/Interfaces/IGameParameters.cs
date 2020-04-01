
namespace Assets._Game.Scripts.Parameters.Interfaces
{
    public interface IGameParameters
    {
        IBallParameters Ball { get; }
        IPlatformParameters Platform { get; }
        IDirectionParameters Direction { get; }
        IDifficultyParameters Difficulty { get; }
        IPlatformGeneratorParameters PlatformGenerator { get; }
        IGemGeneratorParameters GemGenerator { get; }
    }
}
