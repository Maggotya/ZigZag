using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets;

namespace UnityEngine
{
    class CheckedMonoBehaviour : MonoBehaviour
    {
        #region MONO_BEHAVIOUR
        private void Awake()
        {
            OnCheckFields();
            OnAwakeActions();
        }
        #endregion // MONO_BEHAVIOUR

        #region PROTECTED_METHODS
        protected bool CheckField<T>(T component) where T : UnityEngine.Object
        {
            if (component.Equals(null)) {
                throw new NullReferenceException($"{GetType().Name}: {typeof(T)} isn't attached!");
            }
            return true;
        }
        #endregion // PROTECTED_METHODS

        #region METHODS_TO_OVERRIDE
        protected virtual void OnAwakeActions()
        { }

        protected virtual void OnCheckFields()
        { }
        #endregion // METHODS_TO_OVERRIDE
    }
}
