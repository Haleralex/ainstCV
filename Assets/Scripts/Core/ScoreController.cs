using System;
using System.Collections;
using System.Collections.Generic;
using ainst.Gameplay;
using UnityEngine;

namespace ainst.Core
{
    public class ScoreController : MonoBehaviour
    {
        public event Action<int> ScoreChanged;
        private int _currentScore = 0;

        [SerializeField]
        private Spawner _spawner;

        private List<InteractableElement> _subscribedSpawnables = new List<InteractableElement>();

        void OnEnable()
        {
            _spawner.Spawned += OnSpawned;
            _spawner.Respawned += NullifyScore;
        }
        void OnDisable()
        {
            foreach (var k in _subscribedSpawnables)
            {
                k.GetTouched -= OnSpawnableTouched;
            }

            _spawner.Spawned -= OnSpawned;
            _spawner.Respawned -= NullifyScore;
        }

        private void OnSpawned(InteractableElement obj)
        {
            if (_subscribedSpawnables.Contains(obj))
                return;

            obj.GetTouched += OnSpawnableTouched;
            _subscribedSpawnables.Add(obj);
        }

        private void OnSpawnableTouched(InteractableElement obj)
        {
            _spawner.ReturnToPool(obj);
            _currentScore += 1;
            ScoreChanged?.Invoke(_currentScore);
        }

        private void NullifyScore()
        {
            _currentScore = 0;
            ScoreChanged?.Invoke(_currentScore);
        }
    }
}