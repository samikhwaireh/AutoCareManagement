namespace AutoCare.Core.Models.Entity;

public class ServiceDetails : BaseEntity<int>
{
    public int ParentServiceId { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float PriceFromSupplier { get; set; }
    public float PriceFromShop { get; set; }

    public static ServiceDetails Create(int parentServiceId, string name, float price, float priceFromSupplier, float priceFromShop)
    {
        return new ServiceDetails()
        {
            ParentServiceId = parentServiceId,
            Name = name,
            Price = price,
            PriceFromSupplier = priceFromSupplier,
            PriceFromShop = priceFromShop,
        };
    }
}
