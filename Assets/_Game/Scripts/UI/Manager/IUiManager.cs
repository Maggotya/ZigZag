
namespace Assets._Game.Scripts.UI.Manager
{
    public interface IUiManager
    {
        void LaunchStartView();
        void LaunchScoreView();
        void LaunchPauseView();
        void LaunchResultView(int currentScore, int bestScore);
    }
}
