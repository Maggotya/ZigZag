
using Assets._Game.Scripts.Parameters.Classes;

namespace Assets._Game.Scripts.Parameters.Interfaces
{
    public interface IGemGeneratorParameters
    {
        int platformsInBlock { get; }
        PlacementType placementType { get; }
    }
}
