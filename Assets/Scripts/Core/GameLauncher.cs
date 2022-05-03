using System.Collections;
using System.Collections.Generic;
using ainst.InputHandlers;
using UnityEngine;

namespace ainst.Core
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField]
        private TouchHandler _touchHandler;

        [SerializeField]
        private Spawner _spawner;

        private List<ILauncher> _runners = new List<ILauncher>();

        void Awake()
        {
            _runners.Add(_touchHandler);
            _runners.Add(_spawner);
        }

        void OnEnable()
        {
            foreach (var runner in _runners)
            {
                runner.Launch();
            }
        }
        void OnDisable()
        {
            foreach (var runner in _runners)
            {
                runner.ShutDown();
            }
        }
    }
}