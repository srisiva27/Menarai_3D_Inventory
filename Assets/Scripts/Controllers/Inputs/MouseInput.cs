using UnityEngine;

namespace Controllers.Inputs
{
    public class MouseInput : BaseController, IInput
    {
        private float zCoord;

        public Vector3 PointerWorldPosition()
        {
            var mousePosition = PointerPosition();

            mousePosition.z = zCoord;

            return Camera.main.ScreenToWorldPoint(mousePosition);
        }

        public Vector3 PointerPosition()
        {
            return Input.mousePosition;
        }

        public void SetZCoord(Vector3 objectPosition)
        {
            zCoord = Camera.main.WorldToScreenPoint(objectPosition).z;
        }

        public bool Click()
        {
            return Input.GetMouseButton(0);
        }

        public bool Down()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool Up()
        {
            return Input.GetMouseButtonUp(0);
        }
    }
}
