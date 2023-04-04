using UnityEngine;

namespace TS.Model
{
    public interface IItemData
    {
        string Name { get; }
        int Id { get; }
        int Value { get; }
        Sprite Icon { get; }
    }
}