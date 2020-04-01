using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityEngine
{
    class AttachingMonoBehaviour : MonoBehaviour
    {
        #region MONO_BEHAVIOUR
        private void Awake()
        {
            OnAttaching();
            OnAwakeActions();
        }
        #endregion // MONO_BEHAVIOUR

        #region PROTECTED_METHODS
        protected bool Attach<T>(ref T component) where T : UnityEngine.Object
            => component = component ??  GetComponent<T>();
        #endregion // PROTECTED_METHODS

        #region METHODS_TO_OVERRIDE
        protected virtual void OnAwakeActions()
        { }

        protected virtual void OnAttaching()
        { }
        #endregion // METHODS_TO_OVERRIDE
    }
}
