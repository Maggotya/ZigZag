using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.AnimatedObjects
{
    public interface IAnimated
    {
        void OnAnimationStart(AnimatorStateInfo stateInfo);
        void OnAnimationEnd(AnimatorStateInfo stateInfo);
    }
}
