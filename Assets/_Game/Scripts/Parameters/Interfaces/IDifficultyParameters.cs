
using Assets._Game.Scripts.Parameters.Classes;

namespace Assets._Game.Scripts.Parameters.Interfaces
{
    public interface IDifficultyParameters
    {
        Difficulty current { get; }
        int stepWidth { get; }

        int StepWidth(Difficulty difficulty);
    }
}
