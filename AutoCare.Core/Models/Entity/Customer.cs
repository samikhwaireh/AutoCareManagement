
namespace AutoCare.Core.Models.Entity;
public class Customer : BaseEntity<int>
{
    public string Name { get; set; }
    public string PlateNumber { get; set; }
    public string PlateType { get; set; }
    public string Phone { get; set; }

    public static Customer Create(string name, string plateNumber, string plateType, string phone)
    {
        return new Customer
        {
            Name = name,
            PlateNumber = plateNumber,
            PlateType = plateType,
            Phone = phone
        };
    }
}

