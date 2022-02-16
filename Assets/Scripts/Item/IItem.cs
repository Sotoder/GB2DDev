public interface IItem
{
    int Id { get; set; }
    ItemInfo Info { get; set; }
    ItemTypes ItemType { get; }
}
