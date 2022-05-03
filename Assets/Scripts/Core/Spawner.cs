using System;
using System.Collections;
using System.Collections.Generic;
using ainst.Gameplay;
using UnityEngine;

namespace ainst.Core
{
    public class Spawner : MonoBehaviour, ILauncher
    {
        public event Action<InteractableElement> Spawned;
        public event Action Respawned;

        [SerializeField]
        private InteractableElement _extendedPrefab;

        [SerializeField]
        private List<InteractableElement> _spawnables = new List<InteractableElement>();

        [SerializeField]
        private Vector2 _bordersX, _bordersY, _bordersZ;


        [Range(1, 100)]
        public float SpawnPeriod;


        [Range(0, 100)]
        public float Delay = 0.0f;

        private ElementsArranger _spawnablesArranger;
        private Pool _pool;

        public bool PoolCanExtend = false;


        void Awake()
        {
            _spawnablesArranger = new ElementsArranger(_bordersX, _bordersY, _bordersZ);

            _pool = new Pool(_spawnables, _extendedPrefab, transform, PoolCanExtend);
        }

        private void RandomSpawn()
        {
            var newElement = _pool.GetFreeElement();

            _spawnablesArranger.ArrangeElement(newElement);
            newElement.OnSpawn();

            Spawned?.Invoke(newElement);
        }

        public void ReturnToPool(InteractableElement spawnable)
        {
            _pool.ReturnToPool(spawnable);
            spawnable.OnReturn();
        }

        public void Respawn()
        {
            ShutDown();
            _pool.ReturnAllElementsToPool();
            Launch();

            Respawned?.Invoke();
        }

        public void Launch()
        {
            InvokeRepeating(nameof(RandomSpawn), Delay, SpawnPeriod);
        }

        public void ShutDown()
        {
            CancelInvoke(nameof(RandomSpawn));
        }
    }
}
