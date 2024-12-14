using UnityEngine;

namespace Views.Game
{
    public class WorldView : BaseView
    {
        [SerializeField] private Transform[] _itemsPosition;
        [SerializeField] private Transform _inventoryPosition;

        public Transform[] GetItemsPosition()
        {
            return _itemsPosition;
        }

        public Transform GetInventoryPosition()
        {
            return _inventoryPosition;
        }
    }
}
