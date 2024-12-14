using System;
using Controllers.Enums;
using UnityEngine;

namespace Views.UI
{
    [Serializable]
    public class InventoryWindowSettings
    {
        public Transform itemPresenterGroup;
        public ItemType itemType;
        public Color itemPresenterColor;
    }
}