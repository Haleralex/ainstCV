using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ainst.Gameplay
{
    public class Cube : InteractableElement
    {
        public override void OnReturn()
        {
            gameObject.SetActive(false);
        }

        public override void OnSpawn()
        {
            gameObject.SetActive(true);
        }
    }
}