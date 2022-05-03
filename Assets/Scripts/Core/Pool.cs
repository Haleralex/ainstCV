using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ainst.Gameplay;
using UnityEngine;

namespace ainst.Core
{
    public class Pool
    {
        private InteractableElement _extendedPrefab;

        private Transform _mainTransform;

        private Dictionary<InteractableElement, SpawnableCondition> _spawnables = new Dictionary<InteractableElement, SpawnableCondition>();

        private bool CanExtend = false;

        public Pool(List<InteractableElement> spawnables, InteractableElement extendedPrefab, Transform mainTransform, bool canExtend = false)
        {
            CanExtend = canExtend;
            _extendedPrefab = extendedPrefab;
            _mainTransform = mainTransform;

            foreach (var spawnable in spawnables)
            {
                _spawnables.Add(spawnable, SpawnableCondition.InPool);
            }
        }

        public bool HasFreeElement()
        {
            return _spawnables
                .Where(a => a.Value == SpawnableCondition.InPool).Count() > 0;
        }

        public InteractableElement GetFreeElement()
        {
            if (!HasFreeElement())
            {
                if (CanExtend)
                    Extend();
                else
                    throw new System.Exception("No elements");
            }

            var spawnable = _spawnables.First(a => a.Value == SpawnableCondition.InPool).Key;
            _spawnables[spawnable] = SpawnableCondition.Spawned;

            return spawnable;
        }

        public void ReturnToPool(InteractableElement spawnable)
        {
            if (!_spawnables.TryGetValue(spawnable, out var value))
                return;

            spawnable.OnReturn();
            _spawnables[spawnable] = SpawnableCondition.InPool;
        }

        public void ReturnAllElementsToPool()
        {
            foreach (var k in _spawnables.ToList())
            {
                ReturnToPool(k.Key);
            }
        }

        private void Extend()
        {
            var newElement = GameObject.Instantiate<InteractableElement>(_extendedPrefab, _mainTransform);
            newElement.gameObject.SetActive(false);

            _spawnables.Add(newElement, SpawnableCondition.InPool);
        }
    }
}
