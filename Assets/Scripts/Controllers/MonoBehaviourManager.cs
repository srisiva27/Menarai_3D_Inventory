using System.Collections.Generic;
using Controllers.Game;
using Controllers.Inputs;
using Controllers.UI;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class MonoBehaviourManager : BaseController
    {
        private List<BaseController> _controllers = new List<BaseController>();

        [Inject]
        public MonoBehaviourManager(DragAndDropController dragAndDropController, WorldController worldController,
            InventoryController inventoryController, ItemsController itemsController, InventoryWindowController inventoryWindowController)
        {
            _controllers.Add(dragAndDropController);
            _controllers.Add(worldController);
            _controllers.Add(inventoryController);
            _controllers.Add(itemsController);
            _controllers.Add(inventoryWindowController);
        }

        public override void Update(float deltaTime)
        {
            foreach (var controller in _controllers)
            {
                controller.Update(deltaTime);
            }
        }
    }
}
