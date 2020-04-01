
namespace Assets._Game.Scripts.UI.Manager
{
    interface IUiManager
    {
        void LaunchStartView();
        void LaunchScoreView();
        void LaunchPauseView();
        void LaunchResultView(int currentScore, int bestScore);
    }
}
