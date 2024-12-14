using System.Linq;
using Controllers.Enums;
using UnityEngine;

namespace Views.UI
{
    public class InventoryWindowView : BaseView
    {
        [SerializeField] private InventoryWindowSettings[] _inventoryWindowSettingses;

        public InventoryWindowSettings SetPresenter(ItemType itemType)
        {
            return _inventoryWindowSettingses.FirstOrDefault(i => i.itemType == itemType);
        }
    }
}
