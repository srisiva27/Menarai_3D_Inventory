using System;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.UI
{
    public class ItemPresenter : BaseView, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<ItemPresenter> onPointerEnter;
        public event Action onPointerExit;

        public ItemModel _CurrentItemModel { get; private set; }

        [SerializeField] private Image _itemImage = default;

        public void Init(ItemModel itemModel, InventoryWindowSettings settings)
        {
            _CurrentItemModel = itemModel;
            SetColor(settings.itemPresenterColor);
            transform.SetParent(settings.itemPresenterGroup);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("ENTER");
            onPointerEnter?.Invoke(this);
        }

        private void SetColor(Color color)
        {
            _itemImage.color = color;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("EXIT");
            onPointerExit?.Invoke();
        }
    }
}
