using UnityEngine;

namespace Controllers.Inputs
{
    public interface IInput
    {
        public Vector3 PointerWorldPosition();
        public Vector3 PointerPosition();

        public void SetZCoord(Vector3 objectPosition);

        public bool Down();
        public bool Up();
    }
}
