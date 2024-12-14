using System;
using System.Linq;
using Controllers.Enums;
using Models;
using UnityEngine;

namespace Views.Game
{
    public class InventoryView : BaseView, IInventoryInteraction
    {
        public event Action openAction;
        public event Action<ItemModel> putItem;

        [SerializeField] private InventorySettings[] _inventorySettingses;

        public Vector3 GetCorrectItemPosition(ItemType itemType)
        {
            var itemPlace = _inventorySettingses.FirstOrDefault(i => i.itemType == itemType);

            return itemPlace?.itemPosition.position ?? Vector3.zero;
        }

        public void Open()
        {
            openAction?.Invoke();
        }

        public void Close()
        {

        }

        public Vector3 PutItem(ItemModel model)
        {
            putItem?.Invoke(model);
            return GetCorrectItemPosition(model.type);
        }

        public void RemoveItem()
        {

        }
    }
}
