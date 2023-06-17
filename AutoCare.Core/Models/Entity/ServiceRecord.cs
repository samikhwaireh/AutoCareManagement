namespace AutoCare.Core.Models.Entity;

public class ServiceRecord : BaseEntity<int>
{
    public int CustomerId { get; set; }
    public float TotalPaid { get; set; }
    public string Note { get; set; }
    public float Debt { get; set; }
    public float Other { get; set; }

    public static ServiceRecord Create(int customerId, DateTime addedTime, float totalPaid, string note, float other)
    {
        return new ServiceRecord()
        {
            CustomerId = customerId,
            AddedTime = addedTime,
            TotalPaid = totalPaid,
            Note = note,
            Other = other,
        };
    }
}
