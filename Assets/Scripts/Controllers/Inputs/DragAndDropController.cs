using MessageBrokers;
using UniRx;
using UnityEngine;
using Views.Game;
using Zenject;

namespace Controllers.Inputs
{
    public class DragAndDropController : BaseController
    {
        private IInput _input;

        private Ray _ray;
        private RaycastHit _hit;

        [Inject]
        public DragAndDropController(IInput input)
        {
            _input = input;
        }

        public override void Update(float deltaTime)
        {
            if (_input.Up())
            {
                MessageBroker.Default
                    .Publish(new DropMessage());
            }

            if (_input.Down())
            {
                _ray = Camera.main.ScreenPointToRay(_input.PointerPosition());

                if (Physics.Raycast(_ray, out _hit))
                {
                    var item = _hit.collider.GetComponent<IItemInteraction>();
                    if (item != null)
                    {
                        item.Drag();
                        _input.SetZCoord(_hit.collider.transform.position);
                    }

                    var inventory = _hit.collider.GetComponent<IInventoryInteraction>();
                    if (inventory != null)
                    {
                        inventory.Open();
                        _input.SetZCoord(_hit.collider.transform.position);
                    }
                }
            }
        }
    }
}
