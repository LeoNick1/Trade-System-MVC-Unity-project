using UnityEngine;

namespace TS.Model
{
    /// <summary>
    /// Item data SO to design in Unity editor
    /// </summary>
 
    [CreateAssetMenu(menuName = "ScriptableObject/Item", fileName = "ItemData")]
    public class ItemDataSO : ScriptableObject, IItemData
    {
        [SerializeField] private string _itemName;
        [SerializeField] private int _itemId;
        [SerializeField] private int _value;
        [SerializeField] private Sprite _icon;

        public string Name => _itemName;
        public int Id => _itemId;
        public int Value => _value;
        public Sprite Icon => _icon;
    }
}