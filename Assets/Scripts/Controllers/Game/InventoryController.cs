using Controllers.UI;
using MessageBrokers;
using Models;
using UniRx;
using UnityEngine;
using Views.Game;
using Zenject;

namespace Controllers.Game
{
    public class InventoryController : BaseController
    {
        private WorldController _worldController;
        private InventoryWindowController _inventoryWindow;

        private InventoryView _inventoryView;

        [Inject]
        public InventoryController(WorldController worldController, InventoryWindowController inventoryWindowController)
        {
            _worldController = worldController;
            _inventoryWindow = inventoryWindowController;
            Init();
        }

        private void Init()
        {
            _inventoryView = Object.Instantiate(Resources.Load<InventoryView>("Inventory"),
                _worldController.GetInventoryPosition().position, Quaternion.identity);

            _inventoryView.openAction += Open;
            _inventoryView.putItem += PutItem;
        }

        public void Open()
        {
            _inventoryWindow.Show();
        }

        public void Close()
        {
            _inventoryWindow.Hide();
        }

        public void PutItem(ItemModel itemModel)
        {
            _inventoryWindow.PutInInventory(itemModel);
        }
    }
}
