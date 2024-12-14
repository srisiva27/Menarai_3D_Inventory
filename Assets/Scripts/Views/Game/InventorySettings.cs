using System;
using Controllers.Enums;
using UnityEngine;

namespace Views.Game
{
    [Serializable]
    public class InventorySettings
    {
        public ItemType itemType;
        public Transform itemPosition;
    }
}
