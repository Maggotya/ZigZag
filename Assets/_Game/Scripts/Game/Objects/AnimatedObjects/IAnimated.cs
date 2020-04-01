using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.AnimatedObjects
{
    interface IAnimated
    {
        void OnAnimationStart(AnimatorStateInfo stateInfo);
        void OnAnimationEnd(AnimatorStateInfo stateInfo);
    }
}
