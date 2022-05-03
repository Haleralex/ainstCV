using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ainst.Gameplay
{
    public class ElementsArranger
    {
        private Vector2 _bordersX;
        private Vector2 _bordersY;
        private Vector2 _bordersZ;

        public ElementsArranger(Vector2 bordersX, Vector2 bordersY, Vector2 bordersZ)
        {
            _bordersX = bordersX;
            _bordersY = bordersY;
            _bordersZ = bordersZ;
        }

        public void ArrangeElement(InteractableElement spawnable)
        {
            if (!IsPossibleToArrange(spawnable))
                throw new System.Exception($"No way to arrange {spawnable.name}");

            var x = Random.Range(
                _bordersX.x + spawnable.Bounds.extents.x,
                _bordersX.y - spawnable.Bounds.extents.x 
            );
            
            var y = Random.Range(
                _bordersY.x + spawnable.Bounds.extents.y,
                _bordersY.y - spawnable.Bounds.extents.y
            );

            var z = Random.Range(
                _bordersZ.x + spawnable.Bounds.extents.z,
                _bordersZ.y - spawnable.Bounds.extents.z
            );

            var position = new Vector3(x, y, z);

            spawnable.transform.position = position;
        }

        private bool IsPossibleToArrange(InteractableElement spawnable)
        {
            if (_bordersX.y - _bordersX.x < spawnable.Bounds.size.x)
                return false;

            if (_bordersY.y - _bordersY.x < spawnable.Bounds.size.y)
                return false;

            if (_bordersZ.y - _bordersZ.x < spawnable.Bounds.size.z)
                return false;

            return true;
        }
    }
}