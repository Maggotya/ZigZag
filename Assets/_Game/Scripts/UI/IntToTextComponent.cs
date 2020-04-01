using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Game.Scripts.UI
{
    class IntToTextComponent : AttachingMonoBehaviour, IInfoToTextComponent<int>
    {
        #region PRIVATE_CACHE
        private Text _TextCache;
        private TextMeshPro _TextMeshProCache;
        private TextMeshProUGUI _TextMeshProUGUICache;
        #endregion // PRIVATE_CACHE

        ////////////////////////////////////

        #region ATTACHING
        protected override void OnAttaching()
        {
            Attach(ref _TextCache);
            Attach(ref _TextMeshProCache);
            Attach(ref _TextMeshProUGUICache);
        }
        #endregion // ATTACHING

        #region PUBLIC_METHODS
        public void UpdateTextComonent(int value)
        {
            OnAttaching();

            if (_TextCache)
                _TextCache.text = value.ToString();
            if (_TextMeshProCache)
                _TextMeshProCache.text = value.ToString();
            if (_TextMeshProUGUICache)
                _TextMeshProUGUICache.text = value.ToString();
        }
        #endregion // PUBLIC_METHODS
    }
}
