using Assets._Game.Scripts.Game.Objects.Reset;

namespace Assets._Game.Scripts.Game.Handlers.Probability
{
    interface IProbabilitier : IResetable
    {
        float successProbability { get; }
        bool IsSuccess();
    }
}
