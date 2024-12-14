using Controllers.Enums;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
    public class ItemModel : ScriptableObject
    {
        public int weight;
        public string name;
        public string id;
        public ItemType type;
    }
}
