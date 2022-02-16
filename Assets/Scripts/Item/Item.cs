public class Item : IItem
{
    public int Id { get; set; }
    public ItemInfo Info { get; set; }
    public ItemTypes ItemType => Info.ItemType;
}
