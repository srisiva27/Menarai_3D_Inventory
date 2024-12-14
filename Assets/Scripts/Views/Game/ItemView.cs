using System;
using Controllers.Enums;
using Models;
using UnityEngine;

namespace Views.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class ItemView : BaseView, IItemInteraction
    {
        public event Action<string> dragAction;
        public event Action<Collider> onTriggerEnter;

        public ItemState State { get; set; }

        public ItemModel ItemModel;

        [SerializeField] private Rigidbody _rigidbody = default;

        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke(other);
        }

        public void Drag()
        {
            dragAction?.Invoke(ItemModel.id);
        }

        public void Drop()
        {

        }

        public void MoveToPosition(Vector3 position)
        {
            _rigidbody.MovePosition(position);
        }

        public void SetDragMode()
        {
            _rigidbody.isKinematic = true;
        }

        public void SetDropMode()
        {
            _rigidbody.isKinematic = false;
        }
    }
}
