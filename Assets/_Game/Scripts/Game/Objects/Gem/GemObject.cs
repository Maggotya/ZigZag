using System;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Gem
{
    [RequireComponent(typeof(Animator))]
    class GemObject : AttachingMonoBehaviour, IGem
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private bool _Active;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private Animator _animator;
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool active => _Active;
        public bool needDestroy { get; private set; }

        public Action<IRemoteDestroyable> onBecomeDestroyable { get; set; }
        public Action<IGem> onCollected { get; set; }
        #endregion // PUBLIC_VALUES

        #region CONSTS
        private const string COLLECT_TRIGGER = "Collect";
        private const string COLLECTING_STATE = "Collecting";

        private const string DEACTIVATE_TRIGGER = "Deactivate";
        private const string DEACTIVATE_STATE = "Deactivating";
        #endregion // CONSTS

        ///////////////////////////////////////////

        #region ATTACHING
        protected override void OnAttaching() 
            => Attach(ref _animator);
        #endregion // ATTACHING

        #region PUBLIC_METHODS
        public void Collect()
        {
            if (_Active == false)
                return;

            SetActive(false);
            LaunchCollectAnimation();
            onCollected?.Invoke(this);
        }

        public void Deactivate()
        {
            if (_Active == false)
                return;

            SetActive(false);
            LaunchDeactivateAnimation();
        }

        public void SetPositionOnSurface(Vector3 positionOnSurface)
        {
            transform.position = positionOnSurface;
        }

        public void OnAnimationStart(AnimatorStateInfo stateInfo)
        { }

        public void OnAnimationEnd(AnimatorStateInfo stateInfo)
        {
            if (stateInfo.IsName(COLLECTING_STATE))
                SetDestroyed();
            if (stateInfo.IsName(DEACTIVATE_STATE))
                SetDestroyed();
        }

        public void SetDestroyed()
        {
            if (needDestroy)
                return;

            needDestroy = true;
            onBecomeDestroyable?.Invoke(this);
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void LaunchCollectAnimation()
            => LaunchPostDestroyedAnimation(COLLECT_TRIGGER);

        private void LaunchDeactivateAnimation()
            => LaunchPostDestroyedAnimation(DEACTIVATE_TRIGGER);

        private void LaunchPostDestroyedAnimation(string name)
        {
            if (_animator)
                _animator.SetTrigger(name);
            else
                SetDestroyed();
        }

        private void SetActive(bool status)
        {
            if (_Active == status)
                return;

            _Active = status;
        }
        #endregion // PRIVATE_METHODS
    }
}
