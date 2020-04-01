using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.UI
{
    class ResultViewDecorator : MonoBehaviour, IResultViewDecorator
    {
        [SerializeField] private IntUnityEvent _OnSetScore;
        [SerializeField] private IntUnityEvent _OnSetBestScore;

        public void SetScore(int score)
            => _OnSetScore?.Invoke(score);

        public void SetBestScore(int score)
            => _OnSetBestScore?.Invoke(score);
    }
}
