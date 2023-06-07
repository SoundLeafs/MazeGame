using UnityEngine;

namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        public enum RotationalAxis
        {
            MouseX,
            MouseY
        }

        [Header("Rotation Variables")]
        public RotationalAxis axis = RotationalAxis.MouseX;
        public float sensitivity = 100f;
        public float minY = -60f, maxY = 60f;
        private float _rotY;

        void Start()
        {
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (GetComponent<Camera>())
            {
                axis = RotationalAxis.MouseY;
            }
        }

        void Update()
        {
            if (axis == RotationalAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity,0);
            }
            else
            {
                _rotY += Input.GetAxis("Mouse Y") * sensitivity;
                _rotY = Mathf.Clamp(_rotY, minY, maxY);
                transform.localEulerAngles = new Vector3(-_rotY, 0, 0);
            }
        }
    }
}
