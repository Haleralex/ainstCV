using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ainst.Gameplay
{
    public abstract class InteractableElement : MonoBehaviour
    {
        public event Action<InteractableElement> GetTouched;

        [SerializeField]
        private Collider _collider;

        public Vector3 Position => transform.position;

        public Bounds Bounds => _collider.bounds;
        public Vector3 Size => _collider.bounds.size;

        public abstract void OnSpawn();
        public abstract void OnReturn();

        public void OnTouch()
        {
            GetTouched?.Invoke(this);
        }
    }

    public enum SpawnableCondition
    {
        InPool,
        Spawned,
    }
}