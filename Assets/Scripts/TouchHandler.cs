using System;
using System.Collections;
using System.Collections.Generic;
using ainst.Core;
using ainst.Gameplay;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ainst.InputHandlers
{
    public class TouchHandler : MonoBehaviour, ILauncher
    {
        [SerializeField]
        private Camera _arCamera;

        private bool _isInputEnabled = false;

        public void Launch()
        {
            _isInputEnabled = true;
        }

        public void ShutDown()
        {
            _isInputEnabled = false;
        }

        void Update()
        {
            if (!_isInputEnabled)
                return;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                var touchPosition = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = _arCamera.ScreenPointToRay(touchPosition);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        if (!hitObject.collider.gameObject.TryGetComponent<InteractableElement>(out var spawnable))
                            return;

                        spawnable.OnTouch();
                    }
                }
            }
        }
    }
}