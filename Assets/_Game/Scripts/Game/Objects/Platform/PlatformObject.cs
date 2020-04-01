using System;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Platform
{
    [RequireComponent(typeof(Animator))]
    class PlatformObject : CheckedMonoBehaviour, IPlatform
    {
        #region SERIALIZE_FIELDS
        [Header("Attachments")]
        [SerializeField] private Animator _Animator;
        [SerializeField] private PlatformSizer _PlatformSizer;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public bool isActive { get; private set; } = true;
        public bool isUsing { get; private set; }
        public int index { get; set; }

        public bool needDestroy { get; private set; }

        public Action<IPlatform> onStartBeUsing { get; set; }
        public Action<IPlatform> onFinishBeUsing { get; set; }
        public Action onDeactivating { get; set; }
        public Action<IRemoteDestroyable> onBecomeDestroyable { get; set; }

        public Vector3 positionToPlace {
            get {
                if (_PlatformSizer == null)
                    return transform.position;

                var mainPosition = transform.position;
                var offset = Vector3.up * ( _PlatformSizer.thickness / 2f );

                return mainPosition + offset;
            }
        }
        #endregion // PUBLIC_VALUES

        #region CONSTS
        private const string APPEAR_TRIGGER = "Appear";
        private const string INITIAL_TRIGGER = "Initial";
        private const string DEACTIVATE_TRIGGER = "Deactivate";
        private const string DEACTIVATING_STATE = "Deactivating";
        #endregion // CONSTS

        //////////////////////////////////////

        #region CHECKING
        protected override void OnCheckFields()
        {
            CheckField(_Animator);
            CheckField(_PlatformSizer);
        }
        #endregion // CHECKING

        #region INJECT
        [Inject]
        public void Conctruct(IPlatformParameters parameters)
            => _PlatformSizer?.Set(parameters.size, parameters.thickness);
        #endregion // INJECT

        #region PUBLIC_METHODS
        public void Appear(bool animated)
        {
            var trigger = animated ? APPEAR_TRIGGER : INITIAL_TRIGGER;
            _Animator?.SetTrigger(trigger);
        }

        public void StartBeUsing()
        {
            if (isActive == false || isUsing)
                return;

            isUsing = true;
            onStartBeUsing?.Invoke(this);
        }

        public void FinishBeUsing()
        {
            if (isActive == false || isUsing == false)
                return;

            isUsing = false;
            onFinishBeUsing?.Invoke(this);
            Deactivate();
        }

        public void OnAnimationStart(AnimatorStateInfo stateInfo)
        { }

        public void OnAnimationEnd(AnimatorStateInfo stateInfo)
        {
            if (stateInfo.IsName(DEACTIVATING_STATE))
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
        private void Deactivate()
        {
            if (isActive == false)
                return;

            isActive = false;
            LaunchDeactivateAnimation();
            onDeactivating?.Invoke();
        }

        private void LaunchDeactivateAnimation()
        {
            if (_Animator)
                _Animator.SetTrigger(DEACTIVATE_TRIGGER);
            else
                SetDestroyed();
        }
        #endregion // PRIVATE_METHODS
    }
}
