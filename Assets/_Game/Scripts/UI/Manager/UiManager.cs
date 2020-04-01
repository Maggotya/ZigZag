using System.Linq;
using ModestTree;
using UnityEngine;

namespace Assets._Game.Scripts.UI.Manager
{
    class UiManager : CheckedMonoBehaviour, IUiManager
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private View _StartView;
        [SerializeField] private View _ScoreView;
        [SerializeField] private View _PauseView;
        [SerializeField] private View _ResultView;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private IView[] _views => new IView[]
            { _StartView, _ScoreView, _PauseView, _ResultView };
        #endregion // PRIVATE_VALUES

        ////////////////////////////////////

        #region CHECKINGS
        protected override void OnCheckFields()
        {
            CheckField(_StartView);
            CheckField(_ScoreView);
            CheckField(_ResultView);
            CheckField(_PauseView);
        }
        #endregion // CHECKINGS

        #region PUBLIC_METHODS
        public void LaunchStartView()
        {
            CloseAll(_StartView);
            _StartView?.Enable();
        }

        public void LaunchScoreView()
        {
            CloseAll(_ScoreView);
            _ScoreView?.Enable();
        }

        public void LaunchPauseView()
        {
            CloseAll(_PauseView);
            _PauseView?.Enable();
        }

        public void LaunchResultView(int currentScore, int bestScore)
        {
            CloseAll(_ResultView);
            _ResultView?.Enable();

            var decorator = _ResultView?.GetComponent<IResultViewDecorator>();
            decorator?.SetScore(currentScore);
            decorator?.SetBestScore(bestScore);
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void CloseAll(params IView[] exceptions)
        {
            foreach (var view in _views.Except(exceptions))
                view?.Disable();
        }
        #endregion // PUBLIC_METHODS
    }
}
