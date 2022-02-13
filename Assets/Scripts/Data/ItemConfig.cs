using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemConfig : ScriptableObject
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _title;

    [SerializeField]
    private ItemTypes _itemType;

    public int Id => _id;

    public string Title => _title;

    public ItemTypes ItemType => _itemType;
}
