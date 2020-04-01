using UnityEngine;

namespace Assets._Game.Scripts.Game.Handlers.Probability
{
    class Probabilitier : IProbabilitier
    {
        public float successProbability {get; private set;}

        public Probabilitier(float successProbability)
            => this.successProbability = successProbability;

        public bool IsSuccess()
            => RandomValue() < successProbability;

        public void Reset()
        { }

        private float RandomValue()
            => Random.Range(0f, 100f);
    }
}
