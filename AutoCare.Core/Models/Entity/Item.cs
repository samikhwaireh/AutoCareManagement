namespace AutoCare.Core.Models.Entity;

public class Item : BaseEntity<int>
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int Number { get; set; }
    public float Price { get; set; }
    public float SellPrice { get; set; }

    public static Item Create(string name, int quantity, int number, float price, float sellPrice)
    {
        return new Item()
        {
            Name = name,
            Quantity = quantity,
            Number = number,
            Price = price,
            SellPrice = sellPrice,
        };
    }
}
