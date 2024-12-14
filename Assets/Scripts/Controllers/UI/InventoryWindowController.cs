using System.Collections.Generic;
using Controllers.Game;
using MessageBrokers;
using Models;
using UniRx;
using UnityEngine;
using Views.UI;
using Zenject;

namespace Controllers.UI
{
    public class InventoryWindowController : BaseController
    {
        private InventoryWindowView _windowView;

        private List<ItemPresenter> _presenters = new List<ItemPresenter>();

        private ItemPresenter _selectedPresenter;

        private ItemsController _itemsController;

        private CompositeDisposable _disposablesOnDisable = new CompositeDisposable();

        [Inject]
        public InventoryWindowController(Canvas canvas, ItemsController itemsController)
        {
            _itemsController = itemsController;
            Init(canvas);
        }

        private void Init(Canvas canvas)
        {
            _windowView = Object.Instantiate(Resources.Load<InventoryWindowView>("InventoryWindow"), canvas.transform);
            MessageBroker.Default.Receive<DropMessage>()
                .Subscribe(_=>
                {
                    RemovePresenter();
                    DeselectPresenter();
                    Hide();
                })
                .AddTo(_disposablesOnDisable);
            Hide();
        }

        public void Hide()
        {
            _windowView.gameObject.SetActive(false);
        }

        public void Show()
        {
            _windowView.gameObject.SetActive(true);
        }

        public void PutInInventory(ItemModel itemModel)
        {
            var presenter = CreateItemPresenter();
            presenter.Init(itemModel, _windowView.SetPresenter(itemModel.type));
            presenter.onPointerEnter += SelectPresenter;
            presenter.onPointerExit += DeselectPresenter;

            _presenters.Add(presenter);
        }

        private void RemovePresenter()
        {
            if(_selectedPresenter == null) return;

            _itemsController.RemoveFromInventory(_selectedPresenter._CurrentItemModel);

            _selectedPresenter.onPointerEnter -= SelectPresenter;
            _selectedPresenter.onPointerExit -= DeselectPresenter;
            _presenters.Remove(_selectedPresenter);
            Object.Destroy(_selectedPresenter.gameObject);
            _selectedPresenter = null;
        }

        private void DeselectPresenter()
        {
            _selectedPresenter = null;
        }

        private void SelectPresenter(ItemPresenter itemPresenter)
        {
            _selectedPresenter = itemPresenter;
        }

        private ItemPresenter CreateItemPresenter()
        {
            return Object.Instantiate(Resources.Load<ItemPresenter>("ItemPresenter"));
        }
    }
}
