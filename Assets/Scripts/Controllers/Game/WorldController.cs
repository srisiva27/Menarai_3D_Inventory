using UnityEngine;
using Views.Game;
using Zenject;

namespace Controllers.Game
{
    public class WorldController : BaseController
    {
        private WorldView _worldView;

        [Inject]
        public WorldController()
        {
            Init();
        }

        private void Init()
        {
            _worldView = Object.Instantiate(Resources.Load<WorldView>("World"));
        }

        public Transform[] GetItemsPosition()
        {
            return _worldView.GetItemsPosition();
        }

        public Transform GetInventoryPosition()
        {
            return _worldView.GetInventoryPosition();
        }
    }
}
