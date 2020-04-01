using System.Collections.Generic;
using System.Linq;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Changer;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Producer;
using Assets._Game.Scripts.Game.Objects.Platform;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform
{
    class PlatformGenerator : MonoBehaviour, IPlatformGenerator 
    {
        #region SERIALIZE_FIELDS
        [Header("Attachments")]
        [SerializeField] private Transform _PlatformsContainer;

        [Header("Config")]
        [SerializeField] private bool _Active;

        [Header("Events")]
        [SerializeField] private IPlatformUnityEvent _OnCreated;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private IPlatformGeneratorParameters _parameters { get; set; }
        private IDifficultyParameters _difficulty { get; set; }
        private IDirectionChanger _direction { get; set; }

        private IPlatformProducer _producer { get; set; }
        private IPositionCalculator _calculator { get; set; }

        private int _currentStep { get; set; }
        private bool _silentMode { get; set; }
        private Dictionary<int, IPlatform[]> _platformsBySteps;
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool active => _Active;
        public IPlatformUnityEvent onCreated {
            get => _OnCreated;
            set => _OnCreated = value;
        }
        #endregion // PUBLIC_VALUES

        ///////////////////////////////////////////////////////////

        #region MONO_BEHAVIOUR
        private void Start()
            => CacheValues();
        #endregion // MONO_BEHAVIOUR

        #region PUBLIC_METHODS
        [Inject]
        public void Construct(IPlatformGeneratorParameters parameters, IDifficultyParameters difficulty, 
            IDirectionChanger directionChanger, IPositionCalculator positionCalculator, IPlatformProducer producer)
        {
            _producer = producer;
            _parameters = parameters;
            _difficulty = difficulty;
            _direction = directionChanger;
            _calculator = positionCalculator;

            _platformsBySteps = new Dictionary<int, IPlatform[]>();
        }

        public void GenerateInitialPlatforms()
        {
            // в silentMode не отсылаются уведомления о создании. Тем самым начальная активность
            // не будет триггерить другие системы, в т.ч. генератор кристаллов.
            _silentMode = true;
            CalculateFurtherSteps(_parameters.initialAreaSize, _parameters.initialAreaSize, true);
            CreateFurtherPlatforms();
            _silentMode = false;

            CalculateFurtherSteps(_parameters.initialStepsOneDirection, _difficulty.stepWidth, true);

            OnChangeCurrentStep();
        }

        public Vector3 GetInitialPositionOn()
        {
            var count = 0f;

            var middlePosition = Vector3.zero;
            for(var i = 0; i < _parameters.initialAreaSize; i++) {
                var platfroms = GetPlatformsOnStep(i);

                foreach (var platfrom in platfroms)
                    middlePosition += platfrom.positionToPlace;

                count += platfroms.Length;
            }

            return count > 0 ? middlePosition / count : Vector3.zero;
        }

        public void SetActive(bool status)
        {
            if (_Active == status)
                return;
            _Active = status;
        }

        public void Reset()
        {
            ResetFromCache();
            _direction.Reset();
            _calculator.Reset();
            _currentStep = default;
            _silentMode = default;

            foreach (var key in _platformsBySteps.Keys.ToArray())
                foreach (var platform in _platformsBySteps[key])
                    platform.SetDestroyed();

            _platformsBySteps.Clear();
        }
        #endregion // PUBLIC_METHODS

        #region CACHING
        private bool _active_Cache { get; set; }
        private void CacheValues()
            => _active_Cache = _Active;
        private void ResetFromCache()
            => _Active = _active_Cache;
        #endregion // CACHING

        #region STEP_OPERATIONS
        private void SetCurrentStep(int currentStep)
        {
            _currentStep = currentStep;
            OnChangeCurrentStep();
        }

        private void OnChangeCurrentStep()
        {
            CalculateFurtherSteps();
            CorrectBehindVisibility();
            CreateFurtherPlatforms();
        }

        private IPlatform[] GetPlatformsOnStep(int step)
            => _platformsBySteps.ContainsKey(step) ? _platformsBySteps[step] : new IPlatform[0];
        #endregion // STEP_OPERATIONS

        #region CALCULATION_STEPS
        private void CalculateFurtherSteps()
        {
            var stepsFurtherNow = _calculator.lastStep - _currentStep;
            var stepsFurtherNeedMore = _parameters.stepThresholdForward - stepsFurtherNow;
            CalculateFurtherSteps(stepsFurtherNeedMore, _difficulty.stepWidth, false);
        }
        private void CalculateFurtherSteps(int steps, int stepWidth, bool oneDirection)
        {
            for (var i = 0; i < steps; i++) {
                var currentDirection = _direction.currentDirection;
                var nextDirection = oneDirection ? _direction.currentDirection : _direction.ChangeDirection();
                _calculator.CalculateNextStep(currentDirection, nextDirection, stepWidth);
            }
        }
        #endregion // CALCULATION_STEPS

        #region BEHIND_VISIBILITY
        private void CorrectBehindVisibility()
        {
            var stepsBehind = GetUnusedStepsBehind();

            ClearPrecalculatedSteps(stepsBehind);
            MakePlatformsUnusedOnOldSteps(stepsBehind);
        }

        private int[] GetUnusedStepsBehind()
            => ( from key in _calculator.calculatedSteps
                 let minVisibleSteps = _currentStep - _parameters.stepThresholdBack
                 where key < minVisibleSteps
                 select key ).ToArray();

        private void ClearPrecalculatedSteps(params int[] steps)
        {
            foreach (var step in steps)
                _calculator.ClearStep(step);
        }

        private void MakePlatformsUnusedOnOldSteps(params int[] steps)
        {
            foreach (var step in steps)
                if (_platformsBySteps.ContainsKey(step)) {
                    foreach (var platform in _platformsBySteps[step])
                        platform.SetDestroyed();
                    _platformsBySteps.Remove(step);
                }
        }
        #endregion // BEHIND_VISIBILITY

        #region SHOW_FURTHER_PLATFORMS
        private void CreateFurtherPlatforms()
        {
            var startStep = _currentStep;
            var endStep = _currentStep + _parameters.stepThresholdForward;

            for (var i = startStep; i < endStep; i++) 
                if (_calculator.IsStepCalculated(i) && _platformsBySteps.ContainsKey(i) == false)
                    SetPlatformsForStep(i, _calculator.GetPositions(i));
        }

        private void SetPlatformsForStep(int step, params Vector3[] positions)
        {
            _platformsBySteps[step] = ( from pos in positions
                                        select ConfigurePlatform(
                                            _producer.Produce(), pos, step)
                                        ).ToArray();
        }

        private IPlatform ConfigurePlatform(IPlatform platform, Vector3 position, int index)
        {
            if (platform.Equals(null))
                return null;

            platform.gameObject.transform.parent = _PlatformsContainer;
            platform.gameObject.transform.position = position;

            platform.onStartBeUsing += OnBecomeUsing;
            platform.index = index;
            platform.Appear(active && !_silentMode);

            if (!_silentMode)
                onCreated?.Invoke(platform);
            return platform;
        }
        
        private void OnBecomeUsing(IPlatform platform)
        {
            if (platform.Equals(null) == false)
                SetCurrentStep(platform.index);
        }
        #endregion // HANDLING_PLATFORMS_EVENTS
    }
}
