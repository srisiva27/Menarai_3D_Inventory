using System.Collections.Generic;
using System.Linq;
using Controllers.Enums;
using Controllers.Inputs;
using DG.Tweening;
using MessageBrokers;
using Models;
using UniRx;
using UnityEngine;
using Views.Game;
using Zenject;

namespace Controllers.Game
{
    public class ItemsController : BaseController
    {
        private IInput _input;
        private WorldController _worldController;

        private List<ItemView> _items = new List<ItemView>();

        private ItemView _usingItem;

        private Collider _detectedCollider;

        private CompositeDisposable _disposablesOnDisable = new CompositeDisposable();

        [Inject]
        public ItemsController(IInput input, WorldController worldController)
        {
            _input = input;
            _worldController = worldController;

            InitView();
        }

        public override void Update(float deltaTime)
        {
            if(_usingItem == null || _usingItem.State != ItemState.Draging) return;

            _usingItem.MoveToPosition(_input.PointerWorldPosition());
        }

        public void DestroyItems()
        {
            foreach (var itemView in _items)
            {
                itemView.onTriggerEnter -= OnTriggerEnter;
                itemView.dragAction -= Drag;
            }

            _disposablesOnDisable.Dispose();
        }

        public void RemoveFromInventory(ItemModel itemModel)
        {
            _usingItem = _items.FirstOrDefault(i => i.ItemModel.id == itemModel.id);

            _usingItem.transform.DOMove(Vector3.forward, 1f).OnComplete(SetFree);
            MessageBroker.Default.Publish(new WebRequestMessage(_usingItem.ItemModel.id));
        }

        private void OnTriggerEnter(Collider collider)
        {
            _detectedCollider = collider;
        }

        private void InitView()
        {
            for (int i = 0; i < 3; i++)
            {
                var lol = (int) ItemType.Food;

                var itemType = (ItemType) i;

                var item = Object.Instantiate(Resources.Load<ItemView>(itemType.ToString()), _worldController.GetItemsPosition()[i].position, Quaternion.identity);
                item.onTriggerEnter += OnTriggerEnter;
                item.dragAction += Drag;

                _items.Add(item);
            }

            MessageBroker.Default
                .Receive<DropMessage>()
                .Subscribe(d =>
                {
                    Drop();
                })
                .AddTo(_disposablesOnDisable);
        }

        private void Drag(string id)
        {
            _usingItem = _items.FirstOrDefault(i => i.ItemModel.id == id);

            if (_usingItem is null) return;

            _usingItem.SetDragMode();
            _usingItem.State = ItemState.Draging;
        }

        private void Drop()
        {
            var inventoryInteraction = _detectedCollider?.GetComponent<IInventoryInteraction>();

            if (inventoryInteraction == null)
            {
                SetFree();
                return;
            }

            if(_usingItem == null) return;

            var positionInInventory = inventoryInteraction.PutItem(_usingItem.ItemModel);
            PutInInventory(positionInInventory);
        }

        private void PutInInventory(Vector3 positionInInventory)
        {
            _usingItem.transform.DOMove(positionInInventory, 1f);
            _usingItem.State = ItemState.InInventory;
            MessageBroker.Default.Publish(new WebRequestMessage(_usingItem.ItemModel.id));
            _usingItem = null;
            _detectedCollider = null;
        }

        private void SetFree()
        {
            if(_usingItem == null) return;
            _usingItem.State = ItemState.Free;
            _usingItem.SetDropMode();
            _usingItem = null;
        }
    }
}
