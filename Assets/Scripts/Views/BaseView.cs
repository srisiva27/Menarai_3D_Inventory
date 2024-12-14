using System;
using UnityEngine;

namespace Views
{
    public class BaseView : MonoBehaviour
    {
        public Transform transform;

        private void OnValidate()
        {
            transform = GetComponent<Transform>();
        }
    }
}
