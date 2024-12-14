using Models;
using UnityEngine;

namespace Views.Game
{
    public interface IInventoryInteraction
    {
        public void Open();
        public void Close();

        public Vector3 PutItem(ItemModel model);
        public void RemoveItem();
    }
}
